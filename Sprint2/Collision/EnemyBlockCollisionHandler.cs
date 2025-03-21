using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint0.States;
using Zelda.Enums;

namespace Sprint0.CollisionHandling
{
    // Collision Handler for Enemy and Block collisions.
    public class EnemyBlockCollisionHandler : ICollisionHandler
    {
        private const int RANDOM_NUMBER_INTERVAL = 2;

        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Assume objA is an Enemy and objB is a Block.
            Enemy enemy = objA as Enemy;
            Block block = objB as Block;
            if (enemy == null || block == null) return;

            RandomlyChangeDirection(enemy, enemy.Direction);

            switch (side)
            {
                case CollisionSide.Left:
                    enemy.position = new Vector2(block.BoundingBox.Left - enemy.BoundingBox.Width, enemy.position.Y);
                    break;
                case CollisionSide.Right:
                    enemy.position = new Vector2(block.BoundingBox.Right, enemy.position.Y);
                    break;
                case CollisionSide.Top:
                    enemy.position = new Vector2(enemy.position.X, block.BoundingBox.Top - enemy.GetEnemySize(false));
                    break;
                case CollisionSide.Bottom:
                    enemy.position = new Vector2(enemy.position.X, block.BoundingBox.Bottom);
                    break;
            }
        }

        public void RandomlyChangeDirection(Enemy enemy, Direction oldDirection)
        {
            int random = RandomNumberGenerator.GetInt32(RANDOM_NUMBER_INTERVAL);
            switch (oldDirection)
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
