using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.CollisionHandling;
using Sprint0.States;
using Zelda.Enums;
using Sprint0.Display;
using System.Security.Cryptography;

namespace Sprint0.Collision
{
    public class EnemyLevelCollisionHandler : ICollisionHandler
    {
        const int BORDER_POSITION_VALUE = 5;
        const int RANDOM_NUMBER_INTERVAL = 2;

        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Enemy enemy = objA as Enemy;
            Level level = objB as Level;
            if (enemy == null || level == null) return;

            RandomlyChangeDirection(enemy, enemy.Direction);

            switch (side)
            {
                case CollisionSide.Left:
                    enemy.position = new Microsoft.Xna.Framework.Vector2(BORDER_POSITION_VALUE * enemy.GetEnemySize(true), enemy.position.Y);
                    break;
                case CollisionSide.Right:
                    if (enemy.enemyType == EnemyType.Gel)
                    {
                        enemy.position = new Microsoft.Xna.Framework.Vector2(level.roomWidth - BORDER_POSITION_VALUE, enemy.position.Y);
                    } else
                    {
                        enemy.position = new Microsoft.Xna.Framework.Vector2(level.roomWidth - BORDER_POSITION_VALUE, enemy.position.Y);
                    }
                    break;
                case CollisionSide.Top:
                    if (enemy.enemyType == EnemyType.Gel)
                    {
                        enemy.position = new Microsoft.Xna.Framework.Vector2(enemy.position.X, BORDER_POSITION_VALUE + (float)4 * enemy.GetEnemySize(false));
                    }
                    else
                    {
                        enemy.position = new Microsoft.Xna.Framework.Vector2(enemy.position.X, BORDER_POSITION_VALUE + (float)1.8 * enemy.GetEnemySize(false));
                    }
                    break;
                case CollisionSide.Bottom:
                    enemy.position = new Microsoft.Xna.Framework.Vector2(enemy.position.X, level.roomHeight + 75);
                    break;
            }
        }

        public void RandomlyChangeDirection(Enemy enemy, Direction oldDirection)
        {
            int random = RandomNumberGenerator.GetInt32(RANDOM_NUMBER_INTERVAL);
            switch(oldDirection)
            {
                case Direction.Left:
                    switch (random)
                    {
                        case 0:
                            enemy.ChangeDirection(Direction.Right);
                            break;
                        case 1:
                            enemy.ChangeDirection(Direction.Up);
                            break;
                        case 2:
                            enemy.ChangeDirection(Direction.Down);
                            break;
                    }
                    break;
                case Direction.Right:
                    switch (random)
                    {
                        case 0:
                            enemy.ChangeDirection(Direction.Left);
                            break;
                        case 1:
                            enemy.ChangeDirection(Direction.Up);
                            break;
                        case 2:
                            enemy.ChangeDirection(Direction.Down);
                            break;
                    }
                    break;
                case Direction.Up:
                    switch (random)
                    {
                        case 0:
                            enemy.ChangeDirection(Direction.Right);
                            break;
                        case 1:
                            enemy.ChangeDirection(Direction.Left);
                            break;
                        case 2:
                            enemy.ChangeDirection(Direction.Down);
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.Down:
                    switch (random)
                    {
                        case 0:
                            enemy.ChangeDirection(Direction.Right);
                            break;
                        case 1:
                            enemy.ChangeDirection(Direction.Up);
                            break;
                        case 2:
                            enemy.ChangeDirection(Direction.Left);
                            break;
                    }
                    break;
            }
        }
    }
}
