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
        public float Speed;

        public Enemy()
        {
            spriteFactory = EnemySpriteFactory.Instance;
            position = new Vector2(500, 250);

            enemyType = EnemyType.OldMan;
            this.sprite = spriteFactory.CreateEnemySprite(enemyType);
            this.sprite.spriteSize = 32;


            enemyState = new EnemyMovingState(this);
            enemyState.Load();

        }

        public EnemyType GetEnemy()
        {
            return enemyType;
        }

        public void ChangeEnemyBackward()
        {
            switch (enemyType)
            {
                case EnemyType.OldMan:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Dodongo;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Keese:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.OldMan;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Stalfos:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Keese;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Gel:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Stalfos;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Zol:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Gel;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Goriya:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Zol;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Trap:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Goriya;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Wallmaster:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Trap;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Rope:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Wallmaster;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Aquamentus:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Rope;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Dodongo:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Aquamentus;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                default:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.OldMan;
                    this.sprite = spriteFactory.CreateNPCSprite();
                    break;
            }
        }

        public void ChangeEnemyForward()
        {
            switch (enemyType)
            {
                case EnemyType.OldMan:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Keese;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Keese:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Stalfos;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Stalfos:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Gel;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Gel:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Zol;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Zol:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Goriya;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Goriya:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Trap;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Trap:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Wallmaster;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Wallmaster:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Rope;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Rope:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Aquamentus;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Aquamentus:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.Dodongo;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                case EnemyType.Dodongo:
                    position = new Vector2(500, 250);
                    this.enemyType = EnemyType.OldMan;
                    this.sprite = spriteFactory.CreateEnemySprite(this.enemyType);
                    break;
                default:
                    position = new Vector2(500, 250);
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

        public void Move(Vector2 move, GameTime gameTime)
        {
            if (move != Vector2.Zero)
                move.Normalize();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.position += move * Speed * dt;

        }

        public void TakeDamage()
        {
            
        }

        public void Update(GameTime gameTime)
        {

            enemyState.Update(gameTime);
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
