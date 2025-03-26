using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.CollisionHandling;
using Sprint0.States;
using Zelda.Enums;
using Sprint0.Display;

namespace Sprint0.Collision
{
    public class LinkLevelCollisionHandler : ICollisionHandler
    {
        const int BORDER_POSITION_VALUE = 110;

        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Link link = objA as Link;
            Level level = objB as Level;
            if (link == null || level == null) return;

            switch (side)
            {
                // this corresponds to the left side of the level..etc
                case CollisionSide.Left:
                    link.Position = new Microsoft.Xna.Framework.Vector2(level.BoundingBox.X, link.Position.Y);
                    break;
                case CollisionSide.Right:
                    link.Position = new Microsoft.Xna.Framework.Vector2(level.BoundingBox.X + level.BoundingBox.Width - link.BoundingBox.Width, link.Position.Y);
                    break;
                case CollisionSide.Bottom:
                    link.Position = new Microsoft.Xna.Framework.Vector2(link.Position.X, level.BoundingBox.Y + level.BoundingBox.Height - link.BoundingBox.Height);
                    break;
                case CollisionSide.Top:
                    link.Position = new Microsoft.Xna.Framework.Vector2(link.Position.X, level.BoundingBox.Y);
                    break;
            }
        }
    }
}
