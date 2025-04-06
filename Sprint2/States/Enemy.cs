using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.CollisionHandling;
using Sprint0.Sprites;
using Zelda.Enums;
using ZeldaGame.Zelda.CollisionMap;


namespace Sprint0.States
{
    public class Enemy : IGameObject
    {
        private IEnemyState enemyState;
        private double EnemyHealth;
        private EnemySprite sprite;
        private EnemySpriteFactory spriteFactory;
        public Vector2 position { get; set; }
        public EnemyType enemyType { get; set; }
        public float Speed { get; set; }
        public Direction Direction { get; set; }
        private ISprite boomerangSprite, fireballSprite;
        private Boolean itemSpawn;
        private Boolean itemSpawned;
        public ItemType itemType { get; set; }
        public List<ItemType> CurrentItem { get; set; }
        public int chooseItem { get; set; }
        private EnemyProjectileManager projectileManager;
        private List<IGameObject> gameObjects;
        private Vector2 projectilePosition;
        private Vector2 velocity;
        private TileMap tileMap = TileMap.GetInstance();
        private GameAudio _audio = GameAudio.Instance;

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


        public Enemy()
        {
            spriteFactory = EnemySpriteFactory.Instance;

            sprite = spriteFactory.CreateEnemySprite(enemyType, Direction);
            sprite.spriteSize = DEFAULT_SPRITE_SIZE;

            SetHealth();


            enemyState = new EnemyMovingState(this);
            enemyState.Load();

            CurrentItem = new List<ItemType>();
            projectileManager = new EnemyProjectileManager(this, gameObjects);
            itemType = ItemType.Boomerang;
            itemSpawn = false;
            itemSpawned = false;

        }

        public Enemy CreateEnemy(EnemyType enemyCreated, Vector2 spawnPosition)
        {
            enemyType = enemyCreated;
            SetHealth();
            position = tileMap.GetTileCenter(spawnPosition);
            //I combined the two lines below into one line so that position can be a property
            position = new Vector2(tileMap.GetTileCenter(spawnPosition).X,
                tileMap.GetTileCenter(spawnPosition).Y - 100);
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
            sprite.Draw(spriteBatch, this.position);
            projectileManager.Draw(spriteBatch);

            if (itemSpawn)
            {
                projectilePosition = position + new Vector2((float) TWO, (float) TWO);
                itemSpawn = false;
                itemSpawned = true;
            }
            if (itemSpawned)
            {
                switch (itemType)
                {
                    case ItemType.Boomerang:
                        boomerangSprite.Draw(spriteBatch, projectilePosition);
                        break;
                    case ItemType.Fireball:
                        fireballSprite.Draw(spriteBatch, projectilePosition);
                        break;
                    default:
                        break;
                }
            }
        }

        public void ChangeDirection(Direction newDirection)
        {
            Direction = newDirection;

            sprite = spriteFactory.CreateEnemySprite(this.enemyType, Direction);
        }

        public void Move(Vector2 move, GameTime gameTime)
        {
            if (!itemSpawned) 
            {
                if (move != Vector2.Zero)
                move.Normalize();

                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.position += move * Speed * dt;
                switch (Direction)
                {
                    case Direction.Up:
                        velocity = new Vector2((float) ZERO, (float) -ONE);
                        break;
                    case Direction.Down:
                        velocity = new Vector2((float) ZERO, (float) ONE);
                        break;
                    case Direction.Left:
                        velocity = new Vector2((float) ONE, (float) ZERO);
                        break;
                    case Direction.Right:
                        velocity = new Vector2((float) -ONE, (float) ZERO);
                        break;
                    default:
                        velocity = Vector2.Zero;
                        break;
                }
            }
        }

        public void SpawnProjectile()
        {
            if (chooseItem < CurrentItem.Count)
            {
                var itemType = CurrentItem[chooseItem];
                projectileManager.SpawnProjectile(itemType, Direction);
            }
        }

        public void TakeDamage(ItemType projectile)
        {
            _audio.EnemyHit();
            
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
                return spriteFactory.GetEnemySize(true);
            } else
            {
                return spriteFactory.GetEnemySize(false);
            }
        }

        public Rectangle BoundingBox
        {
            get 
            {
                return new Rectangle((int)position.X, (int)position.Y, spriteFactory.GetEnemySize(true), spriteFactory.GetEnemySize(false));
            }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
        }

        public void Update(GameTime gameTime)
        {

            enemyState.Update(gameTime);
            projectileManager.Update(gameTime);
            spriteUpdateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (spriteUpdateTimer >= spriteUpdateInterval)
            {
                sprite.Update(gameTime);
                spriteUpdateTimer = 0;
            }
            //sprite.Update(gameTime);

            if (itemSpawned)
            {
                switch (itemType)
                {
                    case ItemType.Boomerang:
                        boomerangSprite.Update(gameTime);
                        break;
                    case ItemType.Fireball:
                        fireballSprite.Update(gameTime);
                        break;
                    default:
                        break;
                }
            }


            if (EnemyHealth <= ZERO)
            {
                EnemyHealth = ZERO;
                Destroy();
            }
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
            velocity = new Vector2((float) ZERO, (float) ZERO);
            position = new Vector2(ENEMY_DEATH_POSITION, (float) ZERO);
        }
    }
}
