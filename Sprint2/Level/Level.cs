using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Sprint0.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Sprint0.Sprites;
using Zelda.Enums;
using Sprint0.States;

namespace Sprint0.Level
{
    public class Level : ILevel
    {

        private int room = 25;
        private int roomLength = 5;
        private List<String> Objects = new List<String>();
        private List<Block> blocksList = new List<Block>();
        private List<Enemy> enemiesList = new List<Enemy>();
        int enemiesListIndex = 0;
        int blocksListIndex = 0;

        /* TO DO:
         * 1. Create collections for Enemies, Blocks, Items, etc. for loading
         * 2. Implement Load Room to invoke contructor calls for each object
         * 3. Implement Draw and Read (Iterate through each of the collections)
         * 4. Optimize Code with correct Room size information
         * 5. Populate txt file with two rooms for Functionality check
         */


        public Level()
        {
            /*
            foreach (var line in File.ReadLines("../LevelFile.txt"))
            {
                var nums = line.Split(',');
                for (int i = 0; i < nums.Length; i++)
                {
                    Objects.Add(nums[i]);
                }
            }
            */



            
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemiesListIndex;i++)
            {
                enemiesList[i].DrawCurrentSprite(spriteBatch);
            }
            for (int i = 0; i < blocksListIndex;i++)
            {
                blocksList[i].Draw(spriteBatch);
            }
        }

        public void Update(GameTime gametime)
        {
            for (int i = 0; i < enemiesListIndex; i++)
            {
                enemiesList[i].Update(gametime);
            }
            for (int i = 0; i < blocksListIndex; i++)
            {
                blocksList[i].Update();
            }
        }

        public void LoadRoom(int xCoordinate, int yCoordinate)
        {
            int count = Objects.Count;
            int i = 1;
            Boolean foundRoom = false;

            while (i < count || foundRoom)
            {
                if (xCoordinate.ToString() == Objects[i - 1] && yCoordinate.ToString() == Objects[i])
                {
                    foundRoom = true;
                }
                i++;
            }
            i += 10;
            int room = i + 50;
            MethodInfo mi;
            String newConstructor = "new ";
            enemiesListIndex = 0;
            blocksListIndex = 0;

            //We'll need to be able to pass position for creating the objects of the entities

            while (i < room)
            {
                if (Objects[i].Contains("Enemy"))
                {
                    mi = enemiesList[enemiesListIndex].GetType().GetMethod(newConstructor + Objects[i].Substring(4));
                    mi.Invoke(enemiesList[enemiesListIndex], null);
                }
                else if (Objects[i].Contains("Block"))
                {
                    mi = blocksList[blocksListIndex].GetType().GetMethod(newConstructor + Objects[i].Substring(4));
                    mi.Invoke(blocksList[blocksListIndex], null);
                }
                else if (Objects[i].Contains("LinkC"))
                {
                    //This is a Link.cs object = [i % roomLength,i / roomLength];
                }
            }
        }
    }
}