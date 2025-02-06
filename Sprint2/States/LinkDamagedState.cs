using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums

public class LinkDamagedState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private float damagedDuration;

    public LinkDamagedState(Link link, Direction direction)
    {
        this.link = link;
        this.currentDirection = direction;
        damagedDuration = 1.0f; // Damaged state lasts for 1 second.
    }

    public void Enter()
    {
        switch (currentDirection)
        {
            case Direction.Up
                link.setSprite("LinkDamagedUp");
                break;
            case Direction.Down
                link.setSprite("LinkDamagedDown");
                break;
            case Direction.Left
                link.setSprite("LinkDamagedLeft");
                break;
            case Direction.Right
                link.setSprite("LinkDamagedRight");
                break;
        }

        link.StartInvulnerability();
    }

    public void Update(GameTime gameTime)
    {
        damagedDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (damagedDuration <= 0)
        {
            // Return to Idle after recovering.
            link.ChangeState(new LinkIdleState(link, currentDirection));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // flicker effect here maybe idk.
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // End the invulnerability 
        link.EndInvulnerability();
    }
}