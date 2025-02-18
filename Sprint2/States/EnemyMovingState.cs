using System;
using System.Collections.Generic;
using System.Linq;
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
        private Enemy enemy;
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private int timer = 0;
        public Direction Direction { get; set; }

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
                ISprite enemySprite = EnemySpriteFactory.Instance.CreateEnemySprite(enemy.enemyType);
            }
        }

        public void Update(GameTime gameTime)
        {
            Vector2 move = new Vector2(0, 0);
            RandomNumberGenerator randomNumber = RandomNumberGenerator.Create();

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
                    if (position.X >= 500 && position.Y >= 350)
                    {
                        if (timer >= 0 && timer < 33)
                        {
                            move = new Vector2(0, -1);
                        }
                        else if (timer >= 33 && timer < 66)
                        {
                            move = new Vector2(1, 0);
                        }
                        else if (timer >= 66 && timer < 100)
                        {
                            move = new Vector2(0, 0);
                        }
                    } else
                    {
                        if (timer >= 0 && timer < 33)
                        {
                            move = new Vector2(0, 1);
                        }
                        else if (timer >= 33 && timer < 66)
                        {
                            move = new Vector2(-1, 0);
                        }
                        else if (timer >= 66 && timer < 100)
                        {
                            move = new Vector2(0, 0);
                        }
                    }

                    if (timer == 66)
                    {
                        // throw animation, can't move while throwing but still has moving animation
                    }
                    break;
                case EnemyType.Gel:
                    if ((timer >= 0 && timer < 25) || (timer >= 50 && timer < 75))
                    {
                        move = new Vector2(0, 0);
                    } else if (timer >= 25 && timer < 50)
                    {
                        move = new Vector2(RandomNumberGenerator.GetInt32(0, 2), 0);
                    } else
                    {
                        move = new Vector2(0, RandomNumberGenerator.GetInt32(0, 2));
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
                    
                    move = new Vector2(RandomNumberGenerator.GetInt32(0, 2), RandomNumberGenerator.GetInt32(0, 2));
                    
                    break;
                case EnemyType.Rope:
                    // set to a timer in sprint2, rope's double movement speed needs to be triggered by lining up with link, not doing it eveyr once in a while
                    if (timer >= 0 && timer < 50)
                    {
                        move = new Vector2(RandomNumberGenerator.GetInt32(0, 2), 0);
                    } else if (timer >= 50 && timer < 75)
                    {
                        enemy.Speed = 200f;
                        move = new Vector2(0, 1);
                    } else
                    {
                        enemy.Speed = 100f;
                        move = new Vector2(RandomNumberGenerator.GetInt32(0, 2), 0);
                    }
                    break;
                case EnemyType.Aquamentus:
                    if (timer >= 0 && timer < 50)
                    {
                        move = new Vector2(1, 0);
                    } else if (timer >= 50 && timer < 100)
                    {
                        move = new Vector2(-1, 0);
                    }
                    break;
                case EnemyType.Dodongo:
                    enemy.Speed = 50f;
                    if (timer >= 0 && timer < 25)
                    {
                        move = new Vector2(1, 0);
                    }
                    else if (timer >= 25 && timer < 50)
                    {
                        move = new Vector2(0, -1);
                    }
                    else if (timer >= 50 && timer < 75)
                    {
                        move = new Vector2(-1, 0);
                    }
                    else if (timer >= 75 && timer < 100)
                    {
                        move = new Vector2(0, 1);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            enemy.DrawCurrentSprite(spriteBatch);
        }

        public void Stop()
        {
            // no-op
        }
    }
}
