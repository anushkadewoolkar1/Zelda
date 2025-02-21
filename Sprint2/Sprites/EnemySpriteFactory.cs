using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;

namespace Sprint0.Sprites
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpawnSheet;
        private Texture2D largeSpritesheet;
        private Texture2D smallSpritesheet;
        private Texture2D bossSpritesheet;
        private Texture2D npcSpritesheet;
        private EnemySprite sprite;
        private int XSize;
        private int YSize;

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
            XSize = 16;
            YSize = 32;
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
                    YSize = 16;
                    // will need to change this to UpdateSprite so that they still animate
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
                    XSize = 8;
                    YSize = 16;
                    sprite = CreateSmallEnemySprite(enemyType, 2, 2, 1);
                    sprite.spriteSize = 16;
                    return sprite;
                case EnemyType.Zol:
                    YSize = 16;
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
                    XSize = 27;
                    sprite = CreateBossEnemySprite(enemyType, 2, 0, 0);
                    sprite.spriteSize = 54;
                    return sprite;
                case EnemyType.Dodongo:
                    // will need to change CreateBossEnemySprite to UpdateSprite so they still animate (the frames are being reset each time methinks) 
                    switch (direction)
                    {
                        case Direction.Left:
                            XSize = 28;
                            sprite = CreateBossEnemySprite(enemyType, 2, 106, 34);
                            sprite.spriteSize = 35;
                            break;
                        case Direction.Right:
                            XSize = 28;
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
                    sprite = CreateNPCSprite();
                    sprite.spriteSize = 32;
                    return sprite;
            }
        }
        
        public EnemySprite CreateSmallEnemySprite(EnemyType enemyType, int rows, int startX, int startY)
        {
            return new EnemySprite(smallSpritesheet, rows, 1, startX, startY, XSize, YSize, enemyType);
        }

        public EnemySprite CreateLargeEnemySprite(EnemyType enemyType, int rows, int startX, int startY)
        {
            return new EnemySprite(largeSpritesheet, rows, 1, startX, startY, XSize, YSize, enemyType);
        }

        public EnemySprite CreateBossEnemySprite(EnemyType enemyType, int rows, int startX, int startY)
        {
            return new EnemySprite(bossSpritesheet, rows, 1, startX, startY, XSize, YSize, enemyType);
        }
    }
}
