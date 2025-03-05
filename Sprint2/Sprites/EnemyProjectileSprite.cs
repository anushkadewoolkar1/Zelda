using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;

namespace Sprint0.Sprites
{
    public class EnemyProjectileSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        private List<Rectangle> SourceRectangles = new();
        public ItemType ItemType;
        private int XSize;
        private int YSize;
        private int currentFrame;
        private int totalFrames;
        public int spriteSize;
        private double currentDelay = 0.0;
        private int delay = 150;
        public EnemyProjectileSprite(Texture2D texture, int rows, int columns, int startX, int startY, int xSize, int ySize, ItemType itemType)
        {
            Texture = texture;
            Rows = rows;
            Cols = columns;
            XSize = xSize;
            XSize = ySize;
            currentFrame = 0;
            totalFrames = Rows * Cols;
            for (int i = 0; i < totalFrames; i++)
            {
                SourceRectangles.Add(new Rectangle(startX + (i * xSize), startY, xSize, ySize));
            }

            ItemType = itemType;
        }

        public void Update(GameTime gameTime)
        {
            currentDelay += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (currentDelay >= (delay / 2))
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
                if (currentDelay >= delay)
                {
                    currentDelay = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle destinationRectangle;
            
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize * 2);
            

            spriteBatch.Draw(Texture, destinationRectangle, SourceRectangles[currentFrame], Color.White);

        }

        public void Update(GameTime gameTime, Link link)
        {
            //no-op
        }
    }
}
