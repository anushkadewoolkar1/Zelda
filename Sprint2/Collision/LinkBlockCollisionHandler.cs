using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint0.CollisionHandling;
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
                // Push Link to the left.
                link.Position = new Vector2(block.BoundingBox.Left - link.BoundingBox.Width, link.Position.Y);
                break;
            case CollisionSide.Right:
                link.Position = new Vector2(block.BoundingBox.Right, link.Position.Y);
                break;
            case CollisionSide.Top:
                link.Position = new Vector2(link.Position.X, block.BoundingBox.Top - link.BoundingBox.Height);
                break;
            case CollisionSide.Bottom:
                link.Position = new Vector2(link.Position.X, block.BoundingBox.Bottom);
                break;
            default:
                break;
        }

        block.LoadRoom();

    }
}
