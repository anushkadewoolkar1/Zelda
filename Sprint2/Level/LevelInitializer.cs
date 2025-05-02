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
    public partial class LevelManager : BaseLevel, IGameObject
    {
        public override void LoadRoom(int xCoordinate, int yCoordinate)
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
            const float BUFFER = 16f;

            if (moveLink)
            {
                Vector2 newPosition = myLink.Position;

                System.Diagnostics.Debug.WriteLine($"X: {newPosition.X} Y: {newPosition.Y}");

                if (newPosition.X < 100)
                {
                    newPosition = new Vector2(
                                BoundingBox.Right - (myLink.BoundingBox.Width / 2f) - BUFFER,
                                BoundingBox.Center.Y);
                }
                else if (newPosition.X > 400)
                {
                    newPosition = new Vector2(
                                BoundingBox.Left + (myLink.BoundingBox.Width / 2f) + BUFFER,
                                BoundingBox.Center.Y);
                }
                else if (newPosition.Y < 100)
                {
                    newPosition = new Vector2(
                                BoundingBox.Center.X,
                                BoundingBox.Bottom - (myLink.BoundingBox.Height / 2f) - BUFFER);
                }
                else
                {
                    newPosition = new Vector2(
                                BoundingBox.Center.X,
                                BoundingBox.Top + (myLink.BoundingBox.Height / 2f) + BUFFER);
                }

                myLink.Position = newPosition;
            }
            myLink.DestroyPortals();

            myLink.noMoving = true;

            if (currentRoomCoords[0] == newRoomCoords[0] && currentRoomCoords[1] == newRoomCoords[1])
            {
                LoadRoomEnd(xCoordinate, yCoordinate);
            }
            else
            {
                RoomTransition(xCoordinate, yCoordinate, 0);
            }
        }

        public void LoadRoomEnd(int xCoordinate, int yCoordinate)
        {
            sourceRectangle = new Rectangle(
                roomWidth * xCoordinate + LevelConstants.SHIFT_INTO_RANGE * (xCoordinate + LevelConstants.SHIFT_INTO_RANGE),
                roomHeight * yCoordinate + LevelConstants.SHIFT_INTO_RANGE * (yCoordinate + 1),
                roomWidth, roomHeight);

            currentRoom = new Room(contentManager, backgroundTexture, this);
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
        }

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
            if (transition == LevelConstants.ROOM_TRANSITION_END)
                LoadRoomEnd(xCoordinate, yCoordinate);
        }

        public int[] GetCurrentRoomCoords()
        {
            return currentRoomCoords;
        }

        public void SpawnTriForce()
        {
            Vector2 offset = Vector2.Zero;
            switch (myLink.currentDirection)
            {
                case Direction.Up:
                    offset = new Vector2(0, -50);
                    break;
                case Direction.Down:
                    offset = new Vector2(0, 50);
                    break;
                case Direction.Left:
                    offset = new Vector2(-50, 0);
                    break;
                case Direction.Right:
                    offset = new Vector2(50, 0);
                    break;
            }

            Vector2 spawnPosition = myLink.Position + offset;
            int SpawnX = (int)spawnPosition.X;
            int SpawnY = (int)spawnPosition.Y;

            Item triForce = new Item();
            triForce = triForce.CreateItem(ItemType.Triforce, SpawnX, SpawnY);

            globalGameObjects.Add(triForce);
        }

        public Link ReturnLink()
        {
            return myLink;
        }
    }
}

