using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Controllers;

namespace Sprint0.Sprites
{
    public class EnemySprite : ISprite
    {
        public Texture2D Texture {  get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        private int currentFrame;
        private int totalFrames;

        public EnemySprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Cols = columns;
            currentFrame = 0;
            totalFrames = Rows * Cols;
        }

        public void Update(GameTime gameTime)
        {
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle SourceRectangle = new Rectangle(0, 0, 16, 32);
            Rectangle DestinationRectangle = new Rectangle((int)position.X, (int)position.Y, SourceRectangle.Width, SourceRectangle.Height);

            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Color.White);
            
        }
    }
}
