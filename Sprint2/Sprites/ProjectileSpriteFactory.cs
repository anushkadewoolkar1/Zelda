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

        private enum LinkDirection { DownFacing, LeftFacing, RightFacing, UpFacing };
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

            return new ProjectileSprite(projectileSpriteSheet, 1, 185, 2);
        }

        public ISprite CreateUpArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, 1, 185, 0);
        }

        public ISprite CreateLeftArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, 1, 185, 3);
        }

        public ISprite CreateRightArrowBrown()
        {

            return new ProjectileSprite(projectileSpriteSheet, 1, 185, 1);
        }

        public ISprite CreateDownArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, 29, 185, 2);
        }

        public ISprite CreateUpArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, 29, 185, 0);
        }

        public ISprite CreateLeftArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, 29, 185, 3);
        }

        public ISprite CreateRightArrowBlue()
        {

            return new ProjectileSprite(projectileSpriteSheet, 29, 185, 1);
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

            return new ProjectileSprite(projectileSpriteSheet, 280, 185, 0);
        }

        public ISprite CreateLadderHorizontal()
        {

            return new ProjectileSprite(projectileSpriteSheet, 280, 185, 1);
        }

        /* Color
         * 1 = Green
         * 2 = Dark Blue
         * 3 = Red
         * 4 = Light Blue
         */

        public ISprite CreateDownAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, 171 + (color * 17), 154, 2);
        }

        public ISprite CreateUpAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, 171 + (color * 17), 154, 0);
        }

        public ISprite CreateLeftAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, 171 + (color * 17), 154, 3);
        }

        public ISprite CreateRightAttackWaves(int color)
        {

            return new ProjectileSprite(projectileSpriteSheet, 171 + (color * 17), 154, 1);
        }

    }
}
