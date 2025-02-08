using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using Zelda.Enums;

namespace Sprint0.States
{
    public class EnemyStateMachine
    {
        private bool moving = true;
        private double EnemyHealth = 0.0;
        private enum Enemies { Bat, Skeleton };
        private Enemies currentEnemy;

        public void ChangeEnemy()
        {
            switch(currentEnemy)
            {
                case Enemies.Bat:
                    currentEnemy = Enemies.Skeleton;
                    break;
                case Enemies.Skeleton:
                    // change when more enemies are implemented
                    currentEnemy = Enemies.Bat;
                    break;
                default:
                    currentEnemy = Enemies.Bat;
                    break;
            }
        }

        // probably can be deleted later, i didn't wanna waste it just in case
        public Enemy GetEnemy(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Keese:
                // return new Bat();
                case EnemyType.Stalfos:
                // return new Skeleton();
                default:
                    throw new NotSupportedException();
            }
        }

        public void SetHealth()
        {
            switch(currentEnemy)
            {
                case Enemies.Bat:
                    EnemyHealth = 0.5;
                    break;
                case Enemies.Skeleton:
                    EnemyHealth = 2.0;
                    break;

            }
        }

        public void Moving(Enemy enemy)
        {
            EnemyMovingState moving = new EnemyMovingState(enemy);
            
        }

        public void TakeDamage()
        {
            EnemyHealth--;

            if(EnemyHealth <= 0.0)
            {
                
            }
        }

        public void Stop()
        {

        }

        public void Dead()
        {
            if(EnemyHealth <= 0.0)
            {
                // state = new EnemyDeadState();
            }
        }

        public void Update()
        {

        }
    }
}
