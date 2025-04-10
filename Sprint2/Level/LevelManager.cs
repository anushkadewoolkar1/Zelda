
/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Zelda.Enums;
using MainGame.CollisionHandling;
using MainGame.Sprites;

namespace MainGame.Display
{
    public class LevelManager : ILevel, IGameObject
    {
        private LevelFileReader levelData;
        private Room currentRoom;
        private Texture2D backgroundTexture;
        private List<IGameObject> globalGameObjects;
        private ContentManager contentManager;

        //Room coordinate tracking
        public int[] currentRoomCoords { get; private set; }
        private int[] newRoomCoords;
        private int transition;
        private int tokenStartIndex; 

        public bool moveLink { get; set; }
        public bool doubleClickTemp { get; set; }

        private GameState UpdateGameState;
        private CollisionManager collisionManager;
        private Game1 myGame;
        private Link myLink;


        private int roomWidth, roomHeight;
        private Rectangle sourceRectangle;
        private Vector2 roomDimensions;

        public LevelManager(ContentManager content, List<IGameObject> gameObjects)
        {
            contentManager = content;
            globalGameObjects = gameObjects;
            backgroundTexture = contentManager.Load<Texture2D>("Levels Spritesheet");

            roomWidth = (backgroundTexture.Width - LevelConstants.HEIGHT_POSITION) / LevelConstants.LEVEL_TEXTURE_SCALAR;
            roomHeight = (backgroundTexture.Height - LevelConstants.HEIGHT_POSITION) / LevelConstants.LEVEL_TEXTURE_SCALAR;
            sourceRectangle = new Rectangle(
                (roomWidth * LevelConstants.WIDTH_POSITION) + LevelConstants.SHIFT_INTO_RANGE,
                (roomHeight * LevelConstants.HEIGHT_POSITION) + LevelConstants.SHIFT_INTO_RANGE,
                roomWidth, roomHeight);
            roomDimensions = new Vector2((roomWidth * LevelConstants.WIDTH_POSITION) - LevelConstants.LEVEL_CENTER_POSITION,
                                         (roomHeight * LevelConstants.WIDTH_POSITION) - LevelConstants.LEVEL_CENTER_POSITION);

            levelData = new LevelFileReader("../../../Content/LevelFile.txt");

            currentRoomCoords = new int[] { LevelConstants.WIDTH_POSITION, LevelConstants.HEIGHT_POSITION };
            newRoomCoords = new int[] { LevelConstants.WIDTH_POSITION, LevelConstants.HEIGHT_POSITION };

            moveLink = true;
            doubleClickTemp = false;
            UpdateGameState = Zelda.Enums.GameState.StartMenu;
        }

        public void AddLink(Link link)
        {
            myLink = link;
            myLink.level = this;
        }

        public void Game(Game1 game)
        {
            myGame = game;
        }

        public void CollisionManager(CollisionManager manager)
        {
            collisionManager = manager;
        }

        public void GameState(GameState state)
        {
            UpdateGameState = state;
            if (UpdateGameState != Zelda.Enums.GameState.StartMenu)
                myGame.GameState = state;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (UpdateGameState != Zelda.Enums.GameState.Playing && UpdateGameState != Zelda.Enums.GameState.Paused)
                return;

            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0,
                roomWidth * LevelConstants.BACKGROUND_SIZE_SCALAR, roomHeight * LevelConstants.BACKGROUND_SIZE_SCALAR),
                sourceRectangle, Color.White);

            myLink.Draw(spriteBatch);
            currentRoom?.Draw(spriteBatch, myLink);

            if (transition > 0)
            {
                RoomTransition(newRoomCoords[0], newRoomCoords[1], transition);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (UpdateGameState != Zelda.Enums.GameState.Playing)
                return;

            collisionManager.CheckDynamicCollisions(globalGameObjects, this);
            myLink.Update(globalGameObjects, gameTime);
            currentRoom?.Update(gameTime, globalGameObjects);

            if (myLink.Health <= 0)
            {
                myGame.GameState = Zelda.Enums.GameState.GameOver;
                myLink.Health = 3;
            }
        }

        // finds and initiates room loading based on coords
        public void LoadRoom(int xCoordinate, int yCoordinate)
        {
            transition = 1;
            int count = levelData.Objects.Count;
            int index = 1;
            bool found = false;

            while (index < count && !found)
            {
                if (xCoordinate.ToString() == levelData.Objects[index - LevelConstants.SHIFT_INTO_RANGE] &&
                    yCoordinate.ToString() == levelData.Objects[index])
                {
                    found = true;
                }
                index++;
            }

            if (!found || index == count)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to Find Room {xCoordinate}, {yCoordinate}");
                return;
            }

            RemoveOldObjects();

            newRoomCoords[0] = xCoordinate;
            newRoomCoords[1] = yCoordinate;
            tokenStartIndex = index;

            // adjust links position if u need to
            if (xCoordinate != currentRoomCoords[0] && moveLink)
            {
                int roomCenterX = roomWidth - 16;
                myLink.Position = new Vector2(
                    roomCenterX + Math.Sign(roomCenterX - myLink.Position.X) * (roomCenterX - this.BoundingBox.X - 12),
                    myLink.Position.Y);
            }
            else if (yCoordinate != currentRoomCoords[1] && moveLink)
            {
                int roomCenterY = roomHeight - 16;
                myLink.Position = new Vector2(
                    myLink.Position.X,
                    roomCenterY + Math.Sign(roomCenterY - myLink.Position.Y) * (roomCenterY - this.BoundingBox.Y - 12));
            }

            myLink.noMoving = true;

            // if the requested room is the same as the current room, load right away else transition
            if (currentRoomCoords[0] == newRoomCoords[0] && currentRoomCoords[1] == newRoomCoords[1])
            {
                LoadRoomEnd(xCoordinate, yCoordinate);
            }
            else
            {
                RoomTransition(xCoordinate, yCoordinate, 0);
            }
        }

        /// <summary>
        /// finishes the room loading process by creating a new Room instance and loading its data
        /// </summary>
        public void LoadRoomEnd(int xCoordinate, int yCoordinate)
        {
            sourceRectangle = new Rectangle(
                roomWidth * xCoordinate + LevelConstants.SHIFT_INTO_RANGE * (xCoordinate + LevelConstants.SHIFT_INTO_RANGE),
                roomHeight * yCoordinate + LevelConstants.SHIFT_INTO_RANGE * (yCoordinate + 1),
                roomWidth, roomHeight);

            currentRoom = new Room(contentManager, backgroundTexture);
            currentRoom.LoadRoomData(xCoordinate, yCoordinate, tokenStartIndex, levelData.Objects, globalGameObjects, myLink);

            currentRoomCoords[0] = xCoordinate;
            currentRoomCoords[1] = yCoordinate;

            myLink.noMoving = false;
            transition = 0;
        }

        private void RemoveOldObjects()
        {
            if (currentRoom != null)
            {
                foreach (var block in currentRoom.Blocks)
                    globalGameObjects.Remove(block);
                foreach (var enemy in currentRoom.Enemies)
                    globalGameObjects.Remove(enemy);
                foreach (var item in currentRoom.Items)
                    globalGameObjects.Remove(item);
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(LevelConstants.LEVEL_CENTER_POSITION - 9,
                    LevelConstants.LEVEL_CENTER_POSITION - 9,
                    (int)roomDimensions.X - LevelConstants.LEVEL_CENTER_POSITION + 18,
                    (int)roomDimensions.Y - LevelConstants.LEVEL_CENTER_POSITION + 18);
            }
        }

        public Vector2 Velocity => Vector2.Zero;

        public void Destroy()
        {
            // Clean up as needed.
        }

        // handles room transition effect by updating the background source rectangle
        public void RoomTransition(int xCoordinate, int yCoordinate, int transitionNumber)
        {
            if (xCoordinate != currentRoomCoords[0])
            {
                int oldX = roomWidth * currentRoomCoords[0] + LevelConstants.SHIFT_INTO_RANGE * (currentRoomCoords[0] + LevelConstants.SHIFT_INTO_RANGE);
                int newX = roomWidth * xCoordinate + LevelConstants.SHIFT_INTO_RANGE * (xCoordinate + LevelConstants.SHIFT_INTO_RANGE);
                sourceRectangle.X = oldX + ((newX - oldX) % (LevelConstants.ROOM_TRANSITION_SPEED * 6)) * transitionNumber * LevelConstants.ROOM_TRANSITION_SPEED;
            }
            else
            {
                int oldY = roomHeight * currentRoomCoords[1] + LevelConstants.SHIFT_INTO_RANGE * (currentRoomCoords[1] + LevelConstants.SHIFT_INTO_RANGE);
                int newY = roomHeight * yCoordinate + LevelConstants.SHIFT_INTO_RANGE * (yCoordinate + LevelConstants.SHIFT_INTO_RANGE);
                sourceRectangle.Y = oldY + ((newY - oldY) % (LevelConstants.ROOM_TRANSITION_SPEED * 2)) * transitionNumber * LevelConstants.ROOM_TRANSITION_SPEED;
            }
            transition++;
            if (transition == 19)
                LoadRoomEnd(xCoordinate, yCoordinate);
        }
    }
}
*/