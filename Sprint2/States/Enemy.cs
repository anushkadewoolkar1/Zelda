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
        private IEnemyState enemyState;
        private double EnemyHealth;
        private EnemySprite sprite;
        private EnemySpriteFactory spriteFactory;
        public Vector2 position { get; set; }
        public EnemyType enemyType { get; set; }
        public float Speed { get; set; }
        public Direction Direction { get; set; }
        private Boolean itemSpawned;
        public ItemType itemType { get; set; }
        public List<ItemType> CurrentItem { get; set; }
        public int chooseItem { get; set; }
        private EnemyProjectileManager projectileManager;
        private ProjectileSpriteFactory projectileSpriteFactory = ProjectileSpriteFactory.Instance;
        private List<IGameObject> gameObjects;
        public Vector2 velocity { get; set; }
        private TileMap tileMap = TileMap.GetInstance();
        private GameAudio audio;
        private FogOfWar fow = FogOfWar.Instance;
        private List<ProjectileSprite> projectileList = new List<ProjectileSprite>();

        // constants
        private const double ZERO = 0.0;
        private const int DEFAULT_SPRITE_SIZE = 32;
        private const int ONE = 1;
        private const double TWO = 2.0;
        private const double THREE = 3.0;
        private const double NPC_HEALTH = 99.0;
        private const double SMALL_ENEMY_HEALTH = 0.5;
        private const float ENEMY_DEATH_POSITION = -40;
        private const int TWELVE = 12;

        private double spriteUpdateTimer = 0;
        private const double spriteUpdateInterval = 1000.0 / 5.0;

        public Enemy(List<IGameObject> _gameObjects, GameAudio _audio)
        {
            spriteFactory = EnemySpriteFactory.Instance;

            sprite = spriteFactory.CreateEnemySprite(enemyType, Direction);
            sprite.spriteSize = DEFAULT_SPRITE_SIZE;

            SetHealth();

            enemyState = new EnemyMovingState(this);
            enemyState.Load();

            CurrentItem = new List<ItemType>();
            gameObjects = _gameObjects;
            projectileManager = EnemyProjectileManager.Instance;
            itemSpawned = false;

            audio = _audio;
        }

        public Enemy CreateEnemy(EnemyType enemyCreated, Vector2 spawnPosition)
        {
            enemyType = enemyCreated;
            if (enemyCreated == EnemyType.Goriya)
            {
                CurrentItem.Add(ItemType.Boomerang);
            }
            else if (enemyCreated == EnemyType.Aquamentus)
            {
                CurrentItem.Add(ItemType.Fireball);
            }
            SetHealth();
            position = tileMap.GetTileCenter(spawnPosition);
            position = new Vector2(tileMap.GetTileCenter(spawnPosition).X,
                                   tileMap.GetTileCenter(spawnPosition).Y);
            sprite = spriteFactory.CreateEnemySprite(enemyCreated, Direction);
            enemyState = new EnemyMovingState(this);
            enemyState.Load();
            return this;
        }

        public void SetHealth()
        {
            switch (enemyType)
            {
                case EnemyType.OldMan:
                    EnemyHealth = NPC_HEALTH;
                    break;
                case EnemyType.Keese:
                    EnemyHealth = SMALL_ENEMY_HEALTH;
                    break;
                case EnemyType.Stalfos:
                    EnemyHealth = TWO;
                    break;
                case EnemyType.Goriya:
                    EnemyHealth = THREE;
                    break;
                case EnemyType.Gel:
                    EnemyHealth = SMALL_ENEMY_HEALTH;
                    break;
                case EnemyType.Zol:
                    EnemyHealth = TWO;
                    break;
                case EnemyType.Trap:
                    EnemyHealth = NPC_HEALTH;
                    break;
                case EnemyType.Wallmaster:
                    EnemyHealth = TWO;
                    break;
                case EnemyType.Rope:
                    EnemyHealth = SMALL_ENEMY_HEALTH;
                    break;
                case EnemyType.Aquamentus:
                    EnemyHealth = THREE * TWO;
                    break;
                case EnemyType.Dodongo:
                    EnemyHealth = Math.Pow(TWO, THREE);
                    break;
                default:
                    EnemyHealth = ZERO;
                    break;
            }
        }

        public void DrawCurrentSprite(SpriteBatch spriteBatch)
        {
            if (fow.FogOfWarCheck(this))
            {
                sprite.Draw(spriteBatch, position);
            }

            foreach (var projectile in projectileList)
            {
                projectile.Draw(spriteBatch, position);
            }
        }

        public void ChangeDirection(Direction newDirection)
        {
            Direction = newDirection;
            sprite = spriteFactory.CreateEnemySprite(enemyType, Direction);
        }

        public void Move(Vector2 move, GameTime gameTime)
        {
            if (!itemSpawned)
            {
                if (move != Vector2.Zero)
                    move.Normalize();

                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                position += move * Speed * dt;
                switch (Direction)
                {
                    case Direction.Up:
                        velocity = new Vector2((float)ZERO, (float)-ONE);
                        break;
                    case Direction.Down:
                        velocity = new Vector2((float)ZERO, (float)ONE);
                        break;
                    case Direction.Left:
                        velocity = new Vector2((float)ONE, (float)ZERO);
                        break;
                    case Direction.Right:
                        velocity = new Vector2((float)-ONE, (float)ZERO);
                        break;
                    default:
                        velocity = Vector2.Zero;
                        break;
                }
            }
        }
    }
}