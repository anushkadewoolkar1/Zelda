using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LinkIdleState : ILinkState
{
    private Link link;

    public LinkIdleState(Link link)
    {
        this.link = link;
    }

    public void Enter()
    {
        link.SetAnimation("Idle");
    }

    public void Update(GameTime gameTime)
    {
        // Check for input to transition to another state.
        if (Input.IsMovementKeyPressed())
        {
            link.ChangeState(new LinkWalkingState(link));
        }
        else if (Input.IsAttackKeyPressed())
        {
            link.ChangeState(new LinkAttackingState(link));
        }
        else if (Input.IsUseItemKeyPressed())
        {
            link.ChangeState(new LinkUsingItemState(link));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentAnimation(spriteBatch);
    }

    public void Exit()
    {
        // maybe if needed
    }
}