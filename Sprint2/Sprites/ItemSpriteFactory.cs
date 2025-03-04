using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using System.IO;

namespace SpriteFactory
{
    public class ItemSpriteFactory
    {
        private Texture2D itemSpriteSheet;
        //private Dictionary<string, Rectangle> spriteRectangles = new Dictionary<string, Rectangle>();
        private Dictionary<string, int>spriteRectangles = new Dictionary<string, int>();
        private Dictionary<int, string> spriteIndices;
        private int currentIdx = 0;
        List<Rectangle> spriteFrames;

        private const int LINES_PER_BLOCK = 5;

        private static ItemSpriteFactory instance = new ItemSpriteFactory();

        public static ItemSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemSpriteFactory()
        {
        }

        public void ItemTextures(ContentManager Content)
        {
            itemSpriteSheet = Content.Load<Texture2D>("ItemSpritesheet");
            LoadSpriteData("../../../Content/ItemSpriteData.txt");
            // the line below is so it compiles on my computer pls dont delete yet
            //LoadSpriteData("../../../Zelda/Sprint2/Content/ItemSpriteData.txt");
        }

        private void LoadSpriteData(string FilePath)
        {
            spriteFrames = new List<Rectangle>();  // keeps the order of frames
            spriteIndices = new Dictionary<int, string>();
            spriteRectangles = new Dictionary<string, int>();

            foreach (var line in File.ReadLines(FilePath))
            {
                var nums = line.Split(',');
                if (nums.Length == LINES_PER_BLOCK)  
                {
                    string name = nums[0];
                    int x = int.Parse(nums[1]);
                    int y = int.Parse(nums[2]);
                    int width = int.Parse(nums[3]);
                    int height = int.Parse(nums[4]);

                    Rectangle rect = new Rectangle(x, y, width, height);

                    int index = spriteFrames.Count; 
                    spriteFrames.Add(rect);
                    spriteRectangles[name] = index;
                    spriteIndices[index] = name;
                }
            }
        }

        public Rectangle ItemCycleLeftFactory()
        {
            currentIdx = (currentIdx - 1 + spriteFrames.Count) % spriteFrames.Count;
            Rectangle rectangle = spriteFrames[currentIdx];
            return rectangle;
        }

        public Rectangle ItemCycleRightFactory()
        {
            currentIdx = (currentIdx + 1) % spriteFrames.Count;
            Rectangle rectangle = spriteFrames[currentIdx];
            return rectangle;
        }

        public String GetItemStringFromIdx()
        {
            return spriteIndices[currentIdx];
        }

        public Rectangle FetchItemSourceFromString(string spriteName)
        {
            if (spriteRectangles.TryGetValue(spriteName, out int index))
            {
                Rectangle rectangle = spriteFrames[index];
                return rectangle;
            }
            else
            {
                throw new ArgumentException($"Sprite '{spriteName}' not found in the data txt file.");
            }
        }

        public Texture2D GetTexture()
        {
            return itemSpriteSheet;
        }
        /*
        public ItemSprite FetchItemSprite(string spriteName)
        {
            if (spriteRectangles.TryGetValue(spriteName, out int index))
            {
                Rectangle rectangle = spriteFrames[index];

                return new ItemSprite(spriteName);
            }
            else
            {
                throw new ArgumentException($"Sprite '{spriteName}' not found in the data txt file.");
            }
        }
        */

        
    }
}
