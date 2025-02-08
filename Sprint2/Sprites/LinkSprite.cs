using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
    public class LinkSprite : ISprite
    {
        private Texture2D _texture;
        private Rectangle  sourceRectangle;
        int colorAdjustment;
        bool linkDamaged;

        public LinkSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int[] LinkStates)
        {
            _texture = texture;

            //Because the spritesheet is each color of link stacked on top of each other, this adds to the yposition so that the colors match 
            colorAdjustment = (LinkStates[0] / 2) * 310;

            switch (LinkStates[1])
            {
                case 1:
                    sourceRectangle = new Rectangle(16, 16, spriteSheetXPos, spriteSheetYPos);
                    break;
                case 2:
                    sourceRectangle = new Rectangle(16, 16, spriteSheetXPos + 17, spriteSheetYPos + colorAdjustment);
                    break;
                case 3:
                    sourceRectangle = new Rectangle(16, 16, spriteSheetXPos + 34, spriteSheetYPos + colorAdjustment);
                    break;
                case 4:
                    sourceRectangle = new Rectangle(16, 16, spriteSheetXPos + 51, spriteSheetYPos + colorAdjustment);
                    break;
                default:
                    break;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _position)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            // no-op
        }

    }
}
