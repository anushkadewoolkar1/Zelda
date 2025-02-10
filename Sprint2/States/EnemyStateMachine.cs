using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using Zelda.Enums;

namespace Sprint0.States
{
    public class EnemyStateMachine
    {
        private double EnemyHealth;
        private SpriteBatch spriteBatch;
        EnemyStateMachine currentState;
        private EnemyMovingState enemyMoving;
        private EnemyIdleState enemyIdle;
        private EnemyDamagedState enemyDamaged;
        private EnemyType currentEnemy;

        public void ChangeEnemy()
        {
            switch(currentEnemy)
            {
                case EnemyType.OldMan:
                    currentEnemy = EnemyType.Keese;
                    break;
                case EnemyType.Keese:
                    currentEnemy = EnemyType.Stalfos;
                    break;
                case EnemyType.Stalfos:
                    // change when more enemies are implemented
                    currentEnemy = EnemyType.OldMan;
                    break;
                default:
                    currentEnemy = EnemyType.OldMan;
                    break;
            }
        }

        public EnemyType GetEnemy()
        {
            return currentEnemy;
        }

        public void SetHealth()
        {
            switch(currentEnemy)
            {
                case EnemyType.OldMan:
                    EnemyHealth = 99.0;
                    break;
                case EnemyType.Keese:
                    EnemyHealth = 0.5;
                    break;
                case EnemyType.Stalfos:
                    EnemyHealth = 2.0;
                    break;

            }
        }

        public void Moving(Enemy enemy)
        {
            enemyMoving.Load(spriteBatch);
            
        }

        public void TakeDamage()
        {
            enemyDamaged.Load(spriteBatch);
        }

        public void Stop()
        {

        }

        public void Update(GameTime gameTime)
        {

            currentState.Update(gameTime);
        }
    }
}
