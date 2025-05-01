using Microsoft.Xna.Framework;

namespace MainGame.Sprites
{
    internal static class LinkAttackHelper
    {
        // Adjusts source rectangle dimensions when Link is attacking
        public static int[] AdjustAttacks(int xCoordinate, int yCoordinate, int frame, int facingDirection, out int leftAdjustment, out int upAdjustment)
        {
            leftAdjustment = 0;
            upAdjustment = 0;
            int[] sourceRectangleDimensions = { xCoordinate, yCoordinate, LinkSprite.RECTANGLE_DIM, LinkSprite.RECTANGLE_DIM };

            // Exits if Link is not attacking
            if (yCoordinate < 45)
            {
                return sourceRectangleDimensions;
            }

            if (facingDirection == (int)LinkSprite.LinkSpriteDirection.Down)
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
            else if (facingDirection == (int)LinkSprite.LinkSpriteDirection.Up)
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
                        if (facingDirection == (int)LinkSprite.LinkSpriteDirection.Left)
                        {
                            leftAdjustment = 12;
                        }
                        sourceRectangleDimensions[2] = 27;
                        break;
                    case 3:
                        if (facingDirection == (int)LinkSprite.LinkSpriteDirection.Left)
                        {
                            leftAdjustment = 7;
                        }
                        sourceRectangleDimensions[2] = 23;
                        sourceRectangleDimensions[0] = xCoordinate + 11;
                        break;
                    case 4:
                        if (facingDirection == (int)LinkSprite.LinkSpriteDirection.Left)
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

        // Adjusts source rectangle dimensions when Magical Shield is equipped
        public static int[] MagicalShield(int[] sourceRectangleDimensions, int frame)
        {
            switch (sourceRectangleDimensions[1])
            {
                // This is checking for the case where Link is Walking
                case 1:
                    sourceRectangleDimensions[0] += 288;
                    break;

                // This checks for the case when Link is attacking down
                case 47:
                    if (frame == 3 || frame == 4)
                    {
                        sourceRectangleDimensions[1] += 81;
                        sourceRectangleDimensions[0] -= 34;
                    }
                    break;

                // This checks for the case when Link is attacking sideways
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
