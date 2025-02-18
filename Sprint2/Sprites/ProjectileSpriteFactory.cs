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

            return new ProjectileSprite(projectileSpriteSheet, 1, 185, (int)LinkDirection.DownFacing);
        }

        public ISprite CreateUpArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, 1, 185, (int)LinkDirection.UpFacing);
        }

        public ISprite CreateLeftArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, 1, 185, (int)LinkDirection.LeftFacing);
        }

        public ISprite CreateRightArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, 1, 185, (int)LinkDirection.RightFacing);
        }

        public ISprite CreateDownArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, 29, 185, (int)LinkDirection.DownFacing);
        }

        public ISprite CreateUpArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, 29, 185, (int)LinkDirection.UpFacing);
        }

        public ISprite CreateLeftArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, 29, 185, (int)LinkDirection.LeftFacing);
        }

        public ISprite CreateRightArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, 29, 185, (int)LinkDirection.RightFacing);
        }

        public ISprite CreateBoomerangBrown(int direction)
        {
            return new ProjectileSprite(projectileSpriteSheet, 65, 189, direction);
        }

        public ISprite CreateBoomerangBlue(int direction)
        {

            return new ProjectileSprite(projectileSpriteSheet, 92, 189, direction);
        }

        public ISprite CreateBomb()
        {

            return new ProjectileSprite(projectileSpriteSheet, 129, 185, -4);
        }

        public ISprite CreateLadderVertical()
        {

            return new ProjectileSprite(projectileSpriteSheet, 280, 185, (int)LinkDirection.DownFacing - notMoving);
        }

        public ISprite CreateLadderHorizontal()
        {

            return new ProjectileSprite(projectileSpriteSheet, 280, 185, (int)LinkDirection.RightFacing - notMoving);
        }

        /* Color
         * 1 = Green
         * 2 = Dark Blue
         * 3 = Red
         * 4 = Light Blue
         */

        public ISprite CreateDownAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, 171 + (color * 17), 154, (int)LinkDirection.DownFacing);
        }

        public ISprite CreateUpAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, 171 + (color * 17), 154, (int)LinkDirection.UpFacing);
        }

        public ISprite CreateLeftAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, 171 + (color * 17), 154, (int)LinkDirection.LeftFacing);
        }

        public ISprite CreateRightAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, 171 + (color * 17), 154, (int)LinkDirection.RightFacing);
        }

    }
}
