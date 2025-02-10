using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using Sprint0.Sprites;
using SpriteFactory;

public class LinkUsingItemState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private float useItemDuration;

    public LinkUsingItemState(Link link, Direction direction)
    {
        this.link = link;
        this.currentDirection = direction;
        useItemDuration = 0.5f; // Using an item lasts for half a second.
    }

    public void Enter()
    {
        // Set the using item animation 
        switch (currentDirection)
        {
            case Direction.Up:
                // For Up, the factory method takes (state, health) as parameters.
                link.SetSprite(LinkSpriteFactory.Instance.CreateUseItemUp(1, link.Health));
                break;
            case Direction.Down:
                // For Down, assume the factory method takes (frame, state, health) parameters.
                link.SetSprite(LinkSpriteFactory.Instance.CreateUseItemDown(1, 1, link.Health));
                break;
            case Direction.Left:
                link.SetSprite(LinkSpriteFactory.Instance.CreateUseItemLeft(1, link.Health));
                break;
            case Direction.Right:
                link.SetSprite(LinkSpriteFactory.Instance.CreateUseItemRight(1, link.Health));
                break;
        }

        link.UseItem();
    }

    public void Update(GameTime gameTime)
    {
        useItemDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (useItemDuration <= 0)
        {
            // Return to Idle once done.
            link.ChangeState(new LinkWalkingState(link, currentDirection));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // Exit...
    }
}