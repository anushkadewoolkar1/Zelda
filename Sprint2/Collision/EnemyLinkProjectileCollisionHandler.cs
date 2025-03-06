using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Collision;
using Sprint0.CollisionHandling;
using Sprint0.Sprites;
using Sprint0.States;
using Zelda.Enums;

namespace Sprint0.CollisionHandling
{
    // Collision Handler for Enemy-Link projectiles
    public class EnemyLinkProjectileCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Enemy enemy = objA as Enemy;
            ProjectileSprite linkProjectile = objB as ProjectileSprite;

            if (enemy == null || !IsLinkProjectile(linkProjectile))
            {
                enemy = objB as Enemy;
                linkProjectile = objA as ProjectileSprite;
            }

            if (enemy == null || !IsLinkProjectile(linkProjectile))
            {
                System.Diagnostics.Debug.WriteLine("EnemyLinkProjectileCollisionHandler: Could not identify enemy or Link projectile.");
                return;
            }

            System.Diagnostics.Debug.WriteLine("Enemy hit by link's projectile!");

            // once implemented
            enemy.TakeDamage(linkProjectile);

            // This needs to call the destroy method in ProjectileSprite.cs
            // probably need to create a representation of linkprojectile just like the enemyprojectile
            linkProjectile.Destroy();
        }

        // Determines if given object is Link projectile
        private bool IsLinkProjectile(IGameObject obj)
        {
            return obj is ProjectileSprite;
        }
    }
}
