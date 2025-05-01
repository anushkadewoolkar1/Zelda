using Microsoft.Xna.Framework;

namespace MainGame.Sprites
{
    internal static class LinkAttackHelper
    {
        //Link Attack Constants
        private const int DOWN_ATTACK_FRAME_TWO = 27;
        private const int DOWN_ATTACK_FRAME_THREE = 23;
        private const int DOWN_ATTACK_FRAME_FOUR = 19;
        private const int UP_ATTACK_FRAME_TWO = 28;
        private const int UP_ATTACK_FRAME_THREE = 27;
        private const int UP_ATTACK_FRAME_FOUR = 19;
        private const int SIDE_ATTACK_FRAME_TWO = 27;
        private const int SIDE_ATTACK_FRAME_THREE = 23;
        private const int SIDE_ATTACK_FRAME_FOUR = 19;
        private const int UP_ATTACK_ADJUSTMENT_FRAME_TWO = 11;
        private const int UP_ATTACK_ADJUSTMENT_FRAME_THREE = 10;
        private const int UP_ATTACK_ADJUSTMENT_FRAME_FOUR = 2;
        private const int SIDE_ATTACK_ADJUSTMENT_FRAME_THREE = 11;
        private const int SIDE_ATTACK_ADJUSTMENT_FRAME_FOUR = 18;
        private const int VERTICAL_ATTACK_ADJUSTMENT_FRAME_TWO = 12;
        private const int VERTICAL_ATTACK_ADJUSTMENT_FRAME_THREE = 11;
        private const int VERTICAL_ATTACK_ADJUSTMENT_FRAME_FOUR = 3;
        private const int HORIZONTAL_ATTACK_ADJUSTMENT_FRAME_TWO = 12;
        private const int HORIZONTAL_ATTACK_ADJUSTMENT_FRAME_THREE = 7;
        private const int HORIZONTAL_ATTACK_ADJUSTMENT_FRAME_FOUR = 3;

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
                        sourceRectangleDimensions[3] = DOWN_ATTACK_FRAME_TWO;
                        break;
                    case 3:
                        sourceRectangleDimensions[3] = DOWN_ATTACK_FRAME_THREE;
                        break;
                    case 4:
                        sourceRectangleDimensions[3] = DOWN_ATTACK_FRAME_FOUR;
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
                        upAdjustment = VERTICAL_ATTACK_ADJUSTMENT_FRAME_TWO;
                        sourceRectangleDimensions[3] = UP_ATTACK_FRAME_TWO;
                        sourceRectangleDimensions[1] = yCoordinate - UP_ATTACK_ADJUSTMENT_FRAME_TWO;
                        break;
                    case 3:
                        upAdjustment = VERTICAL_ATTACK_ADJUSTMENT_FRAME_THREE;
                        sourceRectangleDimensions[3] = UP_ATTACK_FRAME_THREE;
                        sourceRectangleDimensions[1] = yCoordinate - UP_ATTACK_ADJUSTMENT_FRAME_THREE;
                        break;
                    case 4:
                        upAdjustment = VERTICAL_ATTACK_ADJUSTMENT_FRAME_FOUR;
                        sourceRectangleDimensions[3] = UP_ATTACK_FRAME_FOUR;
                        sourceRectangleDimensions[1] = yCoordinate - UP_ATTACK_ADJUSTMENT_FRAME_FOUR;
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
                            leftAdjustment = HORIZONTAL_ATTACK_ADJUSTMENT_FRAME_TWO;
                        }
                        sourceRectangleDimensions[2] = SIDE_ATTACK_FRAME_TWO;
                        break;
                    case 3:
                        if (facingDirection == (int)LinkSprite.LinkSpriteDirection.Left)
                        {
                            leftAdjustment = HORIZONTAL_ATTACK_ADJUSTMENT_FRAME_THREE;
                        }
                        sourceRectangleDimensions[2] = SIDE_ATTACK_FRAME_THREE;
                        sourceRectangleDimensions[0] = xCoordinate + SIDE_ATTACK_ADJUSTMENT_FRAME_THREE;
                        break;
                    case 4:
                        if (facingDirection == (int)LinkSprite.LinkSpriteDirection.Left)
                        {
                            leftAdjustment = HORIZONTAL_ATTACK_ADJUSTMENT_FRAME_FOUR;
                        }
                        sourceRectangleDimensions[2] = SIDE_ATTACK_FRAME_FOUR;
                        sourceRectangleDimensions[0] = xCoordinate + SIDE_ATTACK_ADJUSTMENT_FRAME_FOUR;
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
