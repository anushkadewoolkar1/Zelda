using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;

public class LinkAttackingState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private float attackDuration;

    public LinkAttackingState(Link link, Direction direction)
    {
        this.link = link;
        currentDirection = direction;
        attackDuration = 0.5f; // Attack lasts for half a second.
    }

    public void Enter()
    {
        switch (currentDirection)
        {
            case Direction.Up:
                link.SetSprite("LinkAttackingUp");
                break;
            case Direction.Down:
                link.SetSprite("LinkAttackingDown");
                break;
            case Direction.Left:
                link.SetSprite("LinkAttackingLeft");
                break;
            case Direction.Right:
                link.SetSprite("LinkAttackingRight");
                break;
        }

        link.PerformAttack();
    }

    public void Update(GameTime gameTime)
    {
        attackDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (attackDuration <= 0)
        {
            // After attacking, return to Idle.
            link.ChangeState(new LinkIdleState(link, currentDirection));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // exit...
    }
}