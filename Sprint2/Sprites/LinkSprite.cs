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

        private int leftAdjustment;
        private int upAdjustment;
        private int linkScale = 2;

        public LinkSprite(Texture2D texture, int spriteSheetXPos, int spriteSheetYPos, int[] LinkStates)
        {
            _texture = texture;


            //Sets clock to 0 when Link isn't damaged 
            linkDamaged = Convert.ToBoolean(LinkStates[3]);
            if (!linkDamaged)
            {
                damageClock = 0;
            }

            //Used to adjust the sprites that shift Link's body
            leftAdjustment = 0;
            upAdjustment = 0;

            //Returns initial rectangle containing appropriate coordinates and dimensions without Magical Shield
            int[] sourceRectangleDimensions = AdjustAttacks(spriteSheetXPos, spriteSheetYPos, LinkStates[1], LinkStates[0]);

            if (LinkStates[0] < 3 && (LinkStates[2] % 2) == 1)
            {
                sourceRectangleDimensions = MagicalShield(sourceRectangleDimensions, LinkStates[0], LinkStates[1]);
            }

            //Because the spritesheet is each color of link stacked on top of each other, this adds to the yposition so that the colors match 
            colorAdjustment = (LinkStates[2] / 2) * 310;


            //Changes Link's Sprite depending on frame of animation
            switch (LinkStates[1])
            {
                case 1:
                    sourceRectangle = new Rectangle(FixDirection(sourceRectangleDimensions[0], LinkStates[0], sourceRectangleDimensions[2]), sourceRectangleDimensions[1] + colorAdjustment,
                        sourceRectangleDimensions[2], sourceRectangleDimensions[3]);
                    break;
                case 2:
                    sourceRectangle = new Rectangle(FixDirection(sourceRectangleDimensions[0] + 17, LinkStates[0], sourceRectangleDimensions[2]), sourceRectangleDimensions[1] + colorAdjustment,
                        sourceRectangleDimensions[2], sourceRectangleDimensions[3]);
                    break;
                case 3:
                    sourceRectangle = new Rectangle(FixDirection(sourceRectangleDimensions[0] + 34, LinkStates[0], sourceRectangleDimensions[2]), sourceRectangleDimensions[1] + colorAdjustment,
                        sourceRectangleDimensions[2], sourceRectangleDimensions[3]);
                    break;
                case 4:
                    sourceRectangle = new Rectangle(FixDirection(sourceRectangleDimensions[0] + 51, LinkStates[0], sourceRectangleDimensions[2]), sourceRectangleDimensions[1] + colorAdjustment,
                        sourceRectangleDimensions[2], sourceRectangleDimensions[3]);
                    break;
                default:
                    break;
            }
            Console.WriteLine("fhesjkgbseghjskdvhskbhkisgjsh");


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _position)
        {
            destinationRectangle = new Rectangle((int)_position.X - (leftAdjustment * linkScale), (int)_position.Y - (upAdjustment * linkScale),
                            sourceRectangle.Width * linkScale, sourceRectangle.Height * linkScale);
            if (!linkDamaged)
            {
                spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.White);
            }
            else
            {
                switch ((damageClock / 3) % 3)
                {
                    case 0:
                        spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.Purple);
                        break;
                    case 1:
                        spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.Red);
                        break;
                    case 2:
                        spriteBatch.Draw(_texture, destinationRectangle, sourceRectangle, Color.Blue);
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

        private int FixDirection(int xCoordinates, int Direction, int sourceRectangleWidth)
        {
            if (Direction != 1)
            {
                return xCoordinates;
            }
            else
            {
                return 742 - xCoordinates - sourceRectangleWidth;
            }
        }

        private int[] AdjustAttacks(int xCoordinate, int yCoordinate, int frame, int facingDirection)
        {
            int[] sourceRectangleDimensions = { xCoordinate, yCoordinate, 16, 16 };



            if (yCoordinate < 45)
            {
                return sourceRectangleDimensions;
            }



            if (facingDirection == 0)
            {
                switch (frame)
                {
                    case 2:
                        sourceRectangleDimensions[3] = 27;
                        break;
                    case 3:
                        sourceRectangleDimensions[3] = 23;
                        break;
                    case 4:
                        sourceRectangleDimensions[3] = 19;
                        break;
                    default:
                        break;
                }
            }
            else if (facingDirection == 3)
                {
                    switch (frame)
                    {
                        case 2:
                            upAdjustment = 12;
                            sourceRectangleDimensions[3] = 28;
                            sourceRectangleDimensions[1] = yCoordinate - 11;
                            break;
                        case 3:
                            upAdjustment = 11;
                            sourceRectangleDimensions[3] = 27;
                            sourceRectangleDimensions[1] = yCoordinate - 10;
                            break;
                        case 4:
                            upAdjustment = 3;
                            sourceRectangleDimensions[3] = 19;
                            sourceRectangleDimensions[1] = yCoordinate - 2;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (frame)
                    {
                        case 2:
                            if (facingDirection == 1)
                            {
                                leftAdjustment = 12;
                            }
                            sourceRectangleDimensions[2] = 27;
                            break;
                        case 3:
                            if (facingDirection == 1)
                            {
                                leftAdjustment = 7;
                            }
                            sourceRectangleDimensions[2] = 23;
                            sourceRectangleDimensions[0] = xCoordinate + 11;
                            break;
                        case 4:
                            if (facingDirection == 1)
                            {
                                leftAdjustment = 3;
                            }
                            sourceRectangleDimensions[2] = 19;
                            sourceRectangleDimensions[0] = xCoordinate + 11 + 7;
                            break;
                        default:
                            break;
                    }
                }

            return sourceRectangleDimensions;
        }

        private int[] MagicalShield(int[] sourceRectangleDimensions, int facingDirection, int frame)
        {
            switch (sourceRectangleDimensions[1])
            {
                case 1:
                    sourceRectangleDimensions[0] += 288;
                    break;
                case 47:
                    if (frame == 3 || frame == 4)
                    {
                        sourceRectangleDimensions[1] += 81;
                        sourceRectangleDimensions[0] -= 34;
                    }
                    break;
                case 77:
                    if (frame == 3)
                    {
                        sourceRectangleDimensions[1] += 52;
                        sourceRectangleDimensions[0] -= 11;
                    }
                    if (frame == 4)
                    {
                        sourceRectangleDimensions[1] += 50;
                        sourceRectangleDimensions[0] -= 11;
                    }
                    break;
                default:
                    break;

            }

            return sourceRectangleDimensions;
        }
    }
}
