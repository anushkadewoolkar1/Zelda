using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.CollisionHandling;
using Sprint0.States;
using Zelda.Enums;
using Sprint0.ILevel;

namespace Sprint0.Collision
{
    public class EnemyLevelCollisionHandler : ICollisionHandler
    {
        const int BORDER_POSITION_VALUE = 5;

        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Enemy enemy = objA as Enemy;
            Level level = objB as Level;
            if (enemy == null || level == null) return;

            switch (side)
            {
                case CollisionSide.Left:
                    enemy.ChangeDirection(Direction.Right);
                    enemy.Position = new Microsoft.Xna.Framework.Vector2(BORDER_POSITION_VALUE, link.Position.Y);
                    break;
                case CollisionSide.Right:
                    enemy.ChangeDirection(Direction.Left);
                    enemy.Position = new Microsoft.Xna.Framework.Vector2(level.roomWidth - BORDER_POSITION_VALUE, link.Position.Y);
                    break;
                case CollisionSide.Top:
                    enemy.ChangeDirection(Direction.Down);
                    enemy.Position = new Microsoft.Xna.Framework.Vector2(link.Position.X, BORDER_POSITION_VALUE);
                    break;
                case CollisionSide.Bottom:
                    enemy.ChangeDirection(Direction.Up);
                    enemy.Position = new Microsoft.Xna.Framework.Vector2(link.Position.X, level.roomHeight - BORDER_POSITION_VALUE);
                    break;
            }
        }
    }
}
