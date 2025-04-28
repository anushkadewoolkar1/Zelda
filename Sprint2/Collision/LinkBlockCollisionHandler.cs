using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MainGame.CollisionHandling;
using Zelda.Enums;

public class LinkBlockCollisionHandler : ICollisionHandler
{
    public void HandleCollision(IGameObject linkObject, IGameObject blockObject, CollisionSide collisionSide)
    {
        Link link = linkObject as Link;
        Block block = blockObject as Block;
        if (link == null || block == null)
            return;

        // Use velocity to further refine the response
        // For example, if Link is moving right and collides with a block on the left,
        // adjust Link's position so he is not intersecting.
        // Implement BoundingBox on Link and Block classes
        
        switch (collisionSide)
        {
            case CollisionSide.Left:
                link.Position = new Vector2(
                    block.BoundingBox.Left - (link.BoundingBox.Width / 2f), 
                    link.Position.Y);
                break;
            case CollisionSide.Right:
                link.Position = new Vector2(
                    block.BoundingBox.Right + (link.BoundingBox.Width / 2f),
                    link.Position.Y);
                break;
            case CollisionSide.Top:
                link.Position = new Vector2(
                    link.Position.X, 
                    block.BoundingBox.Top - (link.BoundingBox.Height /  2f));
                break;
            case CollisionSide.Bottom:
                link.Position = new Vector2(
                    link.Position.X, 
                    block.BoundingBox.Bottom + (link.BoundingBox.Height / 2f));
                break;
            default:
                break;
        }

        if (block.needKey && link.CurrentItem.Contains(ItemType.Key))
        {
            link.CurrentItem.Remove(ItemType.Key);
        } else if (block.needKey)
        {
            return;
        }
        block.LoadRoom();

    }
}
