using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LinkDamagedState : ILinkState
{
    private Link link;
    private float damagedDuration;

    public LinkDamagedState(Link link)
    {
        this.link = link;
        damagedDuration = 1.0f; // Damaged state lasts for 1 second.
    }

    public void Enter()
    {
        link.SetAnimation("Damaged");
        link.StartInvulnerability();
    }

    public void Update(GameTime gameTime)
    {
        damagedDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (damagedDuration <= 0)
        {
            // Return to Idle after recovering.
            link.ChangeState(new LinkIdleState(link));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // flicker effect here maybe idk.
        link.DrawCurrentAnimation(spriteBatch);
    }

    public void Exit()
    {
        // End the invulnerability 
        link.EndInvulnerability();
    }
}