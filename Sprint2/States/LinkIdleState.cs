using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;

public class LinkIdleState : ILinkState
{
    private Link link;
    private Direction currentDirection;

    public LinkIdleState(Link link)
    {
        this.link = link;
        currentDirection = direction
    }

    public void Enter()
    {
        link.SetSprite("Idle");
    }

    public void Update(GameTime gameTime)
    {
        // updates the facing direction 
        // might need to change based on how input is handled
        if (Input.IsLeftPressed() && currentDirection != Direction.Left)
        {
            currentDirection = Direction.Left;
            SetIdleSprite();
        }
        else if (Input.IsRightPressed() && currentDirection != Direction.Right)
        {
            currentDirection = Direction.Right;
            SetIdleSprite();
        }
        else if (Input.IsUpPressed() && currentDirection != Direction.Up)
        {
            currentDirection = Direction.Up;
            SetIdleSprite();
        }
        else if (Input.IsDownPressed() && currentDirection != Direction.Down)
        {
            currentDirection = Direction.Down;
            SetIdleSprite();
        }

        // Transition to another state if the player initiates movement or another action.
        if (Input.IsMovementKeyPressed())
        {
            link.ChangeState(new LinkWalkingState(link, currentDirection));
        }
        else if (Input.IsAttackKeyPressed())
        {
            link.ChangeState(new LinkAttackingState(link, currentDirection));
        }
        else if (Input.IsUseItemKeyPressed())
        {
            link.ChangeState(new LinkUsingItemState(link, currentDirection));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // maybe if needed
    }

    // Sets the correct idle sprite based on current direction.
    private void SetIdleSprite()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                link.SetSprite("LinkIdleUp");
                break;
            case Direction.Down:
                link.SetSprite("LinkIdleDown");
                break;
            case Direction.Left:
                link.SetSprite("LinkIdleLeft");
                break;
            case Direction.Right:
                link.SetSprite("LinkIdleRight");
                break;
        }
    }
}