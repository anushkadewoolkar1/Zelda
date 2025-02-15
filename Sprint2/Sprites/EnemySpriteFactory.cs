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
        private Texture2D largeSpritesheet;
        private Texture2D smallSpritesheet;
        private Texture2D bossSpritesheet;
        private Texture2D npcSpritesheet;
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
            npcSpritesheet = content.Load<Texture2D>("lozNPCs");
            largeSpritesheet = content.Load<Texture2D>("LargeEnemies");
            smallSpritesheet = content.Load<Texture2D>("SmallEnemies");
            bossSpritesheet = content.Load<Texture2D>("BossEnemies");
        }

        // change all these when the spritesheets are actually loaded in
        public EnemySprite CreateNPCSprite()
        {
            return new EnemySprite(npcSpritesheet, 1, 1, 0, 0, 16, 32);
        }

        public EnemySprite CreateEnemySprite(EnemyType enemyType)
        {
            EnemySprite sprite;
            XSize = 16;
            YSize = 32;
            switch(enemyType)
            {
                case EnemyType.OldMan:
                    return sprite = CreateNPCSprite();
                case EnemyType.Keese:
                    return sprite = CreateSmallEnemySprite(enemyType, 2, 19, 0);
                case EnemyType.Stalfos:
                    return sprite = CreateLargeEnemySprite(enemyType, 2, 123, 18);
                case EnemyType.Goriya:
                    YSize = 16;
                    return sprite = CreateLargeEnemySprite(enemyType, 6, 30, 0);
                case EnemyType.Gel:
                    XSize = 8;
                    YSize = 16;
                    return sprite = CreateSmallEnemySprite(enemyType, 2, 0, 0);
                case EnemyType.Zol:
                    XSize = 12;
                    YSize = 16;
                    return sprite = CreateLargeEnemySprite(enemyType, 2, 0, 0);
                default:
                    return sprite = CreateNPCSprite();
            }
        }
        
        public EnemySprite CreateSmallEnemySprite(EnemyType enemyType, int rows, int startX, int startY)
        {
            EnemySprite sprite;
            return sprite = new EnemySprite(smallSpritesheet, rows, 1, startX, startY, XSize, YSize);
        }

        public EnemySprite CreateLargeEnemySprite(EnemyType enemyType, int rows, int startX, int startY)
        {
            EnemySprite sprite;
            return sprite = new EnemySprite(largeSpritesheet, rows, 1, startX, startY, XSize, YSize);
        }

        public EnemySprite CreateBossEnemySprite()
        {
            return new EnemySprite(bossSpritesheet, 0, 0, 0, 0, XSize, YSize);
        }
    }
}
