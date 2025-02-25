using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.States;
using Zelda.Enums;

namespace Sprint0.CollisionHandling
{
    // Collision Handler for Enemy and Block collisions.
    public class EnemyBlockCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Assume objA is an Enemy and objB is a Block.
            Enemy enemy = objA as Enemy;
            Block block = objB as Block;
            if (enemy == null || block == null) return;

            // TODO: Implement enemy response logic.
        }
    }
}
