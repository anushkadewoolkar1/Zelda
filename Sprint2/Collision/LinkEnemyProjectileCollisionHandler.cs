using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Collision;
using Sprint0.CollisionHandling;
using Zelda.Enums;

namespace Sprint0.CollisionHandling
{
    // Collision Handler for Link and Enemy Projectile collisions.
    public class LinkEnemyProjectileCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Assume objA is Link and objB is an Enemy Projectile.
            Link link = objA as Link;
            EnemyProjectile enemyProj = objB as EnemyProjectile;

            if (link == null || enemyProj == null)
            {
                link = objB as Link;
                enemyProj = objA as EnemyProjectile;
            }

            if (link == null || enemyProj == null)
            { 
            System.Diagnostics.Debug.WriteLine("LinkEnemyProjectileCollisionHandler: Collision objects not valid.");
            return;
            }

            if (link.IsInvulnerable) return;

            link.TakeDamage(1);
            // Remove projectile once that method is implemented
            //EnemyProjectile.Destroy();

            // TODO: Apply damage to Link, trigger hit effects, any other logic
            System.Diagnostics.Debug.WriteLine("Link collided with an enemy projectile on side: " + side);
        }
    }
}
