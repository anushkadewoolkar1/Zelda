using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using Sprint0.CollisionHandling;
using Zelda.Enums;

namespace Sprint0.States
{
    public class EnemyProjectileManager
    {
        private Enemy enemy;
        private List<IGameObject> gameobjects;

        // need boomerang and fireball for Goriya and Aquamentus
        private ISprite boomerangSprite, fireballSprite;

        private bool spawnedItem;
        private bool initializeItem;
        private Vector2 projectilePosition;
        private Direction projectileDirection;

        public EnemyProjectileManager(Enemy enemy, List<IGameObject> gameObjects)
        {
            this.enemy = enemy;
            this.gameobjects = gameObjects;
            spawnedItem = false;
            initializeItem = false;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var itemType in enemy.CurrentItem)
            {
                switch(itemType)
                {
                    case ItemType.Boomerang:
                        boomerangSprite.Update(gameTime);
                        break;
                    case ItemType.Fireball:
                        fireballSprite.Update(gameTime);
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (initializeItem)
            {
                projectilePosition = enemy.position + new Vector2(16, 16);
                initializeItem = false;
                spawnedItem = false;
            }

            foreach (ItemType type in enemy.CurrentItem)
            {
                switch(type)
                {
                    case ItemType.Boomerang:
                        break;
                    case ItemType.Fireball:
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
                    break;
                case ItemType.Fireball:
                    fireballSprite = EnemySpriteFactory.Instance.CreateFireballSprite((int) projectilePosition.X, (int) projectilePosition.Y);
                    break;
            }

            projectileDirection = direction;
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
