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
        }

        public void Load()
        {
            SetSprite();
        }

        public void SetSprite()
        {
            if (enemy.enemyType == Zelda.Enums.EnemyType.OldMan)
            {
                ISprite npcSprite = EnemySpriteFactory.Instance.CreateNPCSprite();
            }
            else
            {
                ISprite enemySprite = EnemySpriteFactory.Instance.CreateEnemySprite(enemy.enemyType);
            }
        }

        public void Update(GameTime gameTime)
        {
            // trigger non-moving animation
            // vector work done here
            if (enemy.enemyType != Zelda.Enums.EnemyType.OldMan)
            {
                enemy.ChangeState(new EnemyMovingState(enemy));
            }

        }

        public void Stop()
        {
            // no-op
        }
    }
}
