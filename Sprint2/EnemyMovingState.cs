using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using Zelda.Enums;

namespace Sprint0.States
{
    public class EnemyMovingState : IEnemyState
    {
        private ISprite projectileSprite;
        private Enemy enemy;
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private int timer = 0;
        public Direction Direction;

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

            enemy.Speed = 100f;

            int random = RandomNumberGenerator.GetInt32(-1, 2);

            switch (enemy.enemyType)
            {
                case EnemyType.OldMan:
                    // no-op: OldMan NPC doesn't need to move
                    enemy.Speed = 0;
                    break;
                case EnemyType.Keese:
                    enemy.Speed = 20f;
                    if (enemy.Direction == Direction.Up || enemy.Direction == Direction.Down)
                    {
                        move = MoveDirection(enemy.Direction);
                        move.X = random;
                    } else
                    {
                        move = MoveDirection(enemy.Direction);
                        move.Y = random;
                    }

                    break;
                case EnemyType.Stalfos:
                    enemy.Speed = 30f;
                    if(timer == 0 || timer == 50)
                    {
                        enemy.ChangeDirection((Direction)random);
                    }

                    move = MoveDirection(enemy.Direction);
                    break;
                case EnemyType.Goriya:
                    // refactor this later, this is pretty hard to look at tbh
                    Boolean moving = true;
                    if(timer == 0 || timer == 25 || timer == 50)
                    {
                        enemy.ChangeDirection((Direction)random);
                    } else if (timer == 75)
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
                    enemy.Speed = 20f;
                    if ((timer >= 0 && timer < 50) || (timer >= 100 && timer < 150))
                    {
                        move = new Vector2(0, 0);
                    } else
                    {
                        move = MoveDirection(enemy.Direction);
                    }
                    break;
                case EnemyType.Zol:
                    if ((timer >= 0 && timer < 50) || (timer >= 100 && timer < 150))
                    {
                        move = new Vector2(0, 0);
                    }
                    else
                    {
                        move = MoveDirection(enemy.Direction);
                    }
                    break;
                case EnemyType.Trap:
                    if (timer >= 0 && timer < 25)
                    {
                        enemy.Speed = 0;
                        move = new Vector2(0, 0);
                    }
                    else if (timer >= 25 && timer < 50)
                    {
                        enemy.Speed = 200f;
                        move = MoveDirection(Direction.Down);
                    }
                    else if (timer >= 50 && timer < 100)
                    {
                        enemy.Speed = 100f;
                        move = MoveDirection(Direction.Up);
                    }
                    break;
                case EnemyType.Wallmaster:
                    enemy.Speed = 50f;
                    
                    move = new Vector2(RandomNumberGenerator.GetInt32(-1, 2), RandomNumberGenerator.GetInt32(-1, 2));
                    
                    break;
                case EnemyType.Rope:
                    // set to a timer in sprint2, rope's double movement speed needs to be triggered by lining up with link, not doing it eveyr once in a while
                    if (timer >= 0 && timer < 50)
                    {
                        if (RandomNumberGenerator.GetInt32(-1, 1) == 1)
                        {
                            move = MoveDirection(Direction.Right);
                        } else
                        {
                            move = MoveDirection(Direction.Left);
                        }
                    } else if (timer >= 50 && timer < 75)
                    {
                        enemy.Speed = 200f;
                        move = MoveDirection(Direction.Down);
                    } else
                    {
                        enemy.Speed = 100f;
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
                    if (timer >= 0 && timer < 50)
                    {
                        move = MoveDirection(Direction.Left);
                    } else if (timer >= 50 && timer < 100)
                    {
                        move = MoveDirection(Direction.Right);
                    } else if (timer == 100)
                    {
                        enemy.SpawnProjectile();
                    }
                    break;
                case EnemyType.Dodongo:
                    enemy.Speed = 50f;
                    if (timer == 0)
                    {
                        enemy.ChangeDirection(Direction.Right);
                    }
                    else if (timer == 25)
                    {
                        enemy.ChangeDirection(Direction.Up);
                    }
                    else if (timer == 50)
                    {
                        enemy.ChangeDirection(Direction.Left);
                    }
                    else if (timer == 75)
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
            if (timer == 200)
            {
                timer = 0;
            }

            enemy.Move(move, gameTime);
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
