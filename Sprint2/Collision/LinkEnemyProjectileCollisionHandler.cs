﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.Collision;
using MainGame.CollisionHandling;
using MainGame.Sprites;
using Zelda.Enums;

namespace MainGame.CollisionHandling
{
    // Collision Handler for Link and Enemy Projectile collisions.
    public class LinkEnemyProjectileCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Assume objA is Link and objB is an Enemy Projectile.
            Link link = objA as Link;
            ProjectileSprite enemyProj = objB as ProjectileSprite;

            if (link == null || enemyProj == null)
            {
                link = objB as Link;
                enemyProj = objA as ProjectileSprite;
            }

            if (link == null || enemyProj == null)
            { 
            System.Diagnostics.Debug.WriteLine("LinkEnemyProjectileCollisionHandler: Collision objects not valid.");
            return;
            }

            if (link.IsInvulnerable) return;

            if (enemyProj.isEnemyProjectile == false) return;

            link.TakeDamage(1);
            // Remove projectile once that method is implemented
            enemyProj.Destroy();

            // TODO: Apply damage to Link, trigger hit effects, any other logic
            System.Diagnostics.Debug.WriteLine("Link collided with an enemy projectile on side: " + side);
        }
    }
}
