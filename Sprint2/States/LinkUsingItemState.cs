using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums

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
                link.SetSprite("UsingItemUp");
                break;
            case Direction.Down:
                link.SetSprite("UsingItemDown");
                break;
            case Direction.Left:
                link.SetSprite("UsingItemLeft");
                break;
            case Direction.Right:
                link.SetSprite("UsingItemRight");
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
            link.ChangeState(new LinkIdleState(link, currentDirection));
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