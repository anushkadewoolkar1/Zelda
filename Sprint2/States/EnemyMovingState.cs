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

        public EnemyMovingState(Enemy enemy)
        {
            this.enemy = enemy;
            // construct enemy's sprite here
            ISprite enemySprite = EnemySpriteFactory.Instance.CreateEnemySprite();
        }

        public void Load(SpriteBatch spriteBatch)
        {
            enemy.Load(spriteBatch);
        }

        public void Move(Enemy enemy)
        {
            // NO-OP
            // already moving
        }

        public void TakeDamage()
        {
            // enemy.state = new EnemyDamagedState(enemy, this.game);
        }

        public void Update(GameTime gameTime)
        {
            // enemy.Move();

            enemy.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemy.DrawCurrentSprite(spriteBatch);
        }
    }
}
