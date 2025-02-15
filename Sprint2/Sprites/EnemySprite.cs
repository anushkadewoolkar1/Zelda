using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Controllers;
using Zelda.Enums;

namespace Sprint0.Sprites
{
    public class EnemySprite : ISprite
    {
        public Texture2D Texture {  get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        private List<Rectangle> SourceRectangles = new();
        public EnemyType EnemyType;
        private int XSize;
        private int YSize;
        private int currentFrame;
        private int totalFrames;
        public int spriteSize;
        private double CurrentDelay = 0.0;
        private int delay = 150;

        public EnemySprite(Texture2D texture, int rows, int columns, int startX, int startY, int xSize, int ySize, EnemyType enemyType)
        {
            Texture = texture;
            Rows = rows;
            Cols = columns;
            XSize = xSize;
            YSize = ySize;
            currentFrame = 0;
            totalFrames = Rows * Cols;
            for (int i = 0; i < totalFrames; i++)
            {
                SourceRectangles.Add(new Rectangle(startX + (i * XSize), startY, XSize, YSize));
            }

            EnemyType = enemyType;
        }

        public void Update(GameTime gameTime)
        {
            CurrentDelay += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (CurrentDelay >= (delay / 2))
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
                if (CurrentDelay >= delay)
                {
                    CurrentDelay = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle DestinationRectangle;
            if (EnemyType == EnemyType.Goriya || EnemyType == EnemyType.Zol)
            {
                DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize);
            } else
            {
                DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize * 2);
            }

            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangles[currentFrame], Color.White);
            
        }
    }
}
