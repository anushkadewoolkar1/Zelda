using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.Sprites;
using MainGame.CollisionHandling;
using Zelda.Enums;

namespace MainGame.States
{
    public class EnemyProjectileManager
    {
        private Enemy enemy;
        private List<IGameObject> gameObjects;

        // need boomerang and fireball for Goriya and Aquamentus
        private ISprite boomerangSprite, fireballSprite;

        private bool spawnedItem;
        private bool initializeItem;
        private bool destroy;
        private Vector2 projectilePosition;
        private Direction projectileDirection;
        private float projectileSpeed;
        private int projectileTimer = 0;
        private int projectileTimerMax = 20;

        public EnemyProjectileManager(Enemy enemy, List<IGameObject> _gameObjects)
        {
            this.enemy = enemy;
            gameObjects = _gameObjects;
            spawnedItem = false;
            initializeItem = false;
            destroy = false;
        }

        public void Update(GameTime gameTime)
        {
            if(projectileTimer == projectileTimerMax)
            {
                enemy.CurrentItem.Remove(enemy.itemType);
            } else if(projectileTimer >= projectileTimerMax)
            {
                return;
            }

            projectilePosition += MoveDirection(projectileDirection) * projectileSpeed;
            if(projectileTimer == 10)
            {
                switch(projectileDirection)
                {
                    case Direction.Left:
                        projectileDirection = Direction.Right;
                        break;
                    case Direction.Right:
                        projectileDirection = Direction.Left;
                        break;
                    case Direction.Up:
                        projectileDirection = Direction.Down; 
                        break;
                    case Direction.Down:
                        projectileDirection = Direction.Up;
                        break;
                }
            }

            projectileTimer += gameTime.ElapsedGameTime.Seconds;

            foreach (var itemType in enemy.CurrentItem)
            {
                switch(itemType)
                {
                    case ItemType.Boomerang:
                        boomerangSprite.Update(gameTime, enemy);
                        break;
                    case ItemType.Fireball:
                        fireballSprite.Update(gameTime, enemy);
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (destroy) return;

            foreach (var type in enemy.CurrentItem)
            {
                switch(type)
                {
                    case ItemType.Boomerang:
                        boomerangSprite.Draw(spriteBatch, projectilePosition);
                        break;
                    case ItemType.Fireball:
                        fireballSprite.Draw(spriteBatch, projectilePosition);
                        break;
                }
            }
        }


        public void SpawnProjectile(ItemType itemType, Direction direction)
        {
            spawnedItem = false;

            switch (itemType)
            {
                case ItemType.Boomerang:
                    boomerangSprite = CreateBoomerangSprite(direction);
                    projectileSpeed = 10;
                    gameObjects.Add(boomerangSprite as IGameObject);
                    break;
                case ItemType.Fireball:
                    fireballSprite = EnemySpriteFactory.Instance.CreateFireballSprite((int) projectilePosition.X, (int) projectilePosition.Y);
                    gameObjects.Add(fireballSprite as IGameObject);
                    break;
            }

            projectilePosition = enemy.position;
            projectileDirection = direction;
            initializeItem = true;
        }

        private Vector2 MoveDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Vector2(1, 0);
                case Direction.Right:
                    return new Vector2(-1, 0);
                case Direction.Up:
                    return new Vector2(0, 1);
                case Direction.Down:
                    return new Vector2(0, -1);
                default:
                    return new Vector2(0, 0);
            }
        }

        private ISprite CreateBoomerangSprite(Direction direction)
        {
            if (direction == Direction.Up)
                return ProjectileSpriteFactory.Instance.CreateBoomerangBrown((int)direction);
            else if (direction == Direction.Right)
                return ProjectileSpriteFactory.Instance.CreateBoomerangBrown((int)direction - 2);
            else
                return ProjectileSpriteFactory.Instance.CreateBoomerangBrown((int)direction + 1);
        }


    }
}
