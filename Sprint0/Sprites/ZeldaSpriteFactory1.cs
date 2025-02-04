using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteFactory
{
    public class ZeldaSpriteFactory
    {
        private Texture2D zeldaSpriteSheet;

        private static ZeldaSpriteFactory instance = new ZeldaSpriteFactory();

        public static ZeldaSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemySpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager spriteBatch)
        {

            zeldaSpriteSheet = content.Load<Texture2D>("zelda");

        }

        public ISprite CreateDownIdle()
        {
            return new ZeldaSprite(zeldaSpritesheet, 1, 11);
        }

        public ISprite CreateUpIdle()
        {
            return new ZeldaSprite(zeldaSpritesheet, 69, 11);
        }

        public ISprite CreateLeftIdle()
        {
            return new ZeldaSprite(zeldaSpritesheet, 35, 11);
        }

        public ISprite CreateRightIdle()
        {
            return new ZeldaSprite(zeldaSpritesheet, 35, 11);
        }

        public ISprite CreateDownWalk()
        {
            return new ZeldaSprite(zeldaSpritesheet, 1, 11);
        }

        public ISprite CreateUpWalk()
        {
            return new ZeldaSprite(zeldaSpritesheet, 69, 11);
        }

        public ISprite CreateLeftWalk()
        {
            return new ZeldaSprite(zeldaSpritesheet, 35, 11);
        }

        public ISprite CreateRightWalk()
        {
            return new ZeldaSprite(zeldaSpritesheet, 35, 11);
        }

        public ISprite CreateUseItemDown()
        {
            return new ZeldaSprite(zeldaSpritesheet, 107, 11);
        }

        public ISprite CreateUseItemUp()
        {
            return new ZeldaSprite(zeldaSpritesheet, 141, 11);
        }

        public ISprite CreateUseItemLeft()
        {
            return new ZeldaSprite(zeldaSpritesheet, 124, 11);
        }

        public ISprite CreateUseItemRight()
        {
            return new ZeldaSprite(zeldaSpritesheet, 124, 11);
        }


        public ISprite CreateUsePickUpItem()
        {
            return new ZeldaSprite(zeldaSpritesheet, 213, 11);
        }

        public ISprite CreateUseItemUp()
        {
            return new ZeldaSprite(zeldaSpritesheet, 230, 11);
        }


        public ISprite CreateDownAttackWoodenSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 1, 47);
        }

        public ISprite CreateUpAttackWoodenSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 1, 108);
        }

        public ISprite CreateLeftAttackWoodenSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 1, 77);
        }

        public ISprite CreateRightAttackWoodenSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 1, 77);
        }


        public ISprite CreateDownAttackWhiteSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 94, 47);
        }

        public ISprite CreateUpAttackWhiteSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 94, 108);
        }

        public ISprite CreateLeftAttackWhiteSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 94, 77);
        }

        public ISprite CreateRightAttackWhiteSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 94, 77);
        }


        public ISprite CreateDownAttackMagicalSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 187, 47);
        }

        public ISprite CreateUpAttackMagicalSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 187, 108);
        }

        public ISprite CreateLeftAttackWMagicalSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 187, 77);
        }

        public ISprite CreateRightAttackMagicalSword()
        {
            return new ZeldaSprite(zeldaSpritesheet, 187, 77);
        }


        public ISprite CreateDownAttackMagicalRod()
        {
            return new ZeldaSprite(zeldaSpritesheet, 32, 32);
        }

        public ISprite CreateUpAttackMagicalRod()
        {
            return new ZeldaSprite(zeldaSpritesheet, 32, 32);
        }

        public ISprite CreateLeftAttackWMagicalRod()
        {
            return new ZeldaSprite(zeldaSpritesheet, 32, 32);
        }

        public ISprite CreateRightAttackMagicalRod()
        {
            return new ZeldaSprite(zeldaSpritesheet, 32, 32);
        }
    }
}
