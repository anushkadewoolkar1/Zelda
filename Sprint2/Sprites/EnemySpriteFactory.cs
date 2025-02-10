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
            enemySpritesheet = content.Load<Texture2D>("BossSpriteSheet");
            bossSpritesheet = content.Load<Texture2D>("BossSpriteSheet");
        }

        // change all these when the spritesheets are actually loaded in
        public EnemySprite CreateNPCSprite(SpriteBatch spriteBatch)
        {
            return new EnemySprite(enemySpritesheet, 0, 0);
        }
        
        public EnemySprite CreateSmallEnemySprite(SpriteBatch spriteBatch)
        {
            return new EnemySprite(enemySpritesheet, 0, 0);
        }

        public EnemySprite CreateLargeEnemySprite(SpriteBatch spriteBatch)
        {
            return new EnemySprite(enemySpritesheet, 0, 0);
        }

        public EnemySprite CreateBossEnemySprite(SpriteBatch spriteBatch)
        {
            return new EnemySprite(bossSpritesheet, 0, 0);
        }
    }
}
