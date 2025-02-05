using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LinkWalkingState : ILinkState
{
    private Link link;

    public LinkWalkingState(Link link)
    {
        this.link = link;
    }

    public void Enter()
    {
        // Set Link’s walking animation.
        link.SetAnimation("Walking");
    }

    public void Update(GameTime gameTime)
    {
        // Move Link 
        Vector2 direction = Input.GetMovementVector();
        link.Move(direction);

        
        if (direction == Vector2.Zero)
        {
            link.ChangeState(new LinkIdleState(link));
        }
        // this allows attacking while moving.
        else if (Input.IsAttackKeyPressed())
        {
            link.ChangeState(new LinkAttackingState(link));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentAnimation(spriteBatch);
    }

    public void Exit()
    {
        // maybe if needed..
    }
}