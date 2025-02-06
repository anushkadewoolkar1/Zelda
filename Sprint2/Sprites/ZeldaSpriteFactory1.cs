using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace SpriteFactory
{
    public class ZeldaSpriteFactory
    {
        private Vector2 _position
        private Texture2D zeldaSpriteSheet;
        private enum ZeldaStates { GreenSmallShield, GreenMagicalShield, WhiteSmallShield, WhiteMagicalShield, RedSmallShield, RedMagicalShield };
        private ZeldaStates currentState = ZeldaStates.GreenSmallShield;

        private static ZeldaSpriteFactory instance = new ZeldaSpriteFactory();

        public static ZeldaSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ZeldaSpriteFactory()
        {
        }

        public void LoadZeldaTextures(ContentManager spriteBatch)
        {
            zeldaSpriteSheet = spriteBatch.Load<Texture2D>("Zelda");
        }
        /*
        public ISprite CreateDownIdle()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 1, 11);
        }

        public ISprite CreateUpIdle()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 69, 11);
        }

        public ISprite CreateLeftIdle()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 35, 11);
        }

        public ISprite CreateRightIdle()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 35, 11);
        }
        */
        public ISprite CreateDownWalk(int frame, int state)
        {
            return new ZeldaSprite(zeldaSpriteSheet, 1, 11, frame, state);
        }

        public ISprite CreateUpWalk(int frame, int state)
        {
            return new ZeldaSprite(zeldaSpriteSheet, 69, 11, frame, state);
        }

        public ISprite CreateLeftWalk(int frame, int state)
        {
            return new ZeldaSprite(zeldaSpriteSheet, 35, 11, frame, state);
        }

        public ISprite CreateRightWalk(int frame, int state)
        {
            return new ZeldaSprite(zeldaSpriteSheet, 35, 11, frame, state);
        }

        public ISprite CreateUseItemDown(int frame, int state)
        {
            return new ZeldaSprite(zeldaSpriteSheet, 107, 11, frame, state);
        }

        public ISprite CreateUseItemUp()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 141, 11);
        }

        public ISprite CreateUseItemLeft()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 124, 11);
        }

        public ISprite CreateUseItemRight()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 124, 11);
        }


        public ISprite CreatePickUpItemOne()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 213, 11);
        }

        public ISprite CreatePickUpItemTwo()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 230, 11);
        }


        public ISprite CreateDownAttackWoodenSword(int frame)
        {
            return new ZeldaSprite(zeldaSpriteSheet, 1, 47, frame);
        }

        public ISprite CreateUpAttackWoodenSword(int frame)
        {
            return new ZeldaSprite(zeldaSpriteSheet, 1, 108, frame);
        }

        public ISprite CreateLeftAttackWoodenSword(int frame)
        {
            return new ZeldaSprite(zeldaSpriteSheet, 1, 77, frame);
        }

        public ISprite CreateRightAttackWoodenSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 1, 77);
        }


        public ISprite CreateDownAttackWhiteSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 94, 47);
        }

        public ISprite CreateUpAttackWhiteSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 94, 108);
        }

        public ISprite CreateLeftAttackWhiteSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 94, 77);
        }

        public ISprite CreateRightAttackWhiteSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 94, 77);
        }


        public ISprite CreateDownAttackMagicalSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 187, 47);
        }

        public ISprite CreateUpAttackMagicalSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 187, 108);
        }

        public ISprite CreateLeftAttackWMagicalSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 187, 77);
        }

        public ISprite CreateRightAttackMagicalSword()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 187, 77);
        }


        public ISprite CreateDownAttackMagicalRod()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 32, 32);
        }

        public ISprite CreateUpAttackMagicalRod()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 32, 32);
        }

        public ISprite CreateLeftAttackWMagicalRod()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 32, 32);
        }

        public ISprite CreateRightAttackMagicalRod()
        {
            return new ZeldaSprite(zeldaSpriteSheet, 32, 32);
        }
    }
}
