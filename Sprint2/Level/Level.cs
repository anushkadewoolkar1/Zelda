using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Sprint0.ILevel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Sprint0.Sprites;
using Zelda.Enums;
using Sprint0.States;
using System.Reflection.Metadata;
using Sprint0.CollisionHandling;
using System.Reflection.Emit;

namespace Sprint0.ILevel
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

        public bool doubleClickTemp { get; set; }

        private ContentManager contentManager;

        private List<IGameObject> gameObjects;


        private const int ROOM_LENGTH = 14;
        private const int LEVEL_CENTER_POSITION = 60;
        private const int WIDTH_POSITION_SCALAR = 2;
        private const int HEIGHT_POSITION_SCALAR = 5;
        private const int LEVEL_TEXTURE_SCALAR = 6;

        public Level(ContentManager Content, List<IGameObject> _gameObjects)
        {
            //Creates new collection of Blocks, Enemy and Items to be loaded into levels
            blocksList = new List<Block>();
            enemiesList = new List<Enemy>();
            itemsList = new List<Item>();

            //Creates the background room dimensions as well as loading the entire level texture
            this.contentManager = Content;
            _backgroundTexture = Content.Load<Texture2D>("Levels Spritesheet");
            roomWidth = (_backgroundTexture.Width - 5) / LEVEL_TEXTURE_SCALAR;
            roomHeight = (_backgroundTexture.Height - 5) / LEVEL_TEXTURE_SCALAR;
            _sourceRectangle = new Rectangle(
                roomWidth * WIDTH_POSITION_SCALAR + 1, roomHeight * HEIGHT_POSITION_SCALAR + 1, roomWidth, roomHeight);
            roomDimensions = new Vector2(roomWidth * WIDTH_POSITION_SCALAR - LEVEL_CENTER_POSITION, roomHeight * WIDTH_POSITION_SCALAR - LEVEL_CENTER_POSITION);

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
            currentRoom = [2, 5];
        }


        //Checks for which type of block to create
        private Block CreateBlock(string blockType, Vector2 position, Texture2D[] textures, int targetX = 0, int targetY = 0)
        {
            if (blockType.Contains("invisible"))
            {
                return new InvisibleBlock(position, textures);
            }
            else if (blockType.Contains("load"))
            {
                //swap in Int32.Parse(blockType.Substring(13,1)), Int32.Parse(blockType.Substring(15, 1)
                //when you are done checking
                return new LoadRoomBlock(position, textures, this, targetX, targetY);
            } else
            {
                return new Block(position, textures);
            }
            /*
            switch (blockType)
            {
                case "InvisibleBlock":
                    return new InvisibleBlock(position, textures);
                case "LoadRoomBlock":
                    return new LoadRoomBlock(position, textures, this, targetX, targetY);
                default: // Default to a normal solid block
                    
            }
            */
        }


        private Texture2D[] LoadBlockTextures()
        {
            return new Texture2D[]
            {
                contentManager.Load<Texture2D>("transparent_block") // Change filename as needed
            };
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws room background
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0,
               roomWidth * 2, roomHeight * 2), _sourceRectangle, Color.White);

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

        public void Update(GameTime gameTime)
        {
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
        }

        public void LoadRoom(int xCoordinate, int yCoordinate)
        {
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
                
                if (xCoordinate.ToString() == Objects[currentPosition - 1] && yCoordinate.ToString() == Objects[currentPosition])
                {
                    foundRoom = true;
                }
                currentPosition++;
            }

            //Avoids loading room when room is not found
            if (currentPosition == count || !foundRoom)
            {
                System.Diagnostics.Debug.WriteLine("Failed to Find Room");
                return;
            }

            _sourceRectangle = new Rectangle(
                roomWidth * (xCoordinate) + 1 * (xCoordinate + 1), roomHeight * (yCoordinate) + 1 * (yCoordinate + 1),
                roomWidth, roomHeight);


            //Removes object from various lists
            RemoveOldObjects();

            //Get's to the start of the room and sets room size
            currentPosition += 12;
            int room = currentPosition + ROOM_LENGTH * 9;
            
            

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
                } else if (Objects[currentPosition].Contains("Item"))
                //Constructs Items
                {
                    CreateItem(currentPosition , startOfRoom);
                }
                currentPosition++;
            }
            currentRoom = [xCoordinate, yCoordinate];
        }

        private void CreateEnemy(int currentPosition, int startOfRoom)
        {
            EnemyType enemyType;
            enemiesList.Add(new Enemy());
            Enum.TryParse(Objects[currentPosition].Substring(6).ToString(), out enemyType);
            enemiesList[enemiesList.Count - 1].CreateEnemy(enemyType,
                new Vector2(((currentPosition - startOfRoom) % ROOM_LENGTH) - 1,
                ((currentPosition - startOfRoom) / ROOM_LENGTH) - 1));
            gameObjects.Add(enemiesList[enemiesList.Count - 1]);
        }

        private void CreateBlock(int currentPosition, int startOfRoom)
        {
            Vector2 position = new Vector2(
                        (roomDimensions.X / ROOM_LENGTH) * ((currentPosition - startOfRoom) % ROOM_LENGTH) + 32,
                        (roomDimensions.Y / 9) * ((currentPosition - startOfRoom) / ROOM_LENGTH) + 32
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
            Enum.TryParse(Objects[currentPosition].Substring(5), out itemType);
            itemsList[itemsList.Count - 1] = itemsList[itemsListIndex].CreateItem(itemType,
                ((currentPosition - startOfRoom) % ROOM_LENGTH) - 1,
                ((currentPosition - startOfRoom) / ROOM_LENGTH) - 1);
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
                return new Rectangle(LEVEL_CENTER_POSITION, LEVEL_CENTER_POSITION, (int)roomDimensions.X - LEVEL_CENTER_POSITION, (int)roomDimensions.Y - LEVEL_CENTER_POSITION);
            }
        }
        public Vector2 Velocity
        {
            get { return new Vector2(0, 0); }
        }

        public void Destroy()
        {

        }
    }
}