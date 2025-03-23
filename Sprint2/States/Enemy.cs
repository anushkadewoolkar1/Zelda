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
        private Vector2 projectilePosition;
        private Vector2 velocity;
        TileMap tileMap = TileMap.GetInstance();
        private GameAudio _audio = GameAudio.Instance;


        public Enemy()
        {
            spriteFactory = EnemySpriteFactory.Instance;

            sprite = spriteFactory.CreateEnemySprite(enemyType, Direction);
            sprite.spriteSize = 32;

            SetHealth();


            enemyState = new EnemyMovingState(this);
            enemyState.Load();

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
                tileMap.GetTileCenter(spawnPosition).Y - 12);
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
                    EnemyHealth = 99.0;
                    break;
                case EnemyType.Keese:
                    EnemyHealth = 0.5;
                    break;
                case EnemyType.Stalfos:
                    EnemyHealth = 2.0;
                    break;
                case EnemyType.Goriya:
                    EnemyHealth = 3.0;
                    break;
                case EnemyType.Gel:
                    EnemyHealth = 0.5;
                    break;
                case EnemyType.Zol:
                    EnemyHealth = 2.0;
                    break;
                case EnemyType.Trap:
                    EnemyHealth = 99.0;
                    break;
                case EnemyType.Wallmaster:
                    EnemyHealth = 2.0;
                    break;
                case EnemyType.Rope:
                    EnemyHealth = 0.5;
                    break;
                case EnemyType.Aquamentus:
                    EnemyHealth = 6.0;
                    break;
                case EnemyType.Dodongo:
                    EnemyHealth = 8.0;
                    break;
                default:
                    EnemyHealth = 0;
                    break;

            }
        }

        public void DrawCurrentSprite(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, this.position);

            if (itemSpawn)
            {
                projectilePosition = position + new Vector2(2, 2);
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
            switch (newDirection)
            {
                case Direction.Up:
                    sprite = spriteFactory.CreateEnemySprite(this.enemyType, Direction);
                    break;
                case Direction.Down:
                    sprite = spriteFactory.CreateEnemySprite(this.enemyType, Direction);
                    break;
                case Direction.Left:
                    sprite = spriteFactory.CreateEnemySprite(this.enemyType, Direction);
                    break;
                case Direction.Right:
                    sprite = spriteFactory.CreateEnemySprite(this.enemyType, Direction);
                    break;

            }
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
                        velocity = new Vector2(0, -1);
                        break;
                    case Direction.Down:
                        velocity = new Vector2(0, 1);
                        break;
                    case Direction.Left:
                        velocity = new Vector2(1, 0);
                        break;
                    case Direction.Right:
                        velocity = new Vector2(-1, 0);
                        break;
                    default:
                        velocity = Vector2.Zero;
                        break;
                }
            }
        }

        public void SpawnProjectile()
        {
            itemSpawn = false;
            int directionNumber = -1;
            switch (Direction)
            {
                case Direction.Up:
                    directionNumber = 0;
                    break;
                case Direction.Down:
                    directionNumber = 1;
                    break;
                case Direction.Left:
                    directionNumber = 2;
                    break;
                case Direction.Right:
                    directionNumber = 3;
                    break;
            }
            switch (itemType)
            {
                case ItemType.Boomerang:
                    boomerangSprite = ProjectileSpriteFactory.Instance.CreateBoomerangBrown(directionNumber);
                    break;
                case ItemType.Fireball:
                    // create an EnemySprite since Boss sprite sheet has the needed fireballs
                    //fireballSprite = EnemySpriteFactory.Instance.
                    break;
                default:
                    break;
            }

            itemSpawn = true;
        }

        public void TakeDamage(ItemType projectile)
        {
            _audio.EnemyHit();
            
            switch (projectile)
            {
                case ItemType.Arrow:
                    EnemyHealth -= 2.0;
                    break;
                case ItemType.Boomerang:
                    EnemyHealth -= 0.5;
                    break;
                case ItemType.Bomb:
                    EnemyHealth -= 4.0;
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
            sprite.Update(gameTime);

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


            if (EnemyHealth <= 0)
            {
                EnemyHealth = 0;
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
            velocity = new Vector2(0, 0);
            position = new Vector2(0, 0);
        }
    }
}
