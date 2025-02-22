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

            // need facing directions for Goriya
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
            Vector2 move = new Vector2(0, 0);

            enemy.Speed = 100f;

            switch (enemy.enemyType)
            {
                case EnemyType.OldMan:
                    // no-op: OldMan NPC doesn't need to move
                    enemy.Speed = 0;
                    break;
                case EnemyType.Keese:
                    if (timer >= 0 && timer < 25)
                    {
                        move = new Vector2(1, RandomNumberGenerator.GetInt32(-1, 2));
                    }
                    else if (timer >= 25 && timer < 50)
                    {
                        move = new Vector2(1, RandomNumberGenerator.GetInt32(-1, 2));
                    }
                    else if (timer >= 50 && timer < 75)
                    {
                        move = new Vector2(-1, RandomNumberGenerator.GetInt32(-1, 2));
                    }
                    else if (timer >= 75 && timer < 100)
                    {
                        move = new Vector2(-1, RandomNumberGenerator.GetInt32(-1, 2));
                    }
                    break;
                case EnemyType.Stalfos:
                    if (timer >= 0 && timer < 25)
                    {
                        move = new Vector2(1, 0);
                    } else if (timer >= 25 && timer < 50)
                    {
                        move = new Vector2(0, -1);
                    } else if (timer >= 50 && timer < 75)
                    {
                        move = new Vector2(-1, 0);
                    } else if (timer >= 75 && timer < 100)
                    {
                        move = new Vector2(0, 1);
                    } 
                    break;
                case EnemyType.Goriya:
                    // refactor this later, this is pretty hard to look at tbh
                    Boolean moving = true;
                    if (timer == 0)
                    {
                        int random = RandomNumberGenerator.GetInt32(-1, 2);
                        if (random == -1)
                        {
                            enemy.ChangeDirection(Direction.Up);
                        }
                        else if (random == 1)
                        {
                            enemy.ChangeDirection(Direction.Down);
                        }
                    }
                    else if (timer == 25)
                    {
                        int random = RandomNumberGenerator.GetInt32(-1, 2);
                        if (random == -1)
                        {
                            enemy.ChangeDirection(Direction.Right);
                        }
                        else if (random == 1)
                        {
                            enemy.ChangeDirection(Direction.Left);
                        }
                    }
                    else if (timer == 50)
                    {
                        int random = RandomNumberGenerator.GetInt32(-1, 2);
                        if (random == -1)
                        {
                            enemy.ChangeDirection(Direction.Up);
                        }
                        else if (random == 1)
                        {
                            enemy.ChangeDirection(Direction.Down);
                        }

                    }
                    else if (timer == 75)
                    {
                        enemy.SpawnProjectile();
                    }
                    else if (timer > 75 && timer < 100)
                    {
                        moving = false;

                    }
                    if (moving)
                    {
                        switch (enemy.Direction)
                        {
                            case Direction.Up:
                                move = new Vector2(0, -1);
                                break;
                            case Direction.Down:
                                move = new Vector2(0, 1);
                                break;
                            case Direction.Left:
                                move = new Vector2(1, 0);
                                break;
                            case Direction.Right:
                                move = new Vector2(-1, 0);
                                break;
                        }
                    }
                    else
                    {
                        move = new Vector2(0, 0);
                    }
                    break;
                case EnemyType.Gel:
                    if ((timer >= 0 && timer < 25) || (timer >= 50 && timer < 75))
                    {
                        move = new Vector2(0, 0);
                    } else if (timer >= 25 && timer < 50)
                    {
                        move = new Vector2(RandomNumberGenerator.GetInt32(-1, 2), 0);
                    } else
                    {
                        move = new Vector2(0, RandomNumberGenerator.GetInt32(-1, 2));
                    }
                    break;
                case EnemyType.Zol:
                    if ((timer >= 0 && timer < 25) || (timer >= 50 && timer < 75))
                    {
                        move = new Vector2(0, 0);
                    }
                    else if (timer >= 25 && timer < 50)
                    {
                        move = new Vector2(RandomNumberGenerator.GetInt32(0, 2), 0);
                    }
                    else
                    {
                        move = new Vector2(0, RandomNumberGenerator.GetInt32(0, 2));
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
                        move = new Vector2(0, 1);
                    }
                    else if (timer >= 50 && timer < 100)
                    {
                        enemy.Speed = 100f;
                        move = new Vector2(0, -1);
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
                        move = new Vector2(RandomNumberGenerator.GetInt32(-1, 2), 0);
                    } else if (timer >= 50 && timer < 75)
                    {
                        enemy.Speed = 200f;
                        move = new Vector2(0, 1);
                    } else
                    {
                        enemy.Speed = 100f;
                        move = new Vector2(RandomNumberGenerator.GetInt32(-1, 2), 0);
                    }
                    break;
                case EnemyType.Aquamentus:
                    if (timer >= 0 && timer < 50)
                    {
                        move = new Vector2(1, 0);
                    } else if (timer >= 50 && timer < 100)
                    {
                        move = new Vector2(-1, 0);
                    } else if (timer == 100)
                    {
                        // spawn projectiles
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

                    switch (enemy.Direction)
                    {
                        case Direction.Up:
                            move = new Vector2(0, -1);
                            break;
                        case Direction.Down:
                            move = new Vector2(0, 1);
                            break;
                        case Direction.Left:
                            move = new Vector2(1, 0);
                            break;
                        case Direction.Right:
                            move = new Vector2(-1, 0);
                            break;
                    }
                    break;
                default:
                    move = new Vector2(0, 1); ;
                    break;
            }

            timer++;
            if (timer == 100)
            {
                timer = 0;
            }

            enemy.Move(move, gameTime);
        }

        public void Stop()
        {
            // no-op
        }
    }
}
