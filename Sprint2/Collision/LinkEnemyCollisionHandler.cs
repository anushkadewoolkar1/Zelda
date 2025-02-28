using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.CollisionHandling;
using Sprint0.States;
using Zelda.Enums;

namespace Sprint0.Collision
{
    public class LinkEnemyCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Link link = objA as Link;
            Enemy enemy = objB as Enemy;
            if(link == null || enemy == null) return;

            // link takes damage, enemies will immediately walk the opposite direction (doesn't matter whether they continue to walk in that direction or not tho)

            // start the invulnerability, change later to TakeDamage() method like in another collision case if that's added
            link.StartInvulnerability();

            switch(side)
            {
                case CollisionSide.Left:
                    enemy.ChangeDirection(Direction.Right);
                    break;
                case CollisionSide.Right:
                    enemy.ChangeDirection(Direction.Left);
                    break;
                case CollisionSide.Top:
                    enemy.ChangeDirection(Direction.Down);
                    break;
                case CollisionSide.Bottom:
                    enemy.ChangeDirection(Direction.Up);
                    break;
            }
        }
    }
}
