using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Enums;

namespace Sprint0.CollisionHandling
{
    // Collision Handler for Link and Enemy Projectile collisions.
    public class PlayerEnemyProjectileCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Assume objA is Link and objB is an Enemy Projectile.
            Link link = objA as Link;

            //EnemyProjectile projectile = objB as EnemyProjectile;

            // if (link == null || projectile == null) return;

            // TODO: Apply damage to Link, trigger hit effects, any other logic
        }
    }
}
