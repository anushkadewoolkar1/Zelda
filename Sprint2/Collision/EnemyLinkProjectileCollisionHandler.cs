using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.Collision;
using MainGame.CollisionHandling;
using MainGame.Sprites;
using MainGame.States;
using Zelda.Enums;

namespace MainGame.CollisionHandling
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
            switch(linkProjectile.ReturnCurrentProjectile())
            {
                case ItemType.Arrow:
                    enemy.TakeDamage(ItemType.Arrow);
                    break;
                case ItemType.Boomerang:
                    enemy.TakeDamage(ItemType.Boomerang);
                    break;
                case ItemType.Bomb:
                    enemy.TakeDamage(ItemType.Bomb);
                    break;
            }

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
