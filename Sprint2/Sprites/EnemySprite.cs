﻿using System;
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
        private int xSize;
        private int ySize;
        private int currentFrame;
        private int totalFrames;
        public int spriteSize;
        private double currentDelay = 0.0;

        private const int DELAY_LENGTH = 150;
        private const int TALL_ENEMY_SCALAR = 2;

        public EnemySprite(Texture2D texture, int rows, int columns, int startX, int startY, int xSizeArg, int ySizeArg, EnemyType enemyType)
        {
            Texture = texture;
            Rows = rows;
            Cols = columns;
            xSize = xSizeArg;
            ySize = ySizeArg;
            currentFrame = 0;
            totalFrames = Rows * Cols;
            for (int i = 0; i < totalFrames; i++)
            {
                SourceRectangles.Add(new Rectangle(startX + (i * xSize), startY, xSize, ySize));
            }

            EnemyType = enemyType;
        }

        public void Update(GameTime gameTime)
        {
            currentDelay += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (currentDelay >= (DELAY_LENGTH / 2))
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
                if (currentDelay >= DELAY_LENGTH)
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
            if (EnemyType == EnemyType.Goriya || EnemyType == EnemyType.Zol)
            {
                destinationRectangle = new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize);
            } else
            {
                destinationRectangle = new Rectangle((int)position.X, (int)position.Y, spriteSize, spriteSize * TALL_ENEMY_SCALAR);
            }

            spriteBatch.Draw(Texture, destinationRectangle, SourceRectangles[currentFrame], Color.White);
            
        }

        public void Update(GameTime gameTime, Link link)
        {
            //no-op
        }
    }
}
