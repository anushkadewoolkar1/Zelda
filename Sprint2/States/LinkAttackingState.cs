using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LinkAttackingState : ILinkState
{
    private Link link;
    private float attackDuration;

    public LinkAttackingState(Link link)
    {
        this.link = link;
        attackDuration = 0.5f; // Attack lasts for half a second.
    }

    public void Enter()
    {
        link.SetAnimation("Attacking");
        link.PerformAttack();
    }

    public void Update(GameTime gameTime)
    {
        attackDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (attackDuration <= 0)
        {
            // After attacking, return to Idle.
            link.ChangeState(new LinkIdleState(link));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentAnimation(spriteBatch);
    }

    public void Exit()
    {
        // exit...
    }
}