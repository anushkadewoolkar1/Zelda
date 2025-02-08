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
        private Texture2D zeldaSpriteSheet;
        private enum LinkArmor { GreenSmallShield, GreenMagicalShield, WhiteSmallShield, WhiteMagicalShield, RedSmallShield, RedMagicalShield };
        private LinkArmor currentArmor = LinkArmor.GreenSmallShield;
        private enum LinkDirection { NoFacing, LeftFacing, RightFacing };
        private LinkDirection direction = LinkDirection.RightFacing;
        private enum LinkDamaged { Normal, Damaged };
        private LinkDamaged health = LinkDamaged.Normal;
        int[] LinkTotality;

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
            zeldaSpriteSheet = spriteBatch.Load<Texture2D>("LinkAllOverworldColorsWithReflection");
        }
        public ISprite CreateDownWalk(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 1, 11, LinkTotality);
        }

        public ISprite CreateUpWalk(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 69, 11, LinkTotality);
        }

        public ISprite CreateLeftWalk(int frame, int state, int health)
        {
            LinkTotality = [1, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 35, 11, LinkTotality);
        }

        public ISprite CreateRightWalk(int frame, int state, int health)
        {
            LinkTotality = [2, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 35, 11, LinkTotality);
        }

        public ISprite CreateUseItemDown(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 107, 11, LinkTotality);
        }

        public ISprite CreateUseItemUp(int state, int health)
        {
            LinkTotality = [0, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 141, 11, LinkTotality);
        }

        public ISprite CreateUseItemLeft(int state, int health)
        {
            LinkTotality = [1, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 124, 11, LinkTotality);
        }

        public ISprite CreateUseItemRight(int state, int health)
        {
            LinkTotality = [2, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 124, 11, LinkTotality);
        }


        public ISprite CreatePickUpItemOne(int state, int health)
        {
            LinkTotality = [0, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 213, 11, LinkTotality);
        }

        public ISprite CreatePickUpItemTwo(int state, int health)
        {
            LinkTotality = [0, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 230, 11, LinkTotality);
        }


        public ISprite CreateDownAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 1, 47, LinkTotality);
        }

        public ISprite CreateUpAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 1, 108, LinkTotality);
        }

        public ISprite CreateLeftAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [1, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 1, 77, LinkTotality);
        }

        public ISprite CreateRightAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [2, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 1, 77, LinkTotality);
        }


        public ISprite CreateDownAttackWhiteSword(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 94, 47, LinkTotality);
        }

        public ISprite CreateUpAttackWhiteSword(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 94, 108, LinkTotality);
        }

        public ISprite CreateLeftAttackWhiteSword(int frame, int state, int health)
        {
            LinkTotality = [1, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 94, 77, LinkTotality);
        }

        public ISprite CreateRightAttackWhiteSword(int frame, int state, int health)
        {
            LinkTotality = [2, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 94, 77, LinkTotality);
        }


        public ISprite CreateDownAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 187, 47, LinkTotality);
        }

        public ISprite CreateUpAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 187, 108, LinkTotality);
        }

        public ISprite CreateLeftAttackWMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [1, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 187, 77, LinkTotality);
        }

        public ISprite CreateRightAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [1, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 187, 77, LinkTotality);
        }


        public ISprite CreateDownAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [1, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 32, 32, LinkTotality);
        }

        public ISprite CreateUpAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [1, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 32, 32, LinkTotality);
        }

        public ISprite CreateLeftAttackWMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [1, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 32, 32, LinkTotality);
        }

        public ISprite CreateRightAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [1, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 32, 32, LinkTotality);
        }
    }
}
