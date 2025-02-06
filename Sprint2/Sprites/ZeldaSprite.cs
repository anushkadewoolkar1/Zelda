using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
    public class ZeldaSprite : ISprite
    {
        private Texture2D _texture;
        Rectangle sourceRectangle;

        public ZeldaSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int frame, int state)
        {
            _texture = texture;
            switch (state)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
            switch (frame)
            {
                case 1:
                    sourceRectangle = new Rectangle(16, 16, spriteSheetXPos, spriteSheetYPos);
                    break;
                case 2:
                    sourceRectangle = new Rectangle(16, 16, spriteSheetXPos + 17, spriteSheetYPos);
                    break;
                case 3:
                    sourceRectangle = new Rectangle(16, 16, spriteSheetXPos + 34, spriteSheetYPos);
                    break;
                case 4:
                    sourceRectangle = new Rectangle(16, 16, spriteSheetXPos + 51, spriteSheetYPos);
                    break;
                default:
                    break;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Fill
        }

        public void Update(GameTime gameTime)
        {
            // no-op
        }

    }
}
