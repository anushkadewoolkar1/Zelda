using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Sprint0.States
{
    public class EnemyIdleState : IEnemyState
    {
        // might change things so this class doesn't exist and the death animation triggers in the EnemyDamagedState class, will get to later
        
        private Enemy enemy;

        public EnemyIdleState(Enemy enemy)
        {
            this.enemy = enemy;
            // construct sprite here
        }

        public void Move(Enemy enemy)
        {
            // NO-OP
            // dead
        }

        public void TakeDamage()
        {
            // NO-OP
            // dead
        }

        public void Die()
        {
            // NO-OP
            // currently dying
        }

        public void Update(GameTime gameTime)
        {
            // trigger animation for enemy death
        }
    }
}
