using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.States
{
    public class Enemy : IEnemyState
    {
        private EnemySprite sprite;
        private EnemySpriteFactory spriteFactory;
        private EnemyStateMachine stateMachine;

        public Enemy()
        {
            // start out in moving state
            stateMachine = new EnemyStateMachine();
        }

        public void ChangeEnemy()
        {
            stateMachine.ChangeEnemy();
        }

        public double SetHealth()
        {
            // do switch case here based on what the enemy's name is (similar to GetState method methinks)
            return 0;
        }
        
        public void Load(SpriteBatch spriteBatch)
        {
            sprite = spriteFactory.CreateEnemySprite();
        }


        public void Move(Enemy enemy)
        {
            stateMachine.Moving(enemy);
        }

        public void TakeDamage()
        {
            stateMachine.TakeDamage();
        }

        public void Update(GameTime gameTime)
        {
            stateMachine.Update();

            sprite.Update(gameTime);
        }

        public void DrawCurrentSprite(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public void ChangeState(EnemyStateMachine enemyState)
        {
            stateMachine.Stop();
            stateMachine = enemyState;
            // stateMachine.Load(sprite);

        }
    }
}
