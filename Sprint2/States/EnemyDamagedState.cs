using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.Sprites;

namespace MainGame.States
{
    public class EnemyDamagedState : IEnemyState
    {
        private Enemy enemy;
        private SpriteBatch spriteBatch;
        int timer = 1000;
        private Vector2 position;

        const int ZERO = 0;

        public EnemyDamagedState(Enemy enemy)
        {
            this.enemy = enemy;
            ISprite enemySprite = EnemySpriteFactory.Instance.CreateEnemySprite(enemy.enemyType, enemy.Direction);
        }
        public void Load()
        {
            // sprite work done here
        }

        public void Update(GameTime gameTime)
        {
            // vector work done here
            enemy.Move(position, gameTime);
            
            timer--;
            if (timer == ZERO)
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
