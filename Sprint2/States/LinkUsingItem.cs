using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LinkUsingItemState : ILinkState
{
    private Link link;
    private float useItemDuration;

    public LinkUsingItemState(Link link)
    {
        this.link = link;
        useItemDuration = 0.5f; // Using an item lasts for half a second.
    }

    public void Enter()
    {
        // Set the using item animation and perform the item usage.
        link.SetAnimation("UsingItem");
        link.UseItem();
    }

    public void Update(GameTime gameTime)
    {
        useItemDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (useItemDuration <= 0)
        {
            // Return to Idle once done.
            link.ChangeState(new LinkIdleState(link));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentAnimation(spriteBatch);
    }

    public void Exit()
    {
        // Exit...
    }
}