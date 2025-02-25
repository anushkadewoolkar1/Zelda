using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using Sprint0.Sprites;

public class LinkWalkingState : ILinkState
{
    private Link link;
    private Direction currentDirectionWalk;

    private int currentFrame;
    private float frameTimer;
    private const float frameInterval = 0.15f; // Time between frames
    private const int maxFrame = 2; // frames: 0, 1, 2

    public LinkWalkingState(Link link, Direction direction)
    {
        this.link = link;
        this.link.currentDirection =  direction;
        this.currentDirectionWalk = direction;
        currentFrame = 1;
        frameTimer = 0f;
    }

    public void Enter()
    {
        SetMove();
    }

    public void Update(GameTime gameTime)
    {
        frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (frameTimer > frameInterval)
        {
            frameTimer -= frameInterval;
            currentFrame++;
            if (currentFrame > maxFrame)
                currentFrame = 1;
            SetMove();
        }

        // checks if link became invulnerable or vulnerable
        if (link.invulnerabilityTimer == 1f || link.invulnerabilityTimer <= 0f)
        {
            SetMove();
        }

        // vector corresponding to current direction
        Vector2 movement = Vector2.Zero;
        switch (currentDirectionWalk)
        {
            case Direction.Up:
                movement = new Vector2(0, -1);
                break;
            case Direction.Down:
                movement = new Vector2(0, 1);
                break;
            case Direction.Left:
                movement = new Vector2(-1, 0);
                break; 
            case Direction.Right:
                movement = new Vector2(1, 0);
                break;
        }

        link.currentDirection = currentDirectionWalk;

        link.Move(movement, gameTime);
    }

    private void SetMove()
    {
        // Set Link’s walking animation based on direction
        switch (currentDirectionWalk)
        {
            case Direction.Up:
                link.SetSprite(LinkSpriteFactory.Instance.CreateUpWalk(currentFrame, 0, link.Health));
                break;
            case Direction.Down:
                link.SetSprite(LinkSpriteFactory.Instance.CreateDownWalk(currentFrame, 0, link.Health));
                break;
            case Direction.Left:
                link.SetSprite(LinkSpriteFactory.Instance.CreateLeftWalk(currentFrame, 0, link.Health));
                break;
            case Direction.Right:
                link.SetSprite(LinkSpriteFactory.Instance.CreateRightWalk(currentFrame, 0, link.Health));
                break;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // maybe if needed..
    }
}