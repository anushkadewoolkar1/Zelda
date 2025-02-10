using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using Sprint0.Sprites;

public class LinkWalkingState : ILinkState
{
    private Link link;
    private Direction currentDirection;

    public LinkWalkingState(Link link, Direction direction)
    {
        this.link = link;
        this.currentDirection = direction;
    }

    public void Enter()
    {
        // Set Link’s walking animation based on direction
        switch (currentDirection)
        {
            case Direction.Up:
                link.SetSprite(LinkSpriteFactory.Instance.CreateUpWalk(0, 1, link.Health));
                break;
            case Direction.Down:
                link.SetSprite(LinkSpriteFactory.Instance.CreateDownWalk(0, 1, link.Health));
                break;
            case Direction.Left:
                link.SetSprite(LinkSpriteFactory.Instance.CreateLeftWalk(0, 1, link.Health));
                break;
            case Direction.Right:
                link.SetSprite(LinkSpriteFactory.Instance.CreateRightWalk(0, 1, link.Health));
                break;
        }
    }

    public void Update(GameTime gameTime)
    {
        // vector corresponding to current direction
        Vector2 movement = Vector2.Zero;
        switch (currentDirection)
        {
            case Direction.Up:
                movement = new Vector2(0, -1);
                break;
            case Direction.Down:
                movement = new Vector2(0, 1);
                break;
            case Direction.Left:
                movement = new Vector2(-1, 0);
                break; 
            case Direction.Right:
                movement = new Vector2(1, 0);
                break;
        }

        link.Move(movement, gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // maybe if needed..
    }
}