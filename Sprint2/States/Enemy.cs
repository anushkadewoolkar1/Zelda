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
        private Enemy enemy;
        private EnemySprite sprite;
        private EnemySpriteFactory spriteFactory;
        private EnemyStateMachine stateMachine;
        public Vector2 position;

        public Enemy()
        {
            spriteFactory = EnemySpriteFactory.Instance;
            position = new Vector2(500, 250);

            sprite = EnemySpriteFactory.Instance.CreateNPCSprite();
            // stateMachine = new EnemyMovingState(enemy);
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
        
        public void Load()
        {
            switch (stateMachine.GetEnemy())
            {
                case EnemyType.Keese:
                    spriteFactory.CreateSmallEnemySprite();
                    break;
                case EnemyType.Stalfos:
                    spriteFactory.CreateLargeEnemySprite();
                    break;
            }
        }


        public void Move(Vector2 position)
        {
            stateMachine.Moving(position);
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

        }
    }
}
