using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Sprites;
using Zelda.Enums;

namespace Sprint0.CollisionHandling
{
    // Collision Handler for Link and Item collisions.
    public class PlayerItemCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Assume objA is Link and objB is an Item.
            Link link = objA as Link;
            ItemSprite item = objB as ItemSprite;
            if (link == null || item == null) return;

            // TODO: Implement item pickup logic.
          
        }
    }
}
