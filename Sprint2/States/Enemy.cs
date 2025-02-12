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

            enemyType = EnemyType.OldMan;
            this.sprite = spriteFactory.CreateNPCSprite();


            enemyState = new EnemyIdleState(this);

        }

        public EnemyType GetEnemy()
        {
            return enemyType;
        }

        public void ChangeEnemy()
        {
            switch (enemyType)
            {
                case EnemyType.OldMan:
                    enemyType = EnemyType.Keese;
                    break;
                case EnemyType.Keese:
                    enemyType = EnemyType.Stalfos;
                    break;
                case EnemyType.Stalfos:
                    // change when more enemies are implemented
                    enemyType = EnemyType.OldMan;
                    break;
                default:
                    enemyType = EnemyType.OldMan;
                    break;
            }
        }

        public void SetHealth()
        {
            switch (enemyType)
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

        public void DrawCurrentSprite(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, this.position);
        }

        public void ChangeState(IEnemyState newState)
        {
            enemyState.Stop();
            enemyState = newState;
            enemyState.Load();

        }
    }
}
