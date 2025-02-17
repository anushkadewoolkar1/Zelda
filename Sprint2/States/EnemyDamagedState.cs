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
    public class EnemyDamagedState : IEnemyState
    {
        private Enemy enemy;
        private SpriteBatch spriteBatch;
        int timer = 1000;
        private Vector2 position;

        public EnemyDamagedState(Enemy enemy)
        {
            this.enemy = enemy;
            // construct sprite here
            ISprite enemySprite = EnemySpriteFactory.Instance.CreateEnemySprite(enemy.enemyType);
        }
        public void Load()
        {
            // sprite work done here
        }

        public void Update(Vector2 position, GameTime gameTime)
        {
            // vector work done here
            enemy.TakeDamage();
            enemy.Move(position, gameTime);
            
            timer--;
            if (timer == 0)
            {
                RemoveDecorator();
            }

            enemy.Update(gameTime);
        }

        void RemoveDecorator()
        {
            // go back to EnemyMovingState
        }

        public void Stop()
        {
            // no-op
        }
    }
}
