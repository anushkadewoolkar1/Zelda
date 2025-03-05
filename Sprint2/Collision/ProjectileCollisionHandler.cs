using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;
using Zelda.Enums;
using Sprint0.States;

namespace Sprint0.CollisionHandling
{
    // Collision Handler for Link and Item collisions.
    public class ProjectileCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            if (objB is Enemy)
            {
                objA.Destroy();
                objB.Destroy();
            }
        }
    }
}
