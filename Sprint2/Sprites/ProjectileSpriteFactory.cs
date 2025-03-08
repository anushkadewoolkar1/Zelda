using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using Zelda.Enums;

namespace Sprint0.Sprites
{
    public class ProjectileSpriteFactory
    {
        private Texture2D projectileSpriteSheet;

        private enum LinkDirection { UpFacing, RightFacing, DownFacing, LeftFacing };

        //Subtranction so that rotating sprite works but sprite does not move
        private int notMoving = 4;

        private LinkDirection direction = LinkDirection.RightFacing;

        private const int DEFAULT_YPOS = 185;
        private const int BROWN_SWORD_XPOS = 2;
        private const int BROWN_SWORD_YPOS = 154;
        private const int BROWN_ARROW_XPOS = 1;
        private const int BLUE_ARROW_XPOS = 29;
        private const int BROWN_BMRNG_XPOS = 65;
        private const int BLUE_BMRNG_XPOS = 92;
        private const int BOMB_XPOS = 129;
        private const int LADDER_XPOS = 280;
        private const int BMRNG_YPOS = 189;
        private const int COLOR_SCALAR = 17;
        private const int BASE_POSITION = 171;
        private const int UPRIGHT_ORIENT = -4;


        private static ProjectileSpriteFactory instance = new ProjectileSpriteFactory();

        public static ProjectileSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ProjectileSpriteFactory()
        {
        }

        public void LoadProjectileTextures(ContentManager spriteBatch)
        {
            projectileSpriteSheet = spriteBatch.Load<Texture2D>("LinkAllOverworldColorsWithReflection");
        }

        /* See enumerations above for how state and health is implemented
         */
        public ISprite CreateDownArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, BROWN_ARROW_XPOS, DEFAULT_YPOS, (int)LinkDirection.DownFacing);
        }

        public ISprite CreateUpArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, BROWN_ARROW_XPOS, DEFAULT_YPOS, (int)LinkDirection.UpFacing);
        }

        public ISprite CreateLeftArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, BROWN_ARROW_XPOS, DEFAULT_YPOS, (int)LinkDirection.LeftFacing);
        }

        public ISprite CreateRightArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, BROWN_ARROW_XPOS, DEFAULT_YPOS, (int)LinkDirection.RightFacing);
        }

        public ISprite CreateDownWoodenSwordBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, BROWN_SWORD_XPOS, BROWN_SWORD_YPOS, (int)LinkDirection.DownFacing);
        }

        public ISprite CreateUpWoodenSwordBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, BROWN_SWORD_XPOS, BROWN_SWORD_YPOS, (int)LinkDirection.UpFacing);
        }

        public ISprite CreateLeftWoodenSwordBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, BROWN_SWORD_XPOS, BROWN_SWORD_YPOS, (int)LinkDirection.LeftFacing);
        }

        public ISprite CreateRightWoodenSwordBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, BROWN_SWORD_XPOS, BROWN_SWORD_YPOS, (int)LinkDirection.RightFacing);
        }

        public ISprite CreateDownArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, BLUE_ARROW_XPOS, DEFAULT_YPOS, (int)LinkDirection.DownFacing);
        }

        public ISprite CreateUpArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, BLUE_ARROW_XPOS, DEFAULT_YPOS, (int)LinkDirection.UpFacing);
        }

        public ISprite CreateLeftArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, BLUE_ARROW_XPOS, DEFAULT_YPOS, (int)LinkDirection.LeftFacing);
        }

        public ISprite CreateRightArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, BLUE_ARROW_XPOS, DEFAULT_YPOS, (int)LinkDirection.RightFacing);
        }

        public ISprite CreateBoomerangBrown(int direction)
        {
            return new ProjectileSprite(projectileSpriteSheet, BROWN_BMRNG_XPOS, BMRNG_YPOS, direction);
        }

        public ISprite CreateBoomerangBlue(int direction)
        {

            return new ProjectileSprite(projectileSpriteSheet, BLUE_BMRNG_XPOS, BMRNG_YPOS, direction);
        }

        public ISprite CreateBomb()
        {

            return new ProjectileSprite(projectileSpriteSheet, BOMB_XPOS, DEFAULT_YPOS, UPRIGHT_ORIENT);
        }

        public ISprite CreateLadderVertical()
        {

            return new ProjectileSprite(projectileSpriteSheet, LADDER_XPOS, DEFAULT_YPOS, (int)LinkDirection.DownFacing - notMoving);
        }

        public ISprite CreateLadderHorizontal()
        {

            return new ProjectileSprite(projectileSpriteSheet, LADDER_XPOS, DEFAULT_YPOS, (int)LinkDirection.RightFacing - notMoving);
        }

        /* Color
         * 1 = Green
         * 2 = Dark Blue
         * 3 = Red
         * 4 = Light Blue
         */

        public ISprite CreateDownAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, BASE_POSITION + (color * COLOR_SCALAR), (BASE_POSITION - COLOR_SCALAR), (int)LinkDirection.DownFacing);
        }

        public ISprite CreateUpAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, BASE_POSITION + (color * COLOR_SCALAR), (BASE_POSITION - COLOR_SCALAR), (int)LinkDirection.UpFacing);
        }

        public ISprite CreateLeftAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, BASE_POSITION + (color * COLOR_SCALAR), (BASE_POSITION - COLOR_SCALAR), (int)LinkDirection.LeftFacing);
        }

        public ISprite CreateRightAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, BASE_POSITION + (color * COLOR_SCALAR), (BASE_POSITION - COLOR_SCALAR), (int)LinkDirection.RightFacing);
        }

    }
}
