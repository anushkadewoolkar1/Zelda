using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
    public class LinkSprite : ISprite
    {
        private Texture2D _texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private Vector2 sourceRectangleArea;
        private int colorAdjustment;
        private bool linkDamaged;
        private int simpleClock;

        public LinkSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int[] LinkStates)
        {
            _texture = texture;
            linkDamaged = Convert.ToBoolean(LinkStates[3]);
            if (!linkDamaged)
            {
                simpleClock = 0;
            }

            //Because the spritesheet is each color of link stacked on top of each other, this adds to the yposition so that the colors match 
            colorAdjustment = (LinkStates[0] / 2) * 310;

            switch (LinkStates[1])
            {
                case 1:
                    sourceRectangle = new Rectangle(16, 16, FixDirection(spriteSheetXPos, LinkStates[2]), spriteSheetYPos);
                    break;
                case 2:
                    sourceRectangle = new Rectangle(16, 16, FixDirection(spriteSheetXPos + 17, LinkStates[2]), spriteSheetYPos + colorAdjustment);
                    break;
                case 3:
                    sourceRectangle = new Rectangle(16, 16, FixDirection(spriteSheetXPos + 34, LinkStates[2]), spriteSheetYPos + colorAdjustment);
                    break;
                case 4:
                    sourceRectangle = new Rectangle(16, 16, FixDirection(spriteSheetXPos + 51, LinkStates[2]), spriteSheetYPos + colorAdjustment);
                    break;
                default:
                    break;
            }

            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _position)
        {
            destinationRectangle = new Rectangle((int)_position.X, (int)_position.Y, sourceRectangle.X, sourceRectangle.Y);
            if (!linkDamaged)
            {
                spriteBatch.Draw(_texture, sourceRectangle, destinationRectangle, Color.White);
            } else {
                switch (simpleClock % 3)
                {
                    case 0:
                        spriteBatch.Draw(_texture, sourceRectangle, destinationRectangle, Color.Purple);
                        break;
                    case 1:
                        spriteBatch.Draw(_texture, sourceRectangle, destinationRectangle, Color.Red);
                        break;
                    case 2:
                        spriteBatch.Draw(_texture, sourceRectangle, destinationRectangle, Color.Blue);
                        break;
                    default:
                        break;
                }
                simpleClock++;
            }

        }

        public void Update(GameTime gameTime)
        {
            //no-op
        }

        public void Draw(SpriteBatch _textures)
        {
            //no-op
        }

        private int FixDirection(int xCoordinates, int Direction)
        {
            if (Direction != 1)
            {
                return xCoordinates;
            } else {
                return 742 - xCoordinates + 16;
            }
        }

    }
}
