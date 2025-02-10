using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using SpriteFactory;

public class Link
{
    private ILinkState currentState;
    public Vector2 Position { get; set; }
    private ISprite currentSprite;
    private ZeldaSpriteFactory LinkSpriteFactory;

    // Invulnerability settings.
    public bool IsInvulnerable { get; private set; }
    private float invulnerabilityTimer;

    // (pixels per second).
    private const float Speed = 100f;

    // Health property 
    public int Health { get; set; } = 3;

    public Link()
    {
        // Initialize the sprite factory (assuming it's implemented as a singleton).
        spriteFactory = SpriteFactory.Instance;
        Position = new Vector2(100, 100);
        currentSprite = spriteFactory.CreateLinkSprite("Idle");

        // Start in the Idle state.
        currentState = new LinkIdleState(this);
        currentState.Enter();
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
        currentState.Draw(spriteBatch);
    }

    public void ChangeState(ILinkState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void SetSprite(string spriteName)
    {
        currentSprite = spriteFactory.CreateLinkSprite(spriteName);
    }

    public void DrawCurrentSprite(SpriteBatch spriteBatch)
    {
        currentSprite.Draw(spriteBatch, Position);
    }

    public void Move(Vector2 direction)
    {
        // Normalize direction if it's not zero.
        if (direction != Vector2.Zero)
            direction.Normalize();

        float dt = 1f / 60f; // 1/60 second
        Position += direction * Speed * dt;
    }

    public void PerformAttack()
    {
        Console.WriteLine("Link performs an attack!");
        // Implement actual attack logic here.
    }

    public void UseItem()
    {
        Console.WriteLine("Link uses an item!");
        // Implement item usage logic here
    }

    public void StartInvulnerability()
    {
        IsInvulnerable = true;
        invulnerabilityTimer = 1.0f; // Link remains invulnerable for 1 second.
        Console.WriteLine("Link is now invulnerable.");
    }

    public void EndInvulnerability()
    {
        IsInvulnerable = false;
        invulnerabilityTimer = 0;
        Console.WriteLine("Link is no longer invulnerable.");
    }

    public void HandleDeathStart()
    {
        Console.WriteLine("Link is dying...");
        // logic here to disable player input or play a sound.
    }

    public void HandleDeathCompletion()
    {
        Console.WriteLine("Link has died. Game Over.");
        // Trigger game over or level reset logic here.
    }
}
