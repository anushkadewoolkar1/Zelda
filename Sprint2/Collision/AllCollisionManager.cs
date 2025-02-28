using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.CollisionHandling;
using Sprint0.Sprites;
using Sprint0.States;
using Zelda.Enums;

namespace Sprint0.Collision
{
    // The AllCollisionManager central dispatcher.
    public class AllCollisionManager
    {
        // Constructor needed (PP):
        public AllCollisionManager() {}

        /// Dispatches collision responses based on the types of the colliding objects.
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Example: If objA is Link and objB is a Block.
            if (objA is Link && objB is Block)
            {
                ICollisionHandler handler = new LinkBlockCollisionHandler();
                handler.HandleCollision(objA, objB, side);
            }
            // Example: If objA is Link and objB is an Item.
            else if (objA is Link && objB is ItemSprite)
            {
                ICollisionHandler handler = new PlayerItemCollisionHandler();
                handler.HandleCollision(objA, objB, side);
            }
            // Example: If objA is an Enemy and objB is a Block.
            else if (objA is Enemy && objB is Block)
            {
                ICollisionHandler handler = new EnemyBlockCollisionHandler();
                handler.HandleCollision(objA, objB, side);
            }
            // Example: If objA is Link and objB is an Enemy Projectile.
            else if (objA is Link && objB is EnemyProjectile)
            {
                ICollisionHandler handler = new LinkEnemyProjectileCollisionHandler();
                handler.HandleCollision(objA, objB, side);
            }
            else
            {
                Console.WriteLine($"Unhandled collision between {objA.GetType().Name} and {objB.GetType().Name} on side: {side}");
            }
        }
    }
}

