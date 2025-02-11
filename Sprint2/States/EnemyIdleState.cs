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
    public class EnemyIdleState : IEnemyState
    {
        
        private Enemy enemy;
        private SpriteBatch spriteBatch;
        private Vector2 position;

        public EnemyIdleState()
        {
            // construct sprite here
            ISprite enemySprite = EnemySpriteFactory.Instance.CreateSmallEnemySprite();
        }

        public void Load()
        {
            enemy.Load();
        }

        public void Update(GameTime gameTime)
        {
            // trigger non-moving animation

            enemy.Update(gameTime);
        }
    }
}
