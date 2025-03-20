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
    public class LinkItemCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Assume objA is Link and objB is an Item.
            Link link = objA as Link;
            Item item = objB as Item;
            if (link == null || item == null) return;

            // Do not need to go by case of side, as colliding with any item from any side should result in a pickup (PP):
            link.PickUpItem(item.itemSprite);
            item.Destroy();
        }
    }
}
