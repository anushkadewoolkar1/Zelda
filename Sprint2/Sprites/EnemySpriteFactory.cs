﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;

namespace MainGame.Sprites
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpawnSheet;
        private Texture2D largeSpritesheet;
        private Texture2D smallSpritesheet;
        private Texture2D bossSpritesheet;
        private Texture2D npcSpritesheet;
        private EnemySprite sprite;
        private int xSize;
        private int ySize;

        private static EnemySpriteFactory instance = new EnemySpriteFactory();
        public static EnemySpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemySpriteFactory()
        {

        }

        public void LoadAllTextures(ContentManager content)
        {
            enemySpawnSheet = content.Load<Texture2D>("EnemySpawning");
            npcSpritesheet = content.Load<Texture2D>("lozNPCs");
            largeSpritesheet = content.Load<Texture2D>("LargeEnemies");
            smallSpritesheet = content.Load<Texture2D>("SmallEnemies");
            bossSpritesheet = content.Load<Texture2D>("BossEnemies");
        }

        public EnemySprite CreateNPCSprite()
        {
            return new EnemySprite(npcSpritesheet, 1, 1, 0, 0, 16, 32, EnemyType.OldMan);
        }

        public EnemySprite CreateEnemySprite(EnemyType enemyType, Direction direction)
        {
            // enemies spawn in with an animation, add that in later
            xSize = 16;
            ySize = 32;
            switch(enemyType)
            {
                case EnemyType.OldMan:
                    sprite = CreateNPCSprite();
                    sprite.spriteSize = 32;
                    return sprite;
                case EnemyType.Keese:
                    sprite = CreateSmallEnemySprite(enemyType, 2, 21, 1);
                    sprite.spriteSize = 32;
                    return sprite;
                case EnemyType.Stalfos:
                    sprite = CreateLargeEnemySprite(enemyType, 2, 124, 18);
                    sprite.spriteSize = 32;
                    return sprite;
                case EnemyType.Goriya:
                    ySize = 16;
                    switch(direction)
                    {
                        case Direction.Left:
                            sprite = CreateLargeEnemySprite(enemyType, 2, 100, 1);
                            break;
                        case Direction.Right:
                            sprite = CreateLargeEnemySprite(enemyType, 2, 133, 1);
                            break;
                        case Direction.Up:
                            sprite = CreateLargeEnemySprite(enemyType, 2, 66, 1);
                            break;
                        case Direction.Down:
                            sprite = CreateLargeEnemySprite(enemyType, 2, 32, 1);
                            break;
                        default:
                            sprite = CreateLargeEnemySprite(enemyType, 2, 32, 1);
                            break;
                    }
                    sprite.spriteSize = 32;
                    return sprite;
                case EnemyType.Gel:
                    xSize = 8;
                    ySize = 16;
                    sprite = CreateSmallEnemySprite(enemyType, 2, 2, 1);
                    sprite.spriteSize = 16;
                    return sprite;
                case EnemyType.Zol:
                    ySize = 16;
                    sprite = CreateLargeEnemySprite(enemyType, 2, 0, 1);
                    sprite.spriteSize = 32;
                    return sprite;
                case EnemyType.Trap:
                    sprite = CreateLargeEnemySprite(enemyType, 1, 34, 18);
                    sprite.spriteSize = 32;
                    return sprite;
                case EnemyType.Wallmaster:
                    sprite = CreateLargeEnemySprite(enemyType, 2, 1, 18);
                    sprite.spriteSize = 32;
                    return sprite;
                case EnemyType.Rope:
                    sprite = CreateLargeEnemySprite(enemyType, 2, 52, 18);
                    sprite.spriteSize = 32;
                    return sprite;
                case EnemyType.Aquamentus:
                    xSize = 27;
                    sprite = CreateBossEnemySprite(enemyType, 2, 0, 0);
                    sprite.spriteSize = 45;
                    return sprite;
                case EnemyType.Dodongo:
                    switch (direction)
                    {
                        case Direction.Left:
                            xSize = 28;
                            sprite = CreateBossEnemySprite(enemyType, 2, 106, 34);
                            sprite.spriteSize = 35;
                            break;
                        case Direction.Right:
                            xSize = 28;
                            sprite = CreateBossEnemySprite(enemyType, 2, 235, 34);
                            sprite.spriteSize = 35;
                            break;
                        case Direction.Up:
                            sprite = CreateBossEnemySprite(enemyType, 2, 53, 34);
                            sprite.spriteSize = 32;
                            break;
                        case Direction.Down:
                            sprite = CreateBossEnemySprite(enemyType, 2, 1, 34);
                            sprite.spriteSize = 32;
                            break;
                        default:
                            sprite = CreateBossEnemySprite(enemyType, 2, 0, 34);
                            break;
                    }
                    return sprite;
                default:
                    sprite = CreateSmallEnemySprite(EnemyType.None, 1, 0, 0);
                    sprite.spriteSize = 1;
                    return sprite;
            }
        }

        // if true, then we are wanting an x coordinate. if false, we want a y coordinate
        public int GetEnemySize(Boolean x_coordinate, EnemyType enemyType)
        {
            if (x_coordinate)
            {
                switch(enemyType)
                {
                    case EnemyType.Gel:
                        return 8;
                    case EnemyType.Aquamentus:
                        return 27;
                    default:
                        return 16;
                }
            } else
            {
                if(enemyType == EnemyType.Gel || enemyType == EnemyType.Goriya || enemyType == EnemyType.Zol)
                {
                    return 16;
                } else
                {
                    return 32;
                }
            }
        }

        public EnemyProjectileSprite CreateEnemyProjectileSprite(ItemType itemType, Direction direction)
        {
            EnemyProjectileSprite sprite;
            switch (itemType)
            {
                case ItemType.Boomerang:
                    // return new CreateBoomerangeSprite(itemType, ___, ___, ___);
                case ItemType.Fireball:
                    sprite = CreateFireballSprite(124, 4);
                    break;
                default:
                    sprite = null;
                    break;
            }

            return sprite;
        }
        
        public EnemySprite CreateSmallEnemySprite(EnemyType enemyType, int rows, int startX, int startY)
        {
            return new EnemySprite(smallSpritesheet, rows, 1, startX, startY, xSize, ySize, enemyType);
        }

        public EnemySprite CreateLargeEnemySprite(EnemyType enemyType, int rows, int startX, int startY)
        {
            return new EnemySprite(largeSpritesheet, rows, 1, startX, startY, xSize, ySize, enemyType);
        }

        public EnemySprite CreateBossEnemySprite(EnemyType enemyType, int rows, int startX, int startY)
        {
            return new EnemySprite(bossSpritesheet, rows, 1, startX, startY, xSize, ySize, enemyType);
        }

        public EnemyProjectileSprite CreateFireballSprite(int startX, int startY)
        {
            return new EnemyProjectileSprite(bossSpritesheet, 3, 1, startX, startY, xSize, ySize, ItemType.Fireball);
        }
    }
}
