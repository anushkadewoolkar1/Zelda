using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Sprites
{
    public class EnemySpriteFactory
    {
        private Texture2D enemySpritesheet;
        private Texture2D bossSpritesheet;
        private Texture2D npcSpritesheet;

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
        }

        // change all these when the spritesheets are actually loaded in
        public EnemySprite CreateNPCSprite()
        {
            return new EnemySprite(npcSpritesheet, 5, 1);
        }
        
        public EnemySprite CreateSmallEnemySprite()
        {
            return new EnemySprite(npcSpritesheet, 0, 0);
        }

        public EnemySprite CreateLargeEnemySprite()
        {
            return new EnemySprite(npcSpritesheet, 0, 0);
        }

        public EnemySprite CreateBossEnemySprite()
        {
            return new EnemySprite(npcSpritesheet, 0, 0);
        }
    }
}
