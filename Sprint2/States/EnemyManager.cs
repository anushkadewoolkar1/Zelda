using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.CollisionHandling;
using MainGame.Sprites;
using Zelda.Enums;
using ZeldaGame.Zelda.CollisionMap;
using MainGame.Forces;
using MainGame.Visibility;

namespace MainGame.States
{
    public partial class Enemy : IGameObject
    {
        public void SpawnProjectile()
        {
            ProjectileSprite hold;
            if (enemyType == EnemyType.Goriya)
            {
                switch (Direction)
                {
                    case Direction.Up:
                        hold = (ProjectileSprite)projectileSpriteFactory.CreateBoomerangBrown(0);
                        break;
                    case Direction.Down:
                        hold = (ProjectileSprite)projectileSpriteFactory.CreateBoomerangBrown(2);
                        break;
                    case Direction.Left:
                        hold = (ProjectileSprite)projectileSpriteFactory.CreateBoomerangBrown(1);
                        break;
                    case Direction.Right:
                        hold = (ProjectileSprite)projectileSpriteFactory.CreateBoomerangBrown(3);
                        break;
                    default:
                        hold = (ProjectileSprite)projectileSpriteFactory.CreateBoomerangBrown(0);
                        break;
                }
            }
            else
            {
                hold = (ProjectileSprite)projectileSpriteFactory.CreateLeftArrowBlue();
            }

            System.Diagnostics.Debug.WriteLine($"Directions Are: {Direction}");
            hold.isEnemyProjectile = true;
            projectileList.Add(hold);
            gameObjects.Add(hold);
        }

        public void TakeDamage(ItemType projectile)
        {
            if (!CurrentItem.Contains(ItemType.Boomerang))
            {
                audio.EnemyHit();
            }

            switch (projectile)
            {
                case ItemType.Arrow:
                    EnemyHealth -= TWO;
                    break;
                case ItemType.Boomerang:
                    EnemyHealth -= SMALL_ENEMY_HEALTH;
                    break;
                case ItemType.Bomb:
                    EnemyHealth -= TWO * TWO;
                    break;
                case ItemType.WoodenSword:
                    EnemyHealth -= TWO;
                    break;
            }
        }

        public int GetEnemySize(Boolean x_coordinate)
        {
            if (x_coordinate)
            {
                return spriteFactory.GetEnemySize(true, enemyType);
            }
            else
            {
                return spriteFactory.GetEnemySize(false, enemyType);
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                    spriteFactory.GetEnemySize(true, enemyType),
                    spriteFactory.GetEnemySize(false, enemyType));
            }
        }

        public Vector2 Velocity => velocity;

        public void Update(GameTime gameTime, List<IGameObject> _gameObjects)
        {
            gameObjects = _gameObjects;
            enemyState.Update(gameTime);
            spriteUpdateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (spriteUpdateTimer >= spriteUpdateInterval)
            {
                sprite.Update(gameTime);
                spriteUpdateTimer = 0;
            }

            foreach (var projectile in projectileList)
            {
                Link link = new Link(gameObjects);
                link.Position = new Vector2(position.X - 8, position.Y - 8);
                link.currentDirection = Direction;
                projectile.Update(gameTime, link);
            }

            if (EnemyHealth <= ZERO)
            {
                EnemyHealth = ZERO;
                Destroy();
            }
        }

        public void Update(GameTime gameTime)
        {
            // no-op
        }

        public void ChangeState(IEnemyState newState)
        {
            enemyState.Stop();
            enemyState = newState;
            enemyState.Load();
        }

        public void Destroy()
        {
            sprite = spriteFactory.CreateEnemySprite(EnemyType.None, Direction);
            velocity = new Vector2((float)ZERO, (float)ZERO);
            position = new Vector2(ENEMY_DEATH_POSITION, (float)ZERO);
        }
    }
}
