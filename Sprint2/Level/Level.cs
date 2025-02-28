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

namespace Sprint0.ILevel
{
    public class Level : ILevel
    {

        private int roomWidth;
        private int roomHeight;
        private Texture2D _backgroundTexture;
        private Rectangle _sourceRectangle;

        private int roomLength = 14;
        private List<String> Objects = new List<String>();
        private List<Block> blocksList = new List<Block>();
        private List<Enemy> enemiesList = new List<Enemy>();
        private List<Item> itemsList = new List<Item>();
        private Vector2 roomDimensions;
        private int enemiesListIndex = 0;
        private int blocksListIndex = 0;
        private int itemsListIndex = 0;
        private ContentManager contentManager;


        

        /* TO DO:
         * 1. DONE - Create collections for Enemies, Blocks, Items, etc. for loading
         * 2. Implement Load Room to invoke constructor calls for each object
         * 3. DONE - Implement Draw and Read (Iterate through each of the collections)
         * 4. DONE (MAYBE REIMPLEMENT) - Optimize Code with correct Room size information
         * 5. Populate txt file with two rooms for Functionality check
         * 6. BASICALLY DONE - Implement the Level Background Correctly
         */


        public Level(ContentManager Content)
        {
            this.contentManager = Content;
            _backgroundTexture = Content.Load<Texture2D>("Levels Spritesheet");
            roomWidth = (_backgroundTexture.Width - 5) / 6;
            roomHeight = (_backgroundTexture.Height - 5) / 6;
            _sourceRectangle = new Rectangle(
                roomWidth * 2 + 1, roomHeight * 5 + 1, roomWidth, roomHeight);
            roomDimensions = new Vector2(roomWidth * 2 - 64, roomHeight * 2 - 64);

            foreach (var line in File.ReadLines("../../../Content/LevelFile.txt"))
            {
                var nums = line.Split(',');
                for (int i = 0; i < nums.Length; i++)
                {
                    Objects.Add(nums[i]);
                }
            }
        }

        private Block CreateBlock(string blockType, Vector2 position, Texture2D[] textures, int targetX = 0, int targetY = 0)
        {
            switch (blockType)
            {
                case "InvisibleBlock":
                    return new InvisibleBlock(position, textures);
                case "LoadRoomBlock":
                    return new LoadRoomBlock(position, textures, this, targetX, targetY);
                default: // Default to a normal solid block
                    return new Block(position, textures);
            }
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
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0,
               roomWidth * 2, roomHeight * 2), _sourceRectangle, Color.White);

            for (int i = 0; i < enemiesListIndex;i++)
            {
                enemiesList[i].DrawCurrentSprite(spriteBatch);
            }
            for (int i = 0; i < blocksListIndex;i++)
            {
                blocksList[i].Draw(spriteBatch);
            }
            for (int i = 0; i < itemsListIndex;i++)
            {
                itemsList[i].Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < enemiesListIndex; i++)
            {
                enemiesList[i].Update(gameTime);
            }
            for (int i = 0; i < blocksListIndex; i++)
            {
                blocksList[i].Update();
            }
        }

        public void LoadRoom(int xCoordinate, int yCoordinate)
        {
            _sourceRectangle = new Rectangle(
                roomWidth * (xCoordinate) + 1 *(xCoordinate + 1), roomHeight * (yCoordinate) + 1*(yCoordinate  + 1),
                roomWidth, roomHeight);

            int count = Objects.Count;
            int i = 1;
            Boolean foundRoom = false;

            while (i < count && !foundRoom)
            {
                if (xCoordinate.ToString() == Objects[i - 1] && yCoordinate.ToString() == Objects[i])
                {
                    foundRoom = true;
                }
                i++;
            }
            if (i == count || !foundRoom)
            {
                System.Diagnostics.Debug.WriteLine("Failed to Find Room");
                return;
            }
            i += 12;
            int room = i + 14 * 9;
            //MethodInfo mi;
            //String newConstructor = "new ";
            enemiesListIndex = 0;
            blocksListIndex = 0;

            EnemyType enemyType;

            //We'll need to be able to pass position for creating the objects of the entities
            int hold = i;

            while (i < room)
            {
                if (Objects[i].Contains("Enemy"))
                {
                    enemiesList.Add(new Enemy());
                    enemiesList[enemiesListIndex].position =
                        new Vector2((roomDimensions.X / roomLength) * ((i - hold) % roomLength) + 36,
                        (roomDimensions.Y / 9) * ((i - hold) / roomLength) + 36);
                    Enum.TryParse(Objects[i].Substring(5).ToString(), out enemyType);
                    enemiesList[enemiesListIndex].CreateEnemy(enemyType);
                    enemiesListIndex++;
                    System.Diagnostics.Debug.WriteLine(((i - hold) / roomLength).ToString());
                }
                else if (Objects[i].Contains("Block"))
                {
                    Vector2 position = new Vector2(
                        (roomDimensions.X / roomLength) * ((i - hold) % roomLength) + 36,
                        (roomDimensions.Y / 9) * ((i - hold) / roomLength) + 36
                    );

                    Texture2D[] blockTextures = LoadBlockTextures(); 

                    string blockType = Objects[i]; // Get the block type from the level file

                    Block newBlock = CreateBlock(blockType, position, blockTextures); 
                    blocksList.Add(newBlock);
                    blocksListIndex++;

                    System.Diagnostics.Debug.WriteLine($"Created {blockType} at {position}");
                    //blocksList = new Block(0, 0);
                    //mi = blocksList[blocksListIndex].GetType().GetMethod(newConstructor + Objects[i].Substring(5));
                    //mi.Invoke(blocksList[blocksListIndex], null);
                }
                else if (Objects[i].Contains("Link"))
                {
                    //This is a Link.cs object = [i % roomLength,i / roomLength];
                } else if (Objects[i].Contains(""))
                {

                }
                i++;
            }
        }
    }
}