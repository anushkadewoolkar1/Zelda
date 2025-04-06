using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame.Sprites;
using SpriteFactory;
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
        link.SetSprite(LinkSpriteFactory.Instance.CreateDeath());
        // He still needs to spin
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
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // Exit...
    }
}