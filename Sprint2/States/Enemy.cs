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
                    this.enemyType = EnemyType.Keese;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Keese:
                    this.enemyType = EnemyType.Stalfos;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Stalfos:
                    this.enemyType = EnemyType.Gel;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Gel:
                    this.enemyType = EnemyType.Zol;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Zol:
                    this.enemyType = EnemyType.Goriya;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Goriya:
                    this.enemyType = EnemyType.OldMan;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                default:
                    this.enemyType = EnemyType.OldMan;
                    this.sprite = spriteFactory.CreateNPCSprite();
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
