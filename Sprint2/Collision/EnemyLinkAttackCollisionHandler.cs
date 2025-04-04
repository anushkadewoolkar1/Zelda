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
using Microsoft.Xna.Framework;

namespace Sprint0.CollisionHandling
{
    // Collision Handler for Enemy-Link projectiles
    public class EnemyLinkAttackCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Enemy enemy = objA as Enemy;
            HitBox hitbox = objB as HitBox;

            if (enemy == null || !(hitbox is HitBox))
            {
                enemy = objB as Enemy;
                hitbox = objA as HitBox;
            }

            if (enemy == null || !(hitbox is HitBox))
            {
                System.Diagnostics.Debug.WriteLine("EnemyLinkAttackCollisionHandler: Could not identify enemy or Link projectile.");
                return;
            }

            System.Diagnostics.Debug.WriteLine("Enemy hit by link's attack!");


            enemy.TakeDamage(ItemType.WoodenSword);
            
            //Handles Link's attack knockback on enemies
            switch (hitbox.Velocity.X)
            {
                case -1:
                    enemy.position = new Vector2(enemy.position.X - hitbox.scalePropertyHit, enemy.position.Y);
                    break;
                case 1:
                    enemy.position = new Vector2(enemy.position.X + hitbox.scalePropertyHit, enemy.position.Y);
                    break;
                case 0:
                    if (hitbox.Velocity.Y < 0)
                        enemy.position = new Vector2(enemy.position.X, enemy.position.Y + hitbox.scalePropertyHit);
                    else
                        enemy.position = new Vector2(enemy.position.X, enemy.position.Y - hitbox.scalePropertyHit);
                    break;
            }
        }
    }
}
