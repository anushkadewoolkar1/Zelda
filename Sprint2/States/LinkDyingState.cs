using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;

public class LinkDyingState : ILinkState
{
    private Link link;
    private float dyingDuration;

    public LinkDyingState(Link link)
    {
        this.link = link;
        dyingDuration = 2.0f; // Dying animation lasts 2 seconds.
    }

    public void Enter()
    {
        // dying logic.
        link.SetAnimation("Dying");
        link.HandleDeathStart();
    }

    public void Update(GameTime gameTime)
    {
        dyingDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (dyingDuration <= 0)
        {
            // After the dying animation, handle game over logic.
            link.HandleDeathCompletion();
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