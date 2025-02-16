using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using Zelda.Enums;


public class Link
{
    private ILinkState currentState;
    public Vector2 Position { get; set; }
    private ISprite currentSprite;
    private LinkSpriteFactory spriteFactory;

    // Invulnerability settings.
    public bool IsInvulnerable { get; private set; }
    private float invulnerabilityTimer;

    // (pixels per second).
    private const float Speed = 100f;

    // Health property 
    public int Health { get; set; } = 2;
    // property for which item is currently selected
    public ItemType CurrentItem { get; set; }  

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
        CurrentItem = ItemType.Arrow;

    }

    public void Update(GameTime gameTime)
    {

        currentState.Update(gameTime);
        currentSprite.Update(gameTime);

        // Update invulnerability timer if Link is invulnerable.
        if (IsInvulnerable)
        {
            invulnerabilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (invulnerabilityTimer <= 0)
            {
                EndInvulnerability();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentSprite.Draw(spriteBatch, this.Position);
    }

    public void ChangeState(ILinkState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
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
    }

    public void PerformAttack(Direction direction)
    {
        System.Diagnostics.Debug.WriteLine("Link performs an attack!");

        //// Define the dimensions of the attack hitbox.
        //int hitboxWidth = 20;
        //int hitboxHeight = 20;
        //Rectangle attackHitbox;

        //// Determine the hitbox location based on Link's current facing direction.
        //switch (FacingDirection)
        //{
        //    case Direction.Up:
        //        attackHitbox = new Rectangle((int)Position.X, (int)Position.Y - hitboxHeight, hitboxWidth, hitboxHeight);
        //        break;
        //    case Direction.Down:
        //        attackHitbox = new Rectangle((int)Position.X, (int)Position.Y + hitboxHeight, hitboxWidth, hitboxHeight);
        //        break;
        //    case Direction.Left:
        //        attackHitbox = new Rectangle((int)Position.X - hitboxWidth, (int)Position.Y, hitboxWidth, hitboxHeight);
        //        break;
        //    case Direction.Right:
        //        attackHitbox = new Rectangle((int)Position.X + hitboxWidth, (int)Position.Y, hitboxWidth, hitboxHeight);
        //        break;
        //    default:
        //        // Fallback if FacingDirection isn't set.
        //        attackHitbox = new Rectangle((int)Position.X, (int)Position.Y, hitboxWidth, hitboxHeight);
        //        break;
        //}

        //Console.WriteLine("Attack hitbox: " + attackHitbox.ToString());

        // Uncomment the following code when enemy collision is implemented:
        /*
        foreach (var enemy in EnemyManager.Instance.Enemies)
        {
            if (attackHitbox.Intersects(enemy.BoundingBox))
            {
                enemy.TakeDamage(1); // Apply damage to the enemy.
            }
        }
        */

        // play an attack sound.
        // SoundManager.Instance.PlaySound("LinkAttack");
    }

    public void PickUpItem()
    {
        System.Diagnostics.Debug.WriteLine("Link picks up item");
        // Implement item pick up logic
    }

    public void UseItem()
    {
        System.Diagnostics.Debug.WriteLine("Link uses an item!");

        switch (CurrentItem)
        {
            case ItemType.Arrow:
                {
                    ISprite arrowSprite;
                    switch (FacingDirection)
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
                    break;
                }
            case ItemType.Boomerang:
                {
                    ISprite boomerangSprite;
                    boomerangSprite = ProjectileSpriteFactory.Instance.CreateBoomerangBrown();
                    break;
                }
            case ItemType.Bomb:
                {
                    ISprite bombSprite = ProjectileSpriteFactory.Instance.CreateBomb();
                    break;
                }
            default:
                {
                    Console.WriteLine("No valid item selected.");
                    break;
                }
        }
    }

    public void StartInvulnerability()
    {
        IsInvulnerable = true;
        invulnerabilityTimer = 1.0f; // Link remains invulnerable for 1 second.
        System.Diagnostics.Debug.WriteLine("Link is now invulnerable.");
    }

    public void EndInvulnerability()
    {
        IsInvulnerable = false;
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
}
