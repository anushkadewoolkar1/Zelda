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
    public class Enemy
    {
        public Enemy currentEnemy;
        public IEnemyState enemyState;
        public double EnemyHealth;
        public EnemySprite sprite;
        public EnemySpriteFactory spriteFactory;
        public Vector2 position;
        public EnemyType enemyType;

        public Enemy()
        {
            spriteFactory = EnemySpriteFactory.Instance;
            position = new Vector2(500, 250);
            
            currentEnemy.GetEnemy();
            spriteFactory.CreateNPCSprite();


            enemyState = new EnemyMovingState(currentEnemy);

        }

        public EnemyType GetEnemy()
        {
            return currentEnemy.enemyType;
        }

        public void ChangeEnemy()
        {
            switch (enemyType)
            {
                case EnemyType.OldMan:
                    currentEnemy.enemyType = EnemyType.Keese;
                    break;
                case EnemyType.Keese:
                    currentEnemy.enemyType = EnemyType.Stalfos;
                    break;
                case EnemyType.Stalfos:
                    // change when more enemies are implemented
                    currentEnemy.enemyType = EnemyType.OldMan;
                    break;
                default:
                    currentEnemy.enemyType = EnemyType.OldMan;
                    break;
            }
        }

        public void SetHealth()
        {
            switch (currentEnemy.enemyType)
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

        public void Move(Vector2 position)
        {
            
        }

        public void TakeDamage()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public void DrawCurrentSprite(SpriteBatch spriteBatch, Vector2 position)
        {
            sprite.Draw(spriteBatch, position);
        }

        public void ChangeState(IEnemyState newState)
        {
            enemyState.Stop();
            enemyState = newState;
            enemyState.Load();

        }
    }
}
