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
        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            // Assume objA is an Enemy and objB is a Block.
            Enemy enemy = objA as Enemy;
            Block block = objB as Block;
            if (enemy == null || block == null) return;

            switch(side)
            {
                case CollisionSide.Left:
                    int random = RandomNumberGenerator.GetInt32(-1, 1);
                    if(random == 1)
                    {
                        enemy.ChangeDirection(Direction.Down);
                    } else
                    {
                        enemy.ChangeDirection(Direction.Up);
                    }

                    enemy.position = new Vector2(block.BoundingBox.Left - enemy.BoundingBox.Width, enemy.position.Y);
                    break;
                case CollisionSide.Right:
                    random = RandomNumberGenerator.GetInt32(-1, 1);
                    if (random == 1)
                    {
                        enemy.ChangeDirection(Direction.Down);
                    }
                    else
                    {
                        enemy.ChangeDirection(Direction.Up);
                    }

                    enemy.position = new Vector2(block.BoundingBox.Right, enemy.position.Y);
                    break;
                case CollisionSide.Top:
                    random = RandomNumberGenerator.GetInt32(-1, 1);
                    if (random == 1)
                    {
                        enemy.ChangeDirection(Direction.Down);
                    }
                    else
                    {
                        enemy.ChangeDirection(Direction.Up);
                    }

                    enemy.position = new Vector2(enemy.position.X, block.BoundingBox.Top - enemy.position.Y);
                    break;
                case CollisionSide.Bottom:
                    random = RandomNumberGenerator.GetInt32(-1, 1);
                    if (random == 1)
                    {
                        enemy.ChangeDirection(Direction.Down);
                    }
                    else
                    {
                        enemy.ChangeDirection(Direction.Up);
                    }

                    enemy.position = new Vector2(enemy.position.X, block.BoundingBox.Bottom);
                    break;
            }
        }
    }
}
