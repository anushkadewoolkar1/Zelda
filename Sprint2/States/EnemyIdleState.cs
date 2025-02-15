using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.States
{
    public class EnemyIdleState : IEnemyState
    {
        
        private Enemy enemy;
        private SpriteBatch spriteBatch;
        private Vector2 position;

        public EnemyIdleState(Enemy enemy)
        {
            this.enemy = enemy;
            // construct sprite here
            // will probably end up doing switch case here to account for different enemy sizes
            if (enemy.enemyType == Zelda.Enums.EnemyType.OldMan)
            {
                ISprite npcSprite = EnemySpriteFactory.Instance.CreateNPCSprite();
            } else
            {
                ISprite enemySprite = EnemySpriteFactory.Instance.CreateEnemySprite(enemy.enemyType);
            }
        }

        public void Load()
        {
            // sprite work done here
        }

        public void Update(GameTime gameTime)
        {
            // trigger non-moving animation
            // vector work done here

            enemy.Update(gameTime);
        }

        public void Stop()
        {
            // no-op
        }
    }
}
