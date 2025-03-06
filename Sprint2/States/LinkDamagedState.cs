using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using Sprint0.Sprites;
using SpriteFactory;

public class LinkDamagedState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private float damagedDuration;
    private ILinkState previousState;

    public LinkDamagedState(Link link, Direction direction, ILinkState previousState)
    {
        this.link = link;
        this.currentDirection = direction;
        this.previousState = previousState;
        damagedDuration = 2.0f; // Damaged state lasts for 1 second.
    }

    public void Enter()
    {
        link.StartInvulnerability();
        //link.LinkSprite.SetDamaged(true);
    }

    public void Update(GameTime gameTime)
    {
        damagedDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (damagedDuration <= 0)
        {
            // Once the damaged duration is over, revert to the previous state
            if (previousState != null)
                link.ChangeState(previousState);
            else
                link.ChangeState(new LinkWalkingState(link, currentDirection));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // End the invulnerability 
        link.EndInvulnerability();
        //link.LinkSprite.SetDamaged(false);
    }
}