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


        //Acts as a key for what each value stands of LinkState, LinkDirection, and LinkDamaged stands for
        private enum LinkState { GreenSmallShield, GreenMagicalShield, WhiteSmallShield, WhiteMagicalShield, RedSmallShield, RedMagicalShield };
        private LinkState state = LinkState.GreenSmallShield;
        private enum LinkDirection { DownFacing, LeftFacing, RightFacing, UpFacing };
        private LinkDirection direction = LinkDirection.RightFacing;
        private enum LinkDamaged { Normal, Damaged };
        private LinkDamaged health = LinkDamaged.Normal;

        private const int WALK_AND_ITEM_YPOS = 11;
        private const int WOODEN_XPOS = 1;
        private const int WHITE_XPOS = 94;
        private const int MAGIC_SWORD_XPOS = 187;
        private const int MAGIC_ROD_XPOS = 230;
        private const int ATTACK_DOWN_YPOS = 47;
        private const int ATTACK_UP_YPOS = 108;
        private const int ATTACK_LEFTRIGHT_YPOS = 77;
        private const int WALK_DOWN_XPOS = 1;
        private const int WALK_UP_XPOS = 69;
        private const int WALK_LEFTRIGHT_XPOS = 35;
        private const int ITEM_DOWN_XPOS = 107;
        private const int ITEM_UP_XPOS = 141;
        private const int ITEM_LEFTRIGHT_XPOS = 124;
        private const int ITEM_ONE_XPOS = 213;
        private const int ITEM_TWO_XPOS = 230;
        private const int DOWN = 0;
        private const int LEFT = 1;
        private const int RIGHT = 2;
        private const int UP = 3;
        private const int ANIMTN_START_FRAME = 1;

        //linkTotality = {direction, frame, state, health}
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

        public void LoadLinkTextures(ContentManager spriteBatch)
        {
            zeldaSpriteSheet = spriteBatch.Load<Texture2D>("LinkAllOverworldColorsWithReflection");
        }

        /* See enumerations above for how state and health is implemented
         */
        public ISprite CreateDownWalk(int frame, int state, int health)
        {
            LinkTotality = [DOWN, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WALK_DOWN_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }

        public ISprite CreateUpWalk(int frame, int state, int health)
        {
            LinkTotality = [UP, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WALK_UP_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }

        public ISprite CreateLeftWalk(int frame, int state, int health)
        {
            LinkTotality = [LEFT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WALK_LEFTRIGHT_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }

        public ISprite CreateRightWalk(int frame, int state, int health)
        {
            LinkTotality = [RIGHT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WALK_LEFTRIGHT_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }

        public ISprite CreateUseItemDown(int frame, int state, int health)
        {
            LinkTotality = [DOWN, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, ITEM_DOWN_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }

        public ISprite CreateUseItemUp(int state, int health)
        {
            LinkTotality = [UP, ANIMTN_START_FRAME, state, health];
            return new LinkSprite(zeldaSpriteSheet, ITEM_UP_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }

        public ISprite CreateUseItemLeft(int state, int health)
        {
            LinkTotality = [LEFT, ANIMTN_START_FRAME, state, health];
            return new LinkSprite(zeldaSpriteSheet, ITEM_LEFTRIGHT_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }

        public ISprite CreateUseItemRight(int state, int health)
        {
            LinkTotality = [RIGHT, ANIMTN_START_FRAME, state, health];
            return new LinkSprite(zeldaSpriteSheet, ITEM_LEFTRIGHT_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }


        public ISprite CreatePickUpItemOne(int state, int health)
        {
            LinkTotality = [UP, ANIMTN_START_FRAME, state, health];
            return new LinkSprite(zeldaSpriteSheet, ITEM_ONE_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }

        public ISprite CreatePickUpItemTwo(int state, int health)
        {
            LinkTotality = [UP, ANIMTN_START_FRAME, state, health];
            return new LinkSprite(zeldaSpriteSheet, ITEM_TWO_XPOS, WALK_AND_ITEM_YPOS, LinkTotality);
        }


        public ISprite CreateDownAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [DOWN, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WOODEN_XPOS, ATTACK_DOWN_YPOS, LinkTotality);
        }

        public ISprite CreateUpAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [UP, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WOODEN_XPOS, ATTACK_UP_YPOS, LinkTotality);
        }

        public ISprite CreateLeftAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [LEFT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WOODEN_XPOS, ATTACK_LEFTRIGHT_YPOS, LinkTotality);
        }

        public ISprite CreateRightAttackWoodenSword(int frame, int state, int health)
        {
            LinkTotality = [RIGHT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WOODEN_XPOS, ATTACK_LEFTRIGHT_YPOS, LinkTotality);
        }


        public ISprite CreateDownAttackWhiteSword(int frame, int state, int health)
        {
            LinkTotality = [DOWN, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WHITE_XPOS, ATTACK_DOWN_YPOS, LinkTotality);
        }

        public ISprite CreateUpAttackWhiteSword(int frame, int state, int health)
        {
            LinkTotality = [UP, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WHITE_XPOS, ATTACK_UP_YPOS, LinkTotality);
        }

        public ISprite CreateLeftAttackWhiteSword(int frame, int state, int health)
        {
            LinkTotality = [LEFT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WHITE_XPOS, ATTACK_LEFTRIGHT_YPOS, LinkTotality);
        }

        public ISprite CreateRightAttackWhiteSword(int frame, int state, int health)
        {
            LinkTotality = [RIGHT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, WHITE_XPOS, ATTACK_LEFTRIGHT_YPOS, LinkTotality);
        }


        public ISprite CreateDownAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [DOWN, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, MAGIC_SWORD_XPOS, ATTACK_DOWN_YPOS, LinkTotality);
        }

        public ISprite CreateUpAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [UP, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, MAGIC_SWORD_XPOS, ATTACK_UP_YPOS, LinkTotality);
        }

        public ISprite CreateLeftAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [LEFT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, MAGIC_SWORD_XPOS, ATTACK_LEFTRIGHT_YPOS, LinkTotality);
        }

        public ISprite CreateRightAttackMagicalSword(int frame, int state, int health)
        {
            LinkTotality = [RIGHT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, MAGIC_SWORD_XPOS, ATTACK_LEFTRIGHT_YPOS, LinkTotality);
        }


        public ISprite CreateDownAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [DOWN, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, MAGIC_ROD_XPOS, ATTACK_DOWN_YPOS, LinkTotality);
        }

        public ISprite CreateUpAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [UP, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, MAGIC_ROD_XPOS, ATTACK_UP_YPOS, LinkTotality);
        }

        public ISprite CreateLeftAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [LEFT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, MAGIC_ROD_XPOS, ATTACK_LEFTRIGHT_YPOS, LinkTotality);
        }

        public ISprite CreateRightAttackMagicalRod(int frame, int state, int health)
        {
            LinkTotality = [RIGHT, frame, state, health];
            return new LinkSprite(zeldaSpriteSheet, MAGIC_ROD_XPOS, ATTACK_LEFTRIGHT_YPOS, LinkTotality);
        }
    }
}
