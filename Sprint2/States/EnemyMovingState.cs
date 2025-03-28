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
        public Direction Direction;

        // constants
        private const int ZERO = 0;
        private const int ONE = 1;
        private const int TWO = 2;

        private const int TWENTY = 20;
        private const int TWENTY_FIVE = 25;
        private const int THIRTY = 30;
        private const int FIFTY = 50;
        private const int SEVENTY_FIVE = 75;
        private const int HUNDRED = 100;


        private int timer = ZERO;

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

            enemy.Speed = HUNDRED;

            int random = RandomNumberGenerator.GetInt32(-ONE, TWO);

            switch (enemy.enemyType)
            {
                case EnemyType.OldMan:
                    // no-op: OldMan NPC doesn't need to move
                    enemy.Speed = ZERO;
                    break;
                case EnemyType.Keese:
                    enemy.Speed = TWENTY;
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
                    enemy.Speed = THIRTY;
                    if(timer == ZERO || timer == HUNDRED)
                    {
                        enemy.ChangeDirection((Direction)random);
                    }

                    move = MoveDirection(enemy.Direction);
                    break;
                case EnemyType.Goriya:
                    Boolean moving = true;
                    if(timer == ZERO || timer == FIFTY || timer == HUNDRED)
                    {
                        enemy.ChangeDirection((Direction)random);
                    } else if (timer == (HUNDRED + FIFTY))
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
                    enemy.Speed = TWENTY;
                    if ((timer >= ZERO && timer < FIFTY) || (timer >= HUNDRED && timer < (HUNDRED + FIFTY)))
                    {
                        move = new Vector2(ZERO, ZERO);
                    } else
                    {
                        move = MoveDirection(enemy.Direction);
                    }
                    break;
                case EnemyType.Zol:
                    if ((timer >= ZERO && timer < FIFTY) || (timer >= HUNDRED && timer < (HUNDRED + FIFTY)))
                    {
                        move = new Vector2(ZERO, ZERO);
                    }
                    else
                    {
                        move = MoveDirection(enemy.Direction);
                    }
                    break;
                case EnemyType.Trap:
                    if (timer >= ZERO && timer < TWENTY_FIVE)
                    {
                        enemy.Speed = ZERO;
                        move = new Vector2(ZERO, ZERO);
                    }
                    else if (timer >= TWENTY_FIVE && timer < HUNDRED)
                    {
                        enemy.Speed = HUNDRED * TWO;
                        move = MoveDirection(Direction.Down);
                    }
                    else if (timer >= FIFTY && timer < HUNDRED)
                    {
                        enemy.Speed = HUNDRED;
                        move = MoveDirection(Direction.Up);
                    }
                    break;
                case EnemyType.Wallmaster:
                    enemy.Speed = FIFTY;
                    
                    move = new Vector2(RandomNumberGenerator.GetInt32(-ONE, TWO), RandomNumberGenerator.GetInt32(-ONE, TWO));
                    
                    break;
                case EnemyType.Rope:
                    // set to a timer in sprint2, rope's double movement speed needs to be triggered by lining up with link, not doing it eveyr once in a while
                    if (timer >= ZERO && timer < FIFTY)
                    {
                        if (RandomNumberGenerator.GetInt32(-ONE, ONE) == ONE)
                        {
                            move = MoveDirection(Direction.Right);
                        } else
                        {
                            move = MoveDirection(Direction.Left);
                        }
                    } else if (timer >= FIFTY && timer < SEVENTY_FIVE)
                    {
                        enemy.Speed = HUNDRED * TWO;
                        move = MoveDirection(Direction.Down);
                    } else
                    {
                        enemy.Speed = HUNDRED;
                        if (RandomNumberGenerator.GetInt32(-ONE, ONE) == ONE)
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
                    if (timer >= ZERO && timer < FIFTY)
                    {
                        move = MoveDirection(Direction.Left);
                    } else if (timer >= FIFTY && timer < HUNDRED)
                    {
                        move = MoveDirection(Direction.Right);
                    } else if (timer == HUNDRED)
                    {
                        enemy.SpawnProjectile();
                    }
                    break;
                case EnemyType.Dodongo:
                    enemy.Speed = FIFTY;
                    if (timer == ZERO)
                    {
                        enemy.ChangeDirection(Direction.Right);
                    }
                    else if (timer == TWENTY_FIVE)
                    {
                        enemy.ChangeDirection(Direction.Up);
                    }
                    else if (timer == FIFTY)
                    {
                        enemy.ChangeDirection(Direction.Left);
                    }
                    else if (timer == SEVENTY_FIVE)
                    {
                        enemy.ChangeDirection(Direction.Down);
                    }

                    move = MoveDirection(enemy.Direction);
                    break;
                default:
                    move = new Vector2(ZERO, ONE); ;
                    break;
            }

            timer++;
            if (timer == (HUNDRED * TWO))
            {
                timer = ZERO;
            }

            enemy.Move(move, gameTime);
        }

        public Vector2 MoveDirection(Direction dir)
        {
            switch(dir)
            {
                case Direction.Up:
                    return new Vector2(ZERO, -ONE);
                case Direction.Down:
                    return new Vector2(ZERO, ONE);
                case Direction.Left:
                    return new Vector2(ONE, ZERO);
                case Direction.Right:
                    return new Vector2(-ONE, ZERO);
                default:
                    return new Vector2(ZERO, ZERO);
            }
        }

        public void Stop()
        {
            // no-op
        }
    }
}
