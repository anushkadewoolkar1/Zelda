using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.Sprites;
using MainGame.CollisionHandling;
using Microsoft.Xna.Framework.Content;
using Zelda.Enums;
using MainGame.Collision;

namespace MainGame.States
{
    public class EnemyProjectileManager
    {
        private Enemy enemy;
        private List<IGameObject> gameObjects;
        private Texture2D goriyaSpritesheet;
        private Texture2D aquaSpritesheet;

        // need boomerang and fireball for Goriya and Aquamentus
        private EnemyProjectileSprite boomerangSprite, fireballSprite;

        private bool spawnedItem;
        private bool initializeItem;
        private bool destroy;
        private Vector2 projectilePosition;
        private Direction projectileDirection;
        private float projectileSpeed;
        private int projectileTimer = 0;
        private int projectileTimerMax = 20;

        private EnemyProjectileManager()
        {
        }

        private static EnemyProjectileManager instance = new EnemyProjectileManager();

        public static EnemyProjectileManager Instance
        {  get { return instance; } }

        public void LoadAllTextures(ContentManager spriteBatch)
        {
            goriyaSpritesheet = spriteBatch.Load<Texture2D>("ItemSpritesheet");
            aquaSpritesheet = spriteBatch.Load<Texture2D>("lozNPCs");
        }

        public void SetProjectile(Enemy _enemy, List<IGameObject> _gameObjects)
        {
            enemy = _enemy;
            gameObjects = _gameObjects;
            spawnedItem = false;
            initializeItem = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!spawnedItem)
            {
                return;
            }
            if(projectilePosition == enemy.position)
            {
                enemy.CurrentItem.Remove(enemy.itemType);
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
            if (destroy || !spawnedItem) return;

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

            projectilePosition = enemy.position + MoveDirection(direction);
            projectileDirection = direction;
            initializeItem = true;
            spawnedItem = true;
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

        private EnemyProjectileSprite CreateBoomerangSprite(Direction direction)
        {
            return new EnemyProjectileSprite(goriyaSpritesheet, 1, 1, 65, 0, 5, 8, ItemType.Boomerang);
        }


    }
}
