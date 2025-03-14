using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using Sprint0.Sprites;
using SpriteFactory;


public class LinkIdleState : ILinkState
{
    private Link link;
    private Direction currentDirection;

    public LinkIdleState(Link link, Direction direction)
    {
        this.link = link;
        link.currentDirection = direction;
        this.currentDirection = direction;
    }

    public void Enter()
    {
        this.currentDirection = this.link.currentDirection;
        SetIdleSprite();
    }

    public void Update(GameTime gameTime)
    {
        // updates the facing direction 
        // might need to change based on how input is handled

        /* !!!!!! Fully take this part out due to this being now the responsibility of the Command class? (PP)
         * 
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
        */
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
        // Here, we use frame 0 as the idle (standing) frame.
        switch (currentDirection)
        {
            case Direction.Up:
                link.SetSprite(LinkSpriteFactory.Instance.CreateUpWalk(1, 1, link.Health));
                break;
            case Direction.Down:
                link.SetSprite(LinkSpriteFactory.Instance.CreateDownWalk(1, 1, link.Health));
                break;
            case Direction.Left:
                link.SetSprite(LinkSpriteFactory.Instance.CreateLeftWalk(1, 1, link.Health));
                break;
            case Direction.Right:
                link.SetSprite(LinkSpriteFactory.Instance.CreateRightWalk(1, 1, link.Health));
                break;
        }
    }
}
