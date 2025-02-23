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

namespace Sprint0.Level
{
    public class Level : ILevel
    {

        private int room = 15;
        private List<String> Objects = new List<String>();

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

        }

        public void Update(GameTime gametime)
        {
            
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

            while (i < room)
            {
                if (Objects[i].Contains("Enem"))
                {
                    //Enemy = Enemy + ""
                }
                else if (Objects[i].Contains("Bloc"))
                {

                    //Blocks = Blocks + ""
                }
                else if (Objects[i].Contains("Link"))
                {
                    //Get the method information using the method info class
                    //LinkSprite hello = new LinkSprite();
                    //mi = hello.GetType().GetMethod(Objects[i].Substring(4));


                    //Invoke the method
                    // (null- no parameter for the method call
                    // or you can pass the array of parameters...)
                    //mi.Invoke(this, null);
                }
            }
        }
    }
}