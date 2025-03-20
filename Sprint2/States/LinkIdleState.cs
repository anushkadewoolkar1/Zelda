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
