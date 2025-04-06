using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Input;
using MainGame.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using MainGame.Sprites;
using Zelda.Enums;
using MainGame.States;
using System.Reflection.Metadata;
using MainGame.CollisionHandling;
using System.Reflection.Emit;


namespace MainGame.Display
{
    public class Level : ILevel, IGameObject
    {

        public int roomWidth;
        public int roomHeight;
        private Texture2D _backgroundTexture;
        private Rectangle _sourceRectangle;
        public List<String> Objects = new List<String>();
        public List<Block> blocksList { get; set; }
        public List<Enemy> enemiesList { get; set; }
        public List<Item> itemsList { get; set; }
        private Vector2 roomDimensions;
        public int enemiesListIndex { get; set; }
        public int blocksListIndex { get; set; }
        public int itemsListIndex { get; set; }

        public int[] currentRoom { get; set; }

        private int transition;
        private int loadCurrentPosition;
        private int[] newRoom;

        public bool doubleClickTemp { get; set; }

        private ContentManager contentManager;

        private List<IGameObject> gameObjects;

        private const int LEVEL_CENTER_POSITION = 60;
        private const int WIDTH_POSITION = 2;
        private const int HEIGHT_POSITION = 5;
        private const int LEVEL_TEXTURE_SCALAR = 6;
        private const int BACKGROUND_SIZE_SCALAR = 2;
        private const int ITEM_START_INDEX = 5;
        private const int ENEMY_START_INDEX = 6;
        private const int BLOCKS_PER_ROOM_X = 14;
        private const int BLOCKS_PER_ROOM_Y = 9;
        private const int BLOCK_X_ADJUST = 32;
        private const int SHIFT_INTO_RANGE = 1;
        private const int ROOM_STARTING_POINT = 12;
        private const int ROOM_TRANSITION_SPEED = 3;

        private Game1 myGame;

        private Link myLink;

        private GameState UpdateGameState;

        private CollisionManager collisionManager;

        public Level(ContentManager Content, List<IGameObject> _gameObjects)
        {
            //Creates new collection of Blocks, Enemy and Items to be loaded into levels
            blocksList = new List<Block>();
            enemiesList = new List<Enemy>();
            itemsList = new List<Item>();

            //Creates the background room dimensions as well as loading the entire level texture
            this.contentManager = Content;
            _backgroundTexture = Content.Load<Texture2D>("Levels Spritesheet");
            roomWidth = (_backgroundTexture.Width - HEIGHT_POSITION) / LEVEL_TEXTURE_SCALAR;
            roomHeight = (_backgroundTexture.Height - HEIGHT_POSITION) / LEVEL_TEXTURE_SCALAR;
            _sourceRectangle = new Rectangle(
                (roomWidth * WIDTH_POSITION) + SHIFT_INTO_RANGE, (roomHeight * HEIGHT_POSITION) + SHIFT_INTO_RANGE, roomWidth, roomHeight);
            roomDimensions = new Vector2((roomWidth * WIDTH_POSITION) - LEVEL_CENTER_POSITION, (roomHeight * WIDTH_POSITION) - LEVEL_CENTER_POSITION);

            //Reads through the levelfile to create the various levels
            foreach (var line in File.ReadLines("../../../Content/LevelFile.txt"))
            {
                var nums = line.Split(',');
                for (int i = 0; i < nums.Length; i++)
                {
                    Objects.Add(nums[i]);
                }
            }

            //Current implementation involves clicking through rooms
            //used to avoid accidental skipping rooms
            doubleClickTemp = false;

            gameObjects = _gameObjects;

            //Spawn Room
            currentRoom = [WIDTH_POSITION, HEIGHT_POSITION];

            newRoom = [WIDTH_POSITION, HEIGHT_POSITION];

            UpdateGameState = Zelda.Enums.GameState.StartMenu;

        }


        //Checks for which type of block to create
        private Block CreateBlock(string blockType, Vector2 position, Texture2D[] textures)
        {
            Block block = new Block(position, textures, this);

            if (blockType.Contains("Load"))
            {
                block.loadRoom = new Vector2(Int32.Parse(blockType.Substring(14, 1)), Int32.Parse(blockType.Substring(16, 1)));
            }
            return block;
        }

        private Texture2D[] LoadBlockTextures()
        {
            return new Texture2D[]
            {
                contentManager.Load<Texture2D>("transparent_block") // Change filename as needed
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (UpdateGameState != Zelda.Enums.GameState.Playing &&
                UpdateGameState != Zelda.Enums.GameState.Paused)
            {
                return;
            }

            //Draws room background
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0,
               roomWidth * BACKGROUND_SIZE_SCALAR, roomHeight * BACKGROUND_SIZE_SCALAR), _sourceRectangle, Color.White);

            myLink.Draw(spriteBatch);

            if (transition >= 1)
            {
                RoomTransition(newRoom[0], newRoom[1], transition);
            }

            //Goes through each list drawing each object
            for (int i = 0; i < enemiesList.Count;i++)
            {
                enemiesList[i].DrawCurrentSprite(spriteBatch);
            }
            for (int i = 0; i < blocksList.Count; i++)
            {
                blocksList[i].Draw(spriteBatch);
            }
            for (int i = 0; i < itemsList.Count;i++)
            {
                itemsList[i].Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (UpdateGameState != Zelda.Enums.GameState.Playing)
            {
                return;
            }

            collisionManager.CheckDynamicCollisions(gameObjects, this);

            myLink.Update(gameObjects, gameTime);

            //Goes through each list updating each object
            for (int i = 0; i < enemiesList.Count; i++)
            {
                enemiesList[i].Update(gameTime);
            }
            for (int i = 0; i < blocksList.Count; i++)
            {
                blocksList[i].Update();
            }
            for (int i = 0; i < itemsList.Count; i++)
            {
                itemsList[i].Update(gameTime);
            }

            if (myLink.Health <= 0)
            {
                myGame.GameState = Zelda.Enums.GameState.GameOver;
                myLink.Health = 3;
            }
        }

        //Finds requested room from objects list and adds objects according to levelFile.txt
        //Returns and prints failed to find room if failed to find requested room
        public override void LoadRoom(int xCoordinate, int yCoordinate)
        {

            transition = 1;

            //Temporary implementation for clicking through rooms double click bug
            if (!doubleClickTemp)
            {
                doubleClickTemp = true;
            } else
            {
                doubleClickTemp = false;
                return;
            }

            int count = Objects.Count;
            int currentPosition = 1;
            Boolean foundRoom = false;

            //Goes through the level file, looking for the room coordinates to find start of room
            while (currentPosition < count && !foundRoom)
            {

                if (xCoordinate.ToString() == Objects[currentPosition - SHIFT_INTO_RANGE] && yCoordinate.ToString() == Objects[currentPosition])
                {
                    foundRoom = true;
                }
                currentPosition++;
            }

            //Removes object from various lists
            RemoveOldObjects();

            //Avoids loading room when room is not found
            if (currentPosition == count || !foundRoom)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to Find Room {xCoordinate}, {yCoordinate}");
                return;
            }

            newRoom[0] = xCoordinate;
            newRoom[1] = yCoordinate;
            loadCurrentPosition = currentPosition;

            if (xCoordinate != currentRoom[0])
            {
                myLink.Position = new Vector2(
                    myLink.Position.X + 2 * ((roomWidth) - 16 - myLink.Position.X),
                    myLink.Position.Y);
            }
            else if (yCoordinate != currentRoom[1])
            {
                myLink.Position = new Vector2(
                    myLink.Position.X,
                    myLink.Position.Y + 2 * ((roomHeight) - 16 - myLink.Position.Y));
            }

            if (currentRoom == newRoom)
            {
                LoadRoomEnd(xCoordinate, yCoordinate, currentPosition);
            }
            else
            {
                RoomTransition(xCoordinate, yCoordinate, 0);
            }
        }

        public void LoadRoomEnd(int xCoordinate, int yCoordinate, int currentPosition)
        {

            //Creates rectangle for found room. Used for drawing
            _sourceRectangle = new Rectangle(
                roomWidth * (xCoordinate) + SHIFT_INTO_RANGE * (xCoordinate + SHIFT_INTO_RANGE), roomHeight * (yCoordinate) + SHIFT_INTO_RANGE * (yCoordinate + 1),
                roomWidth, roomHeight);

            //Get's to the start of the room and sets room size
            currentPosition += ROOM_STARTING_POINT;
            int room = currentPosition + BLOCKS_PER_ROOM_X * BLOCKS_PER_ROOM_Y;
            
            

            //We'll need to be able to pass position for creating the objects of the entities
            int startOfRoom = currentPosition;

            while (currentPosition < room)
            {
                //Constructs Enemies
                if (Objects[currentPosition].Contains("Enemy"))
                {
                    CreateEnemy(currentPosition , startOfRoom);
                }
                //Constructs Blocks
                else if (Objects[currentPosition].Contains("Block"))
                {
                    CreateBlock(currentPosition , startOfRoom);
                } 
                else if (Objects[currentPosition].Contains("Item"))
                //Constructs Items
                {
                    CreateItem(currentPosition , startOfRoom);
                }
                currentPosition++;
            }

            System.Diagnostics.Debug.WriteLine($"Check Middle {(roomHeight) - 16} and {myLink.Position.Y}");


            currentRoom = [xCoordinate, yCoordinate];

            transition = 0;
        }

        private void CreateEnemy(int currentPosition, int startOfRoom)
        {
            EnemyType enemyType;
            enemiesList.Add(new Enemy());
            Enum.TryParse(Objects[currentPosition].Substring(ENEMY_START_INDEX).ToString(), out enemyType);
            enemiesList[enemiesList.Count - 1].CreateEnemy(enemyType,
                new Vector2(((currentPosition - startOfRoom) % BLOCKS_PER_ROOM_X) - SHIFT_INTO_RANGE,
                ((currentPosition - startOfRoom) / BLOCKS_PER_ROOM_X) - SHIFT_INTO_RANGE));
            gameObjects.Add(enemiesList[enemiesList.Count - 1]);
        }

        private void CreateBlock(int currentPosition, int startOfRoom)
        {
            Vector2 position = new Vector2(
                        (roomDimensions.X / BLOCKS_PER_ROOM_X) * ((currentPosition - startOfRoom) % BLOCKS_PER_ROOM_X) + BLOCK_X_ADJUST,
                        (roomDimensions.Y / BLOCKS_PER_ROOM_Y) * ((currentPosition - startOfRoom) / BLOCKS_PER_ROOM_X) + BLOCK_X_ADJUST
                    );

            Texture2D[] blockTextures = LoadBlockTextures();

            string blockType = Objects[currentPosition]; // Get the block type from the level file

            Block newBlock = CreateBlock(blockType, position, blockTextures);

            //Block newBlock = CreateBlock(blockType, position, blockTextures); 
            blocksList.Add(newBlock);

            gameObjects.Add(blocksList[blocksList.Count - 1]);

            //System.Diagnostics.Debug.WriteLine($"Created {blockType} at {position}");
        }

        private void CreateItem(int currentPosition, int startOfRoom)
        {
            ItemType itemType;
            itemsList.Add(new Item());
            Enum.TryParse(Objects[currentPosition].Substring(ITEM_START_INDEX), out itemType);
            itemsList[itemsList.Count - 1] = itemsList[itemsListIndex].CreateItem(itemType,
                ((currentPosition - startOfRoom) % BLOCKS_PER_ROOM_X) - SHIFT_INTO_RANGE,
                ((currentPosition - startOfRoom) / BLOCKS_PER_ROOM_X) - SHIFT_INTO_RANGE);
            gameObjects.Add((itemsList[itemsList.Count - 1]));
        }

        private void RemoveOldObjects()
        {
            //Removes old objects from collisions handling
            for (int j = 0; j < blocksList.Count; j++)
            {
                this.gameObjects.Remove(blocksList[j]);
            }
            for (int j = 0; j < enemiesList.Count; j++)
            {
                this.gameObjects.Remove(enemiesList[j]);
            }
            for (int j = 0; j < itemsList.Count; j++)
            {
                this.gameObjects.Remove(itemsList[j]);
            }

            //Clears object list
            blocksList.Clear();
            enemiesList.Clear();
            itemsList.Clear();
        }

        public Rectangle BoundingBox
        {
            get
            {
                //return new Rectangle(LEVEL_CENTER_POSITION - 8, LEVEL_CENTER_POSITION - 8, (int)roomDimensions.X - LEVEL_CENTER_POSITION + 16, (int)roomDimensions.Y - LEVEL_CENTER_POSITION + 16);
                return new Rectangle(LEVEL_CENTER_POSITION - 4, LEVEL_CENTER_POSITION - 8, (int)roomDimensions.X - LEVEL_CENTER_POSITION + 10, (int)roomDimensions.Y - LEVEL_CENTER_POSITION + 4);
            }
        }
        public Vector2 Velocity
        {
            get { return new Vector2(0, 0); }
        }

        public void Destroy()
        {

        }

        public void RoomTransition(int xCoordinate, int yCoordinate, int transitionNumber)
        {
            if (xCoordinate != currentRoom[0])
            {
                int oldXcoordinate = roomWidth * (currentRoom[0]) + SHIFT_INTO_RANGE * (currentRoom[0] + SHIFT_INTO_RANGE);
                int newXcoordinate = roomWidth * (xCoordinate) + SHIFT_INTO_RANGE * (xCoordinate + SHIFT_INTO_RANGE);
                _sourceRectangle.X = oldXcoordinate + ((newXcoordinate - oldXcoordinate) % (ROOM_TRANSITION_SPEED * 6)) * transitionNumber * ROOM_TRANSITION_SPEED;
            } else
            {
                int oldYcoordinate = roomHeight * (currentRoom[1]) + SHIFT_INTO_RANGE * (currentRoom[1] + SHIFT_INTO_RANGE);
                int newYcoordinate = roomHeight * (yCoordinate) + SHIFT_INTO_RANGE * (yCoordinate + SHIFT_INTO_RANGE);
                _sourceRectangle.Y = oldYcoordinate + ((newYcoordinate - oldYcoordinate) % (ROOM_TRANSITION_SPEED * 2)) * transitionNumber * ROOM_TRANSITION_SPEED;
            }
            transition++;

            if (transition == 19)
            {
                LoadRoomEnd(xCoordinate, yCoordinate, loadCurrentPosition);
            }
        }

        public void AddLink(Link link)
        {
            this.myLink = link;
            myLink.level = this;
        }

        public void GameState(GameState state)
        {
            this.UpdateGameState = state;
            if (UpdateGameState != Zelda.Enums.GameState.StartMenu)
            {
                myGame.GameState = state;
            }
        }

        public void CollisionManager(CollisionManager _collisionManager)
        {
            collisionManager = _collisionManager;
        }

        public void Game(Game1 _game)
        {
            myGame = _game;
        }

    }
}