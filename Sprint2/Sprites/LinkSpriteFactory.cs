using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.Sprites
{
    public class LinkSpriteFactory
    {
        private Texture2D zeldaSpriteSheet;
        private enum LinkState { GreenSmallShield, GreenMagicalShield, WhiteSmallShield, WhiteMagicalShield, RedSmallShield, RedMagicalShield };
        private LinkState state = LinkState.GreenSmallShield;
        private enum LinkDirection { DownFacing, LeftFacing, RightFacing, UpFacing };
        private LinkDirection direction = LinkDirection.RightFacing;
        private enum LinkDamaged { Normal, Damaged };
        private LinkDamaged health = LinkDamaged.Normal;
        int[] LinkTotality;

        private static LinkSpriteFactory instance = new LinkSpriteFactory();

        public static LinkSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private LinkSpriteFactory()
        {
        }

        public void LoadZeldaTextures(ContentManager spriteBatch)
        {
            zeldaSpriteSheet = spriteBatch.Load<Texture2D>("LinkAllOverworldColorsWithReflection");
        }

        /* See enumerations above for how state and health is implemented
         */
        public ISprite CreateDownWalk(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 1, 11, LinkTotality);
        }

        public ISprite CreateUpWalk(int frame, int state, int health)
        {
            LinkTotality = [3, frame, state, health];
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
            LinkTotality = [3, 1, state, health];
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
            LinkTotality = [3, 1, state, health];
            return new LinkSprite(zeldaSpriteSheet, 230, 11, LinkTotality);
        }


        public ISprite CreateDownAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 1, 47, LinkTotality);
        }

        public ISprite CreateUpAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [3, frame, state, health];
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
            LinkTotality = [3, frame, state, health];
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
            LinkTotality = [3, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 187, 108, LinkTotality);
        }

        public ISprite CreateLeftAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [1, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 187, 77, LinkTotality);
        }

        public ISprite CreateRightAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [2, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 187, 77, LinkTotality);
        }


        public ISprite CreateDownAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [0, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 280, 47, LinkTotality);
        }

        public ISprite CreateUpAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [3, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 280, 108, LinkTotality);
        }

        public ISprite CreateLeftAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [1, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 280, 77, LinkTotality);
        }

        public ISprite CreateRightAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [2, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, 280, 77, LinkTotality);
        }
    }
}
