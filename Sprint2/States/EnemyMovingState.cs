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
    public class EnemyMovingState : IEnemyState
    {
        private Enemy enemy;
        private SpriteBatch spriteBatch;
        private Vector2 position;

        public EnemyMovingState(Enemy enemy)
        {
            this.enemy = enemy;
            // construct enemy's sprite here
            ISprite enemySprite = EnemySpriteFactory.Instance.CreateSmallEnemySprite();
        }

        public void Load()
        {
            // sprite work done here
        }

        public void Update(GameTime gameTime)
        {
            // vector movement done here

            switch(enemy.GetEnemy())
            {
                case EnemyType.OldMan:
                    // no-op: OldMan NPC doesn't need to move
                    break;
                case EnemyType.Keese:
                    
                    break;


            }

            enemy.Move(position);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemy.DrawCurrentSprite(spriteBatch);
        }

        public void Stop()
        {
            // no-op
        }
    }
}
