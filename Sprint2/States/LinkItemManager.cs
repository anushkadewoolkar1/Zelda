using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.CollisionHandling;
using MainGame.Sprites;
using Zelda.Enums;

namespace MainGame.States
{
    public class LinkItemManager
    {
        private Link owner;
        private List<IGameObject> gameObjects;

        // Holds the sprites for each item
        private ISprite arrowSprite;
        private ISprite boomerangSprite;
        private ISprite bombSprite;
        private ISprite woodenSwordSprite;

        // Tracks if projectil is spawned, and what direction
        private bool spawnedItem;
        private bool initializeItem;
        private Vector2 projectilePosition;
        private Direction projectileDirection;
        private GameAudio _audio;
        private int boomerangTimer = 0;

        public LinkItemManager (Link link, List<IGameObject> gameObjects)
        {
            this.owner = link;
            this.gameObjects = gameObjects;
            spawnedItem = false;
            initializeItem = false;
            _audio = GameAudio.Instance;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var itemType in owner.CurrentItem)
            {
                switch (itemType)
                {
                    case ItemType.Arrow:
                        arrowSprite?.Update(gameTime, owner);
                        break;
                    case ItemType.Boomerang:
                        //boomerangTimer++;
                        //if (boomerangTimer == 10)
                        //{
                        //    _audio.ShootArrow();
                        //    boomerangTimer = 0;
                        //}

                        boomerangSprite?.Update(gameTime, owner);
                        break;
                    case ItemType.Bomb:
                        bombSprite?.Update(gameTime, owner);
                        break;
                    case ItemType.WoodenSword:
                        woodenSwordSprite?.Update(gameTime, owner);
                        break;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (initializeItem)
            {
                projectilePosition = owner.Position + new Vector2(16, 16);
                initializeItem = false;
                spawnedItem = false;
            }

            if (spawnedItem) {
            
            }

            foreach (var itemType in owner.CurrentItem)
            {
                switch (itemType)
                {
                    case ItemType.Arrow:
                        arrowSprite?.Draw(spriteBatch, projectilePosition);
                        break;
                    case ItemType.Boomerang:
                        boomerangSprite?.Draw(spriteBatch, projectilePosition);
                        break;
                    case ItemType.Bomb:
                        bombSprite?.Draw(spriteBatch, projectilePosition);
                        break;
                    case ItemType.WoodenSword:
                        woodenSwordSprite?.Draw(spriteBatch, projectilePosition);
                        break;
                }
            }
        }

        public void UseItem(ItemType itemType, Direction direction)
        {
            spawnedItem = false;

            switch (itemType)
            {
                case ItemType.Arrow:
                    arrowSprite = CreateArrowSprite(direction);
                    gameObjects.Add(arrowSprite as IGameObject);
                    _audio.ShootArrow();
                    break;

                case ItemType.Boomerang:
                    boomerangSprite = CreateBoomerangSprite(direction);
                    gameObjects.Add(boomerangSprite as IGameObject);
                    break;

                case ItemType.Bomb:
                    bombSprite = ProjectileSpriteFactory.Instance.CreateBomb();
                    gameObjects.Add(bombSprite as IGameObject);
                    break;

                case ItemType.WoodenSword:
                    woodenSwordSprite = CreateWoodenSwordSprite(direction);
                    owner.swordBeam = true;
                    gameObjects.Add(woodenSwordSprite as IGameObject);
                    break;
            }

            projectileDirection = direction;
            initializeItem = true;
        }

        private ISprite CreateArrowSprite(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return ProjectileSpriteFactory.Instance.CreateUpArrowBrown();
                case Direction.Down: return ProjectileSpriteFactory.Instance.CreateDownArrowBrown();
                case Direction.Left: return ProjectileSpriteFactory.Instance.CreateLeftArrowBrown();
                case Direction.Right: return ProjectileSpriteFactory.Instance.CreateRightArrowBrown();
                default: return ProjectileSpriteFactory.Instance.CreateDownArrowBrown();
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

        private ISprite CreateWoodenSwordSprite(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: return ProjectileSpriteFactory.Instance.CreateUpWoodenSwordBrown();
                case Direction.Down: return ProjectileSpriteFactory.Instance.CreateDownWoodenSwordBrown();
                case Direction.Left: return ProjectileSpriteFactory.Instance.CreateLeftWoodenSwordBrown();
                case Direction.Right: return ProjectileSpriteFactory.Instance.CreateRightWoodenSwordBrown();
                default: return ProjectileSpriteFactory.Instance.CreateDownWoodenSwordBrown();
            }
        }
    }
}
