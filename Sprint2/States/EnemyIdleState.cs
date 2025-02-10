using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.States
{
    public class EnemyIdleState : IEnemyState
    {
        
        private Enemy enemy;

        public EnemyIdleState(Enemy enemy)
        {
            this.enemy = enemy;
            // construct sprite here
        }

        public void Load(SpriteBatch spriteBatch)
        {
            enemy.Load(spriteBatch);
        }

        public void Move(Enemy enemy)
        {
            // NO-OP
            // idle
        }

        public void TakeDamage()
        {
            // NO-OP
            // pretty sure this is gonna be used for NPCs, i don't think they take any damage
        }

        public void Update(GameTime gameTime)
        {
            // trigger non-moving animation

            enemy.Update(gameTime);
        }
    }
}
