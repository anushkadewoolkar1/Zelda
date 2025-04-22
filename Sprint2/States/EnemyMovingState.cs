using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.Sprites;
using Zelda.Enums;

namespace MainGame.States
{
    public class EnemyMovingState : IEnemyState
    {
        private ISprite projectileSprite;
        private Enemy enemy;
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Direction Direction;
        private Boolean moving = true;

        // constants

        private const int SMALL_ENEMY_SPEED = 20;
        private const int EIGTH_TIMER = 25;
        private const int LARGE_ENEMY_SPEED = 30;
        private const int FOURTH_TIMER = 50;
        private const int THREE_EIGHT_TIMER = 75;
        private const int HALF_TIMER = 100;
        private const int MAX_TIMER = 200;
        private const int MAX_SPEED = 200;


        private int timer = 0;

        public EnemyMovingState(Enemy enemy)
        {
            this.enemy = enemy;

        }


        public void Load()
        {
            if (enemy.enemyType == EnemyType.OldMan)
            {
                ISprite npcSprite = EnemySpriteFactory.Instance.CreateNPCSprite();
            }
            else
            {
                ISprite enemySprite = EnemySpriteFactory.Instance.CreateEnemySprite(enemy.enemyType, enemy.Direction);
            }
        }

        public void Update(GameTime gameTime)
        {
            Vector2 move = Vector2.Zero;

            enemy.Speed = HALF_TIMER;

            int random = RandomNumberGenerator.GetInt32(-1, 1);
            Direction randomDirection = GenerateRandomDirection();

            switch (enemy.enemyType)
            {
                case EnemyType.OldMan:
                    // no-op: OldMan NPC doesn't need to move
                    enemy.Speed = 0;
                    break;
                case EnemyType.Keese:
                    enemy.Speed = SMALL_ENEMY_SPEED;
                    if(timer == HALF_TIMER)
                    {
                        enemy.Direction = randomDirection;
                    }

                    if (enemy.Direction == Direction.Up || enemy.Direction == Direction.Down)
                    {
                        move.X = random;
                        move = MoveDirection(enemy.Direction);
                    } else
                    {
                        move.Y = random;
                        move = MoveDirection(enemy.Direction);
                    }

                    break;
                case EnemyType.Stalfos:
                    enemy.Speed = LARGE_ENEMY_SPEED;
                    if(timer == 0 || timer == HALF_TIMER)
                    {
                        enemy.Direction = randomDirection;
                    }

                    move = MoveDirection(enemy.Direction);
                    break;
                case EnemyType.Goriya:
                    if(timer == 0 || timer == HALF_TIMER)
                    {
                        moving = true;
                        enemy.ChangeDirection(randomDirection);
                    } else if (timer == FOURTH_TIMER || timer == (HALF_TIMER + FOURTH_TIMER))
                    {
                        enemy.SpawnProjectile();
                        moving = false;
                    }

                    if (moving)
                    {
                        move = MoveDirection(enemy.Direction);
                    }
                    else
                    {
                        move = Vector2.Zero;
                    }
                    break;
                case EnemyType.Gel:
                    enemy.Speed = SMALL_ENEMY_SPEED;
                    if ((timer >= 0 && timer < FOURTH_TIMER) || (timer >= HALF_TIMER && timer < (HALF_TIMER + FOURTH_TIMER)))
                    {
                        move = new Vector2(0, 0);
                    } else if (timer == THREE_EIGHT_TIMER)
                    {
                        enemy.ChangeDirection(randomDirection);
                    } else
                    {
                        move = MoveDirection(enemy.Direction);
                    }
                    break;
                case EnemyType.Zol:
                    if ((timer >= 0 && timer < FOURTH_TIMER) || (timer >= HALF_TIMER && timer < (HALF_TIMER + FOURTH_TIMER)))
                    {
                        move = new Vector2(0, 0);
                    }
                    else
                    {
                        move = MoveDirection(enemy.Direction);
                    }
                    break;
                case EnemyType.Trap:
                    if (timer >= 0 && timer < EIGTH_TIMER)
                    {
                        enemy.Speed = 0;
                        move = new Vector2(0, 0);
                    }
                    else if (timer >= EIGTH_TIMER && timer < HALF_TIMER)
                    {
                        enemy.Speed = MAX_SPEED;
                        move = MoveDirection(Direction.Down);
                    }
                    else if (timer >= FOURTH_TIMER && timer < HALF_TIMER)
                    {
                        enemy.Speed = HALF_TIMER;
                        move = MoveDirection(Direction.Up);
                    }
                    break;
                case EnemyType.Wallmaster:
                    enemy.Speed = FOURTH_TIMER;
                    
                    move = new Vector2(RandomNumberGenerator.GetInt32(-1, 2), RandomNumberGenerator.GetInt32(-1, 2));
                    
                    break;
                case EnemyType.Rope:
                    // set to a timer in sprint2, rope's double movement speed needs to be triggered by lining up with link, not doing it eveyr once in a while
                    if (timer >= 0 && timer < FOURTH_TIMER)
                    {
                        if (RandomNumberGenerator.GetInt32(-1, 1) == 1)
                        {
                            move = MoveDirection(Direction.Right);
                        } else
                        {
                            move = MoveDirection(Direction.Left);
                        }
                    } else if (timer >= FOURTH_TIMER && timer < THREE_EIGHT_TIMER)
                    {
                        enemy.Speed = MAX_SPEED;
                        move = MoveDirection(Direction.Down);
                    } else
                    {
                        enemy.Speed = HALF_TIMER;
                        if (RandomNumberGenerator.GetInt32(-1, 1) == 1)
                        {
                            move = MoveDirection(Direction.Right);
                        }
                        else
                        {
                            move = MoveDirection(Direction.Left);
                        }
                    }
                    break;
                case EnemyType.Aquamentus:
                    enemy.itemType = ItemType.Fireball;
                    if (timer >= 0 && timer < FOURTH_TIMER)
                    {
                        move = MoveDirection(Direction.Left);
                    } else if (timer >= FOURTH_TIMER && timer < HALF_TIMER)
                    {
                        move = MoveDirection(Direction.Right);
                    } else if (timer == HALF_TIMER)
                    {
                        enemy.SpawnProjectile();
                    }
                    break;
                case EnemyType.Dodongo:
                    enemy.Speed = FOURTH_TIMER;
                    if (timer == 0)
                    {
                        enemy.ChangeDirection(Direction.Right);
                    }
                    else if (timer == EIGTH_TIMER)
                    {
                        enemy.ChangeDirection(Direction.Up);
                    }
                    else if (timer == FOURTH_TIMER)
                    {
                        enemy.ChangeDirection(Direction.Left);
                    }
                    else if (timer == THREE_EIGHT_TIMER)
                    {
                        enemy.ChangeDirection(Direction.Down);
                    }

                    move = MoveDirection(enemy.Direction);
                    break;
                default:
                    move = new Vector2(0, 1); ;
                    break;
            }

            timer++;
            if (timer == (MAX_TIMER))
            {
                timer = 0;
            }

            enemy.Move(move, gameTime);
        }

        public Direction GenerateRandomDirection()
        {
            int random = RandomNumberGenerator.GetInt32(0, 4);
            switch(random)
            {
                case 0:
                    return Direction.Left;
                case 1:
                    return Direction.Right;
                case 2:
                    return Direction.Up;
                case 3:
                    return Direction.Down;
                default:
                    return Direction.Left;
            }
        }

        public Vector2 MoveDirection(Direction dir)
        {
            switch(dir)
            {
                case Direction.Up:
                    return new Vector2(0, -1);
                case Direction.Down:
                    return new Vector2(0, 1);
                case Direction.Left:
                    return new Vector2(1, 0);
                case Direction.Right:
                    return new Vector2(-1, 0);
                default:
                    return new Vector2(0, 0);
            }
        }

        public void Stop()
        {
            // no-op
        }
    }
}
