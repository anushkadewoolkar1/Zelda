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
            ISprite enemySprite = EnemySpriteFactory.Instance.CreateSmallEnemySprite();
        }
        public void Load()
        {
            enemy.Load();
        }

        public void Move(Vector2 position)
        {
            enemy.Move(position);
        }

        public void TakeDamage()
        {
            // change health status
            // i don't think the upgraded swords will be added, but this is where different levels of damage would be done
        }

        public void Update(GameTime gameTime)
        {
            enemy.TakeDamage();
            enemy.Move(position);
            
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
    }
}
