using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.CollisionHandling;
using MainGame.States;
using Zelda.Enums;
using MainGame.Display;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;

namespace MainGame.Collision
{
    public class EnemyLevelCollisionHandler : ICollisionHandler
    {
        const int BORDER_POSITION_VALUE = 5;
        const int RANDOM_NUMBER_INTERVAL = 2;

        public void HandleCollision(IGameObject objA, IGameObject objB, CollisionSide side)
        {
            Enemy enemy = objA as Enemy;
            LevelManager level = objB as LevelManager;
            if (enemy == null || level == null) return;

            RandomlyChangeDirection(enemy, enemy.Direction);

            switch (side)
            {
                case CollisionSide.Left:
                    if (enemy.enemyType == EnemyType.Gel || enemy.enemyType == EnemyType.Keese)
                    {
                        enemy.position = new Vector2(level.BoundingBox.X, enemy.position.Y);
                    }
                    else
                    {
                        enemy.position = new Vector2(level.BoundingBox.X, enemy.position.Y);
                    }
                    break;
                case CollisionSide.Right:
                    if (enemy.enemyType == EnemyType.Gel || enemy.enemyType == EnemyType.Keese)
                    {
                        enemy.position = new Microsoft.Xna.Framework.Vector2(level.BoundingBox.X + level.BoundingBox.Width - enemy.BoundingBox.Width, enemy.position.Y);
                    }
                    else
                    {
                        enemy.position = new Microsoft.Xna.Framework.Vector2(level.BoundingBox.X + level.BoundingBox.Width - enemy.BoundingBox.Width, enemy.position.Y);
                    }
                    break;
                case CollisionSide.Top:
                    enemy.position = new Microsoft.Xna.Framework.Vector2(enemy.position.X, level.BoundingBox.Y);

                    break;
                case CollisionSide.Bottom:
                    enemy.position = new Microsoft.Xna.Framework.Vector2(enemy.position.X, level.BoundingBox.Y + level.BoundingBox.Height - enemy.BoundingBox.Height);
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
