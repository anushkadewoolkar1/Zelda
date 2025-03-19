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
                    enemy.position = new Microsoft.Xna.Framework.Vector2(BORDER_POSITION_VALUE, enemy.position.Y);
                    break;
                case CollisionSide.Right:
                    enemy.ChangeDirection(Direction.Left);
                    enemy.position = new Microsoft.Xna.Framework.Vector2(level.roomWidth - BORDER_POSITION_VALUE, enemy.position.Y);
                    break;
                case CollisionSide.Top:
                    enemy.ChangeDirection(Direction.Down);
                    enemy.position = new Microsoft.Xna.Framework.Vector2(enemy.position.X, BORDER_POSITION_VALUE);
                    break;
                case CollisionSide.Bottom:
                    enemy.ChangeDirection(Direction.Up);
                    enemy.position = new Microsoft.Xna.Framework.Vector2(enemy.position.X, level.roomHeight - BORDER_POSITION_VALUE);
                    break;
            }
        }
    }
}
