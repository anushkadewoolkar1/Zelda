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
        private Dictionary<string, Rectangle> spriteRectangles = new Dictionary<string, Rectangle>();
        private int currentIdx = 0;
        List<Rectangle> spriteFrames;

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
            LoadSpriteData("../Content/ItemSpriteData.txt");
            // the line below is so it compiles on my computer pls dont delete yet
            //LoadSpriteData("../../../Zelda/Sprint2/Content/ItemSpriteData.txt");
        }

        private void LoadSpriteData(string FilePath)
        {

            spriteRectangles = new Dictionary<string, Rectangle>();

            foreach (var line in File.ReadLines(FilePath))
            {

                var nums = line.Split(',');
                if (nums.Length == 5)      // it should never not be 5
                {
                    string name = nums[0];
                    int x = int.Parse(nums[1]);
                    int y = int.Parse(nums[2]);
                    int width = int.Parse(nums[3]);
                    int height = int.Parse(nums[4]);

                    spriteRectangles.Add(name, new Rectangle(x, y, width, height));
                }
            }
            spriteFrames = spriteRectangles.Values.ToList();
        }

        public Rectangle itemCycleLeftFactory()
        {
            currentIdx = (currentIdx - 1 + spriteFrames.Count) % spriteFrames.Count;
            Rectangle rectangle = spriteFrames[currentIdx];
            return rectangle;
        }

        public Rectangle itemCycleRightFactory()
        {
            currentIdx = (currentIdx + 1) % spriteFrames.Count;
            Rectangle rectangle = spriteFrames[currentIdx];
            return rectangle;
        }

        public ItemSprite FetchItemSprite(string spriteName)
        {
            // the second parameter is where the rectangle will be stored if it can find the key in spriteRectangles (C# out means output)
            if (spriteRectangles.TryGetValue(spriteName, out Rectangle rectangle)) 
            {
                return new ItemSprite(itemSpriteSheet, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            }

            else
            {
                throw new ArgumentException($"sprite '{spriteName}' not found in the data txt file.");
            }
        }

        
    }
}
