// File: LevelManager.Part1.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Zelda.Enums;
using MainGame.CollisionHandling;
using MainGame.Sprites;
using MainGame.States;

namespace MainGame.Display
{
    public partial class LevelManager : ILevel, IGameObject
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

        private GameState UpdateGameState;
        private CollisionManager collisionManager;
        private Game1 myGame;
        public Game1 game => myGame;
        public Link myLink { get; set; }

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

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (UpdateGameState != Zelda.Enums.GameState.Playing && UpdateGameState != Zelda.Enums.GameState.Paused)
                return;

            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0,
                roomWidth * LevelConstants.BACKGROUND_SIZE_SCALAR, roomHeight * LevelConstants.BACKGROUND_SIZE_SCALAR),
                sourceRectangle, Color.White);

            myLink.Draw(spriteBatch);
            if (transition == 0) currentRoom?.Draw(spriteBatch, myLink);

            if (transition > 0)
            {
                RoomTransition(newRoomCoords[0], newRoomCoords[1], transition);
            }
        }

        public override void Update(GameTime gameTime)
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
                myGame.GamesPlayed += 1;
                myGame.GamesLost += 1;
            }
        }
    }
}

