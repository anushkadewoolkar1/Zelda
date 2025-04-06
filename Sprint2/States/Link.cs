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
using Zelda.Enums;


public class Link : IGameObject
{
    // constants

    public ILinkState currentState;
    public Vector2 Position { get; set; }
    public Boolean noMoving { get; set; }
    private ISprite currentSprite;
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
    private Vector2 velocity;

    private List<IGameObject> gameObjects;
    private LinkItemManager itemManager;

    // used for sound effects
    private GameAudio _audio;
    private double healthTimer = 0;

    // used for link winning the game
    public Level level { get; set; }

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

        LinkHurt = 1;
    }

    public void Update(List<IGameObject> _gameObjects,GameTime gameTime)
    {

        gameObjects = _gameObjects;
        currentState.Update(gameTime);
        currentSprite.Update(gameTime);

        itemManager.Update(gameTime);

        if(Health <= 1.0)
        {
            healthTimer++;
            if (healthTimer == 10.0)
            {
                healthTimer = 0;
                _audio.LowHealth();
            }
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

        System.Diagnostics.Debug.WriteLine($"Link's Position {Position} and {level.roomHeight}");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentSprite.Draw(spriteBatch, this.Position);

        // Draw item/projectiles via item manager
        itemManager.Draw(spriteBatch);
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

    public void TakeDamage(int damage)
    {
        Health -= damage;
        System.Diagnostics.Debug.WriteLine("Health: " + Health);

        _audio.LinkHit();

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
        // logic here to disable player input or play a sound.
    }

    public void HandleDeathCompletion()
    {
        System.Diagnostics.Debug.WriteLine("Link has died. Game Over.");
        // Trigger game over or level reset logic here.
    }

    public void AddGameObject(IGameObject gameObject)
    {
        gameObjects.Add(gameObject);
    }

    public void RemoveGameObject(IGameObject gameObject)
    {
        gameObjects.Remove(gameObject);
    }

    public void Destroy()
    {

    }
}
