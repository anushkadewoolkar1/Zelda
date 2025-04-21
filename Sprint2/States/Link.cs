using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MainGame;
using MainGame.CollisionHandling;
using MainGame.Display;
using MainGame.Sprites;
using MainGame.States;
using MainGame.Forces;
using Zelda.Enums;
using MainGame.Visibility;


public class Link : IGameObject
{
    // constants

    public ILinkState currentState;
    public Vector2 Position { get; set; }
    public Boolean noMoving { get; set; }
    private ISprite currentSprite;
    public ISprite CurrentSprite => currentSprite;
    private LinkSpriteFactory spriteFactory;

    // Invulnerability settings.
    public bool IsInvulnerable { get; private set; }
    public float invulnerabilityTimer;

    // (pixels per second).
    private const float Speed = 100f;

    // Health 
    public int Health { get; set; } = 3;

    // Link's hurt
    public int LinkHurt { get; set; }

    // Iventory and item usage
    public List<ItemType> CurrentItem { get; set; }
    public int chooseItem { get; set; }
    public Boolean linkAttacking { get; set; }
    public Boolean linkUseItem { get; set; }
    public Boolean swordBeam { get; set; }

    // Current facing direction 
    public Direction currentDirection { get; set; }

    // other fields for movement
    public Vector2 velocity { get; set; }
    private Vector2 lastNonZeroVelocity;

    private List<IGameObject> gameObjects;
    private LinkItemManager itemManager;

    // used for sound effects
    public GameAudio _audio { get; set; }
    private double healthTimer = 0;

    // used for link winning the game
   // public Level level { get; set; }
    public LevelManager level { get; set; }

    public float SpeedMultiplier { get; set; } = 1.0f;
    public float LinkScale { get; set; } = 2f;

    private int runningCounter = 0;
    private int runningCooldown = 100;
    private bool running = false;

    private Gravity gravity;
    private FogOfWar fow = FogOfWar.Instance;
    private Portal portal = Portal.Instance;

    public Link(List<IGameObject> _gameObjects)
    {
        // Initialize the sprite factory 
        spriteFactory = LinkSpriteFactory.Instance;
        Position = new Vector2(240, 250);

        // Start initial sprite
        currentSprite = LinkSpriteFactory.Instance.CreateDownWalk(1, 0, 0);
        currentState = new LinkWalkingState(this, Direction.Down);
        currentState.Enter();

        // Set invulnerability to false and timer to 0.
        IsInvulnerable = false;
        invulnerabilityTimer = 0f;

        // item related properties
        CurrentItem = new List<ItemType>();
        linkAttacking = false;
        swordBeam = false;

        gameObjects = _gameObjects;
        itemManager = new LinkItemManager(this, gameObjects);

        _audio = GameAudio.Instance;

        CurrentItem.Add(ItemType.WoodenSword);

        LinkHurt = 1;

        gravity = Gravity.Instance;
    }

    public void Update(List<IGameObject> _gameObjects,GameTime gameTime)
    {
        gravity.ApplyForce(this);

        gameObjects = _gameObjects;
        currentState.Update(gameTime);
        currentSprite.Update(gameTime);

        itemManager.Update(gameTime);

        DetermineSpeed();
        if(Health <= 1.0)
        {
            healthTimer++;
            if (healthTimer == 10.0)
            {
                healthTimer = 0;
                _audio.LowHealth();
            }
        }

        if (running)
        {
            UpdateRunning();
        }
        else if (!running && runningCooldown < 100)
        {
            UpdateCooldown();
        }


        // Handle invulnerability timer
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

        fow.UpdateLink(this);

        //System.Diagnostics.Debug.WriteLine($"Link's Position {Position} and {level.roomHeight}");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentSprite.Draw(spriteBatch, this.Position);

        // Draw item/projectiles via item manager
        itemManager.Draw(spriteBatch);

        // Draw portal
        portal.DrawPortal(spriteBatch);
    }

    public void ChangeState(ILinkState newState)
    {
        if ((!linkAttacking && !linkUseItem))
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
        if (noMoving) return; // Used for level transitions

        // Normalize direction if it's not zero.
        if (direction != Vector2.Zero)
            direction.Normalize();

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Position += direction * Speed * SpeedMultiplier * dt;
        lastNonZeroVelocity = velocity;
        velocity = direction;
    }

    public Rectangle BoundingBox
    {
        get
        {
            int width = (int)(16 * LinkScale);
            int height = (int)(16 * LinkScale);

            int x = (int)Math.Round(Position.X - width / 2f);
            int y = (int)Math.Round(Position.Y - height / 2f);

            return new Rectangle(x, y, width, height);
        }
    }
    public Vector2 Velocity
    {
        get { return velocity;  }
    }

    public void PerformAttack(Direction direction)
    {
        _audio.SwordSwing();
        System.Diagnostics.Debug.WriteLine("Link performs an attack!");
    }

    public void PickUpItem(ItemSprite pickedUpItem)
    {
        System.Diagnostics.Debug.WriteLine("Link picks up item");

        if (pickedUpItem.GetPickedUp() == false)
        {
            //only play if it hasn't been picked up yet
            switch(pickedUpItem.GetItemString())
            {
                case "ZeldaSpriteBoomerang":
                    _audio.PickUpBetterItem();
                    break;
                case "ZeldaSpriteBow":
                    _audio.PickUpBetterItem();
                    CurrentItem.Add(ItemType.Bow);
                    break;
                case "ZeldaSpriteCompass":
                    _audio.PickUpBetterItem();
                    CurrentItem.Add(ItemType.Compass);
                    break;
                case "ZeldaSpriteFairy":
                    _audio.PickUpBetterItem();
                    break;
                case "ZeldaSpriteHeartContainer":
                    _audio.PickUpBetterItem();
                    break;
                case "ZeldaSpriteMap":
                    _audio.PickUpBetterItem();
                    CurrentItem.Add(ItemType.Map);
                    break;
                case "ZeldaSpriteTriforce_frame_000":
                    _audio.PickUpBetterItem();
                    break;
                case "ZeldaSpriteTriforce_frame_001":
                    _audio.PickUpBetterItem();
                    break;
                case "ZeldaSprite5Rupies":
                    _audio.CollectRupee();
                    break;
                default:
                    _audio.PickUpItem();
                    break;

            }
        }

        pickedUpItem.SetPickedUp();

        switch (pickedUpItem.GetItemString())
        {
            case "ZeldaSpriteArrow": 
                CurrentItem.Add(ItemType.Arrow);  
                break;
            case "ZeldaSpriteBoomerang": 
                CurrentItem.Add(ItemType.Boomerang); 
                break;
            case "ZeldaSpriteBomb": 
                CurrentItem.Add(ItemType.Bomb); 
                break;
        }
    }

    public void UseItem()
    {
        if (linkAttacking) return;
        System.Diagnostics.Debug.WriteLine("Link uses an item...");

        // Delegate item logic to itemManager
        if (chooseItem < CurrentItem.Count)
        {
            var itemType = CurrentItem[chooseItem];
            itemManager.UseItem(itemType, currentDirection);
        }
    }
    public void CycleInventory()
    {
        if (CurrentItem.Count == 0)
            return;
        do
        {
            chooseItem = (chooseItem + 1) % CurrentItem.Count;
            System.Diagnostics.Debug.WriteLine("Selected inventory index: " + chooseItem);
        }
        while (CurrentItem[chooseItem] != ItemType.Boomerang
        && CurrentItem[chooseItem] != ItemType.Arrow
        && CurrentItem[chooseItem] != ItemType.Bomb
        && CurrentItem[chooseItem] != ItemType.WoodenSword);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        System.Diagnostics.Debug.WriteLine("Health: " + Health);

        _audio.LinkHit();

        if (linkAttacking == true)
        {
            linkAttacking = false;
            RemoveGameObject(new HitBox(Position, currentDirection)); //<---.....
            ChangeState(new LinkWalkingState(this, currentDirection));
        }

        if (Health <= 0)
        {
            Health = 0;
            ChangeState(new LinkDyingState(this));
        }
    }
    public void StartInvulnerability()
    {
        IsInvulnerable = true;
        invulnerabilityTimer = 2.0f; // Link remains invulnerable for 2 second.
        LinkHurt = 0;
        System.Diagnostics.Debug.WriteLine("Link is now invulnerable.");
    }

    public void EndInvulnerability()
    {
        IsInvulnerable = false;
        invulnerabilityTimer = 0;
        LinkHurt = 1;
        System.Diagnostics.Debug.WriteLine("Link is no longer invulnerable.");
    }

    public void HandleDeathStart()
    {
        System.Diagnostics.Debug.WriteLine("Link is dying...");
    }

    public void HandleDeathCompletion()
    {
        System.Diagnostics.Debug.WriteLine("Link has died. Game Over.");
    }

    public void AddGameObject(IGameObject gameObject)
    { 
        gameObjects.Add(gameObject);
    }

    public void RemoveGameObject(IGameObject gameObject)
    {
        if (gameObject is HitBox)
        {
            gameObjects.RemoveAll(obj => obj is HitBox);
        }
        else
        {
            gameObjects.Remove(gameObject);
        }
    }

    public void Destroy()
    {

    }

    public void FireSpreadArrows()
    {
        itemManager.UseItem(ItemType.Arrow, Direction.Up);
        itemManager.UseItem(ItemType.Arrow, Direction.Down);
        itemManager.UseItem(ItemType.Arrow, Direction.Left);
        itemManager.UseItem(ItemType.Arrow, Direction.Right);
    }

    public void StartRunning()
    {
        running = true;

        switch (Health)
        {
            case 1:
                SpeedMultiplier = 1.0f;
                break;
            case 2:
                SpeedMultiplier = 2.0f;
                break;
            case 3:
                SpeedMultiplier = 3.0f;
                break;
        }
    }

    public void UpdateRunning()
    {
        runningCounter++;
        if (runningCounter >= 50)
        {
            runningCounter = 0;
            running = false;
            runningCooldown--;
        }
    }

    public void UpdateCooldown()
    {
        SpeedMultiplier = 1.0f;
        runningCooldown--;
        System.Diagnostics.Debug.WriteLine("Link has run out of stamina...");

        if (runningCooldown <= 0)
        {
            runningCooldown = 100;
        }
    }

    public void DetermineSpeed()
    {
        switch(Health)
        {
            case 1:
                SpeedMultiplier = 1.0f;
                break;
            case 2:
                SpeedMultiplier = 1.5f;
                break;
            case 3:
                SpeedMultiplier = 2.0f;
                break;
        }
    }

    public void UsePortal()
    {
        portal.ApplyForce((IGameObject)this);
    }
}
