using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.CollisionHandling;
using MainGame.States;
using Zelda.Enums;
using MainGame.Display;
using Microsoft.Xna.Framework;

namespace MainGame.Collision
{
    public class LinkLevelCollisionHandler : ICollisionHandler
    {
        const int BORDER_POSITION_VALUE = 110;

        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Link link = objA as Link;
            LevelManager level = objB as LevelManager;
            if (link == null || level == null) return;

            switch (side)
            {
                case CollisionSide.Left:
                    link.Position = new Vector2(
                        level.BoundingBox.Left + (link.BoundingBox.Width / 2f),
                        link.Position.Y);
                    break;

                case CollisionSide.Right:
                    link.Position = new Vector2(
                        level.BoundingBox.Right - (link.BoundingBox.Width / 2f),
                        link.Position.Y);
                    break;

                case CollisionSide.Top:
                    link.Position = new Vector2(
                        link.Position.X,
                        level.BoundingBox.Top + (link.BoundingBox.Height / 2f));
                    break;

                case CollisionSide.Bottom:
                    link.Position = new Vector2(
                        link.Position.X,
                        level.BoundingBox.Bottom - (link.BoundingBox.Height / 2f));
                    break;
            }
        }
    }
}
