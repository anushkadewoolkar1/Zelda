using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using Zelda.Enums;


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
        }

        public EnemyType GetEnemy()
        {
            return stateMachine.GetEnemy();
        }

        public void ChangeEnemy()
        {
            stateMachine.ChangeEnemy();
        }

        public void SetHealth()
        {
            stateMachine.SetHealth();
        }
        
        public void Load(SpriteBatch spriteBatch)
        {
            switch (stateMachine.GetEnemy())
            {
                case EnemyType.Keese:
                    spriteFactory.CreateSmallEnemySprite(spriteBatch);
                    break;
                case EnemyType.Stalfos:
                    spriteFactory.CreateLargeEnemySprite(spriteBatch);
                    break;
            }
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
            stateMachine.Update(gameTime);

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
