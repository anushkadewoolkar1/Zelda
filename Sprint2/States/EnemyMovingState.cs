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
    public class EnemyMovingState : IEnemyState
    {
        private Enemy enemy;
        private SpriteBatch spriteBatch;

        public EnemyMovingState(Enemy enemy)
        {
            this.enemy = enemy;
            // construct enemy's sprite here
            ISprite enemySprite = EnemySpriteFactory.Instance.CreateSmallEnemySprite(spriteBatch);
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
            // change to EnemyDamagedState
        }

        public void Update(GameTime gameTime)
        {
            // move the sprite however the enemy moves (probably do a switch case here)

            enemy.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemy.DrawCurrentSprite(spriteBatch);
        }
    }
}
