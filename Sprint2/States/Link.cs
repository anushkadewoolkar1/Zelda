using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.CollisionHandling;
using Sprint0.Sprites;
using Zelda.Enums;


public class Link : IGameObject
{
    public ILinkState currentState;
    public Vector2 Position { get; set; }
    private ISprite currentSprite;
    private LinkSpriteFactory spriteFactory;
    //public LinkSprite LinkSprite { get; set; }

    // Invulnerability settings.
    public bool IsInvulnerable { get; private set; }
    public float invulnerabilityTimer;

    // (pixels per second).
    private const float Speed = 100f;

    // Health property 
    public int Health { get; set; } = 3;
    // property for which item is currently selected
    public List<ItemType> CurrentItem { get; set; }  
    public Direction currentDirection { get; set; }

    public Boolean linkAttacking { get; set; }
    public Boolean linkUseItem { get; set; }

    private Boolean initializeItem;
    public int chooseItem { get; set; }
    private Boolean spawnedItem;
    private ISprite arrowSprite, boomerangSprite, bombSprite;
    private Vector2 projectilePosition;
    private Direction projectileDirection;
    private List<IGameObject> gameObjects;

    private Vector2 velocity;

    public Link()
    {
        // Initialize the sprite factory 
        spriteFactory = LinkSpriteFactory.Instance;
        Position = new Vector2(100, 100);

        // Start initial sprite
        currentSprite = LinkSpriteFactory.Instance.CreateDownWalk(1, 0, 0);
        currentState = new LinkWalkingState(this, Direction.Down);
        currentState.Enter();

        // Set invulnerability to false and timer to 0.
        IsInvulnerable = false;
        invulnerabilityTimer = 0f;

        // Set the default current item 
        initializeItem = false;
        spawnedItem = false;

        linkAttacking = false;

        CurrentItem = new List<ItemType>();

    }

    public void Update(List<IGameObject> _gameObjects,GameTime gameTime)
    {
        gameObjects = _gameObjects;
        currentState.Update(gameTime);
        currentSprite.Update(gameTime);

        //Updates projectile for sprite movement when projectile exists
        if (spawnedItem)
        {
            
        }

        for (int i = 0; i < CurrentItem.Count; i++)
        {
            switch (CurrentItem[i])
            {
                case ItemType.Arrow:
                    if (arrowSprite != null)
                    {
                        arrowSprite.Update(gameTime, this);
                    }
                    break;
                case ItemType.Boomerang:
                    if (boomerangSprite != null)
                    {
                        boomerangSprite.Update(gameTime, this);
                    }
                    break;
                case ItemType.Bomb:
                    if (bombSprite != null)
                    {
                        bombSprite.Update(gameTime, this);
                    }
                    break;
                default:
                    break;
            }
        }



        // Update invulnerability timer if Link is invulnerable.
        if (IsInvulnerable)
        {
            invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (invulnerabilityTimer <= 0)
            {
                // Once the damaged duration is over, revert to the previous state
                if (this.currentState != null)
                    ChangeState(this.currentState);
                else
                    ChangeState(new LinkWalkingState(this, currentDirection));
                EndInvulnerability();
            }
            
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentSprite.Draw(spriteBatch, this.Position);


        //Starts Link's Projectile at Link's location

        if (initializeItem == true)
        {
            projectilePosition = this.Position + new Vector2(4 * 4, 4 * 4);
            initializeItem = false;
            spawnedItem = true;
        }

        //Draws Projectile Sprite while Projectile exists
        if (spawnedItem == true)
        {
        }
        for (int i = 0; i < CurrentItem.Count; i++)
        {
            switch (CurrentItem[i])

            {
                case ItemType.Arrow:
                    if (arrowSprite != null)
                    {
                        arrowSprite.Draw(spriteBatch, projectilePosition);
                    }
                    break;
                case ItemType.Boomerang:
                    if (boomerangSprite != null)
                    {
                        boomerangSprite.Draw(spriteBatch, projectilePosition);
                       
                    }
                    break;
                case ItemType.Bomb:
                    if (bombSprite != null)
                    {
                        bombSprite.Draw(spriteBatch, projectilePosition);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void ChangeState(ILinkState newState)
    {
        if (!linkAttacking && !linkUseItem)
        {
            this.currentState.Exit();
            this.currentState = newState;
            this.currentState.Enter();
        }
    }

    public void SetSprite(ISprite sprite)
    {
        currentSprite = sprite;
    }

    public void DrawCurrentSprite(SpriteBatch spriteBatch)
    {
        currentSprite.Draw(spriteBatch, Position);
    }

    public ISprite GetCurrentSprite()
    {
        return currentSprite; 
    }

    public void Move(Vector2 direction, GameTime gameTime)
    {
        // Normalize direction if it's not zero.
        if (direction != Vector2.Zero)
            direction.Normalize();

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Position += direction * Speed * dt;
        velocity = direction;
    }

    public Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)Position.X, (int)Position.Y, 30, 30);
        }
    }
    public Vector2 Velocity
    {
        get { return velocity;  }
    }

    public void PerformAttack(Direction direction)
    {
        System.Diagnostics.Debug.WriteLine("Link performs an attack!");
    }

    public void PickUpItem(ItemSprite pickedUpItem)
    {
        System.Diagnostics.Debug.WriteLine("Link picks up item");
        // Implement item pick up logic
        switch (pickedUpItem.GetItemString())
        {
            case "ZeldaSpriteArrow": CurrentItem.Add(ItemType.Arrow); System.Diagnostics.Debug.WriteLine("ARROW!");  break;
            case "ZeldaSpriteBoomerang": CurrentItem.Add(ItemType.Boomerang); break;
            case "ZeldaSpriteBomb": CurrentItem.Add(ItemType.Bomb); break;
        }
    }

    public void UseItem()
    {
        if (linkAttacking)
        {
            return;
        }
        System.Diagnostics.Debug.WriteLine("Link uses an");
        spawnedItem = false;
        switch (CurrentItem[chooseItem])
        {
            case ItemType.Arrow:
                {
                    switch (currentDirection)
                    {
                        case Direction.Up:
                            arrowSprite = ProjectileSpriteFactory.Instance.CreateUpArrowBrown();
                            break;
                        case Direction.Down:
                            arrowSprite = ProjectileSpriteFactory.Instance.CreateDownArrowBrown();
                            break;
                        case Direction.Left:
                            arrowSprite = ProjectileSpriteFactory.Instance.CreateLeftArrowBrown();
                            break;
                        case Direction.Right:
                            arrowSprite = ProjectileSpriteFactory.Instance.CreateRightArrowBrown();
                            break;
                        default:
                            arrowSprite = ProjectileSpriteFactory.Instance.CreateDownArrowBrown();
                            break;
                    }
                    gameObjects.Add((IGameObject)arrowSprite);
                    break;
                }
            case ItemType.Boomerang:
                {

                    //Need to do this for Link Projectile Conversions
                    if (currentDirection == Direction.Up)
                    {
                        boomerangSprite = ProjectileSpriteFactory.Instance.CreateBoomerangBrown((int)currentDirection);
                    } else if (currentDirection == Direction.Right)
                    {
                        boomerangSprite = ProjectileSpriteFactory.Instance.CreateBoomerangBrown((int)currentDirection - 2);
                    } else
                    {
                        boomerangSprite = ProjectileSpriteFactory.Instance.CreateBoomerangBrown((int)currentDirection + 1);
                    }
                    gameObjects.Add((IGameObject)boomerangSprite);
                    break;
                }
            case ItemType.Bomb:
                {
                    bombSprite = ProjectileSpriteFactory.Instance.CreateBomb();
                    gameObjects.Add((IGameObject)bombSprite);
                    break;

                }
            default:
                {
                    break;
                }
        }

        // Prepares to draw Link's Projectile and holds item direction information
        projectileDirection = currentDirection;
        initializeItem = true;
        
        System.Diagnostics.Debug.WriteLine("Item");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            HandleDeathStart();
        }
    }
    public void StartInvulnerability()
    {
        IsInvulnerable = true;
        Health = 0;
        invulnerabilityTimer = 2.0f; // Link remains invulnerable for 2 second.
        System.Diagnostics.Debug.WriteLine("Link is now invulnerable.");
    }

    public void EndInvulnerability()
    {
        IsInvulnerable = false;
        Health = 1;
        invulnerabilityTimer = 0;
        System.Diagnostics.Debug.WriteLine("Link is no longer invulnerable.");
    }

    public void HandleDeathStart()
    {
        System.Diagnostics.Debug.WriteLine("Link is dying...");
        // logic here to disable player input or play a sound.
    }

    public void HandleDeathCompletion()
    {
        System.Diagnostics.Debug.WriteLine("Link has died. Game Over.");
        // Trigger game over or level reset logic here.
    }

    public void Destroy()
    {

    }
}
