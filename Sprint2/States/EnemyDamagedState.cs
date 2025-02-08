using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.States
{
    public class EnemyDamagedState : IEnemyState
    {
        private Enemy enemy;
        int timer = 1000;

        public EnemyDamagedState(Enemy enemy)
        {
            this.enemy = enemy;
            // construct sprite here
        }
        public void Load(SpriteBatch spriteBatch)
        {
            enemy.Load(spriteBatch);
        }

        public void Move(Enemy enemy)
        {
            enemy.Move(enemy);
        }

        public void TakeDamage()
        {
            // change health status
            // i don't think the upgraded swords will be added, but this is where different levels of damage would be done
        }

        public void Update(GameTime gameTime)
        {
            enemy.TakeDamage();
            enemy.Move(enemy);
            
            timer--;
            if (timer == 0)
            {
                RemoveDecorator();
            }

            enemy.Update(gameTime);
            
            // call move but also find out when this state will stop (after a certain number of frames probs)
        }

        void RemoveDecorator()
        {
            // game.Link = decoratedLink; in example, figure out this.game logistics first
            new EnemyMovingState(enemy);
        }
    }
}
