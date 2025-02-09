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
        private int damageClock;

        public LinkSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int[] LinkStates)
        {
            _texture = texture;

            //Sets clock to 0 when Link isn't damaged 
            linkDamaged = Convert.ToBoolean(LinkStates[3]);
            if (!linkDamaged)
            {
                damageClock = 0;
            }

            int[] sourceDimensions = AdjustAttacks(spriteSheetXPos, spriteSheetYPos, LinkStates[0], LinkStates[1]);

            //Checks if Link is facing Down or Sideways and if Link has the Magical Shield
            if (LinkStates[2] < 3 && (LinkStates[0] % 2) == 1)
            {
                sourceDimensions = MagicalShield(sourceDimensions, LinkStates[1], LinkStates[2]);
            }
            
            //Because the spritesheet is each color of link stacked on top of each other, this adds to the yposition so that the colors match 
            colorAdjustment = (LinkStates[0] / 2) * 310;

            switch (LinkStates[1])
            {
                case 1:
                    sourceRectangle = new Rectangle(sourceDimensions[2], sourceDimensions[3],
                        FixDirection(sourceDimensions[0], LinkStates[2]), sourceDimensions[1]);
                    break;
                case 2:
                    sourceRectangle = new Rectangle(sourceDimensions[2], sourceDimensions[3],
                        FixDirection(sourceDimensions[0] + 17, LinkStates[2]), sourceDimensions[1] + colorAdjustment);
                    break;
                case 3:
                    sourceRectangle = new Rectangle(sourceDimensions[2], sourceDimensions[3],
                        FixDirection(sourceDimensions[0] + 34, LinkStates[2]), sourceDimensions[1] + colorAdjustment);
                    break;
                case 4:
                    sourceRectangle = new Rectangle(sourceDimensions[2], sourceDimensions[3],
                        FixDirection(sourceDimensions[0] + 51, LinkStates[2]), sourceDimensions[1] + colorAdjustment);
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
                switch (damageClock % 3)
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
                damageClock++;
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

        private int[] AdjustAttacks(int xCoordinate, int yCoordinate, int frame, int facingDirection)
        {
            int[] sourceDimensions = {xCoordinate, yCoordinate, 16, 16};

            if (yCoordinate > 46)
            {
                return sourceDimensions;
            }

            if (facingDirection == 0)
            {
                switch (frame)
                {
                    case 2:
                        sourceDimensions[3] = 27;
                        break;
                    case 3:
                        sourceDimensions[3] = 23;
                        break;
                    case 4:
                        sourceDimensions[3] = 19;
                        break;
                    default:
                        break;
                }    
            } else if (facingDirection == 3)
            {
                switch (frame)
                {
                    case 2:
                        sourceDimensions[3] = 27;
                        break;
                    case 3:
                        sourceDimensions[3] = 23;
                        sourceDimensions[1] = yCoordinate - (27 - 17);
                        break;
                    case 4:
                        sourceDimensions[3] = 19;
                        sourceDimensions[1] = yCoordinate - (27 - 17);
                        break;
                    default:
                        break;
                }
            } else
            {
                switch (frame)
                {
                    case 2:
                        sourceDimensions[2] = 28;
                        break;
                    case 3:
                        sourceDimensions[2] = 23;
                        sourceDimensions[0] = xCoordinate + 12;
                        break;
                    case 4:
                        sourceDimensions[3] = 19;
                        sourceDimensions[0] = xCoordinate + 12 + 8;
                        break;
                    default:
                        break;
                }
            }
            return sourceDimensions;
        }

        private int[] MagicalShield(int[] sourceDimensions, int facingDirection, int frame)
        {
            switch (sourceDimensions[2])
            {
                case 1:
                    sourceDimensions[0] += 288;
                    break;
                case 47:
                    sourceDimensions[1] += 81;
                    sourceDimensions[0] -= 35;
                    break;
                case 77:
                    sourceDimensions[1] += 51;
                    if (frame == 3)
                    {
                        sourceDimensions[0] -= 10;
                    } else if (frame == 4)
                    {
                        sourceDimensions[0] -= 11;
                    }
                    break;
                default:
                    break;
            }
            return sourceDimensions;
        }
    }
}
