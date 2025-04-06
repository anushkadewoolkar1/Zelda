using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.CollisionHandling;
using MainGame.States;
using Zelda.Enums;

namespace MainGame.Collision
{
    public class LinkEnemyCollisionHandler : ICollisionHandler
    {
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Link link = objA as Link;
            Enemy enemy = objB as Enemy;
            if(link == null || enemy == null) return;
            if (link.IsInvulnerable) return;

            // link takes damage, enemies will immediately walk the opposite direction
            // (doesn't matter whether they continue to walk in that direction or not tho)
            link.TakeDamage(1);
            link.StartInvulnerability();

            switch (side)
            {
                case CollisionSide.Left:
                    enemy.ChangeDirection(Direction.Right);
                    link.Position = new Microsoft.Xna.Framework.Vector2(link.Position.X - 38, link.Position.Y);
                    break;
                case CollisionSide.Right:
                    enemy.ChangeDirection(Direction.Left);
                    link.Position = new Microsoft.Xna.Framework.Vector2(link.Position.X + 38, link.Position.Y);
                    break;
                case CollisionSide.Top:
                    enemy.ChangeDirection(Direction.Down);
                    link.Position = new Microsoft.Xna.Framework.Vector2(link.Position.X, link.Position.Y - 38);
                    break;
                case CollisionSide.Bottom:
                    enemy.ChangeDirection(Direction.Up);
                    link.Position = new Microsoft.Xna.Framework.Vector2(link.Position.X, link.Position.Y + 38);
                    break;
            }


        }
    }
}
