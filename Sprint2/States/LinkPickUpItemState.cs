using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using MainGame.Sprites;
using SpriteFactory;

public class LinkPickUpItemState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private float pickUpDuration;

    
    public LinkPickUpItemState(Link link, Direction direction)
    {
        this.link = link;
        this.currentDirection = direction;
        pickUpDuration = 0.5f; // Duration for the pick-up animation 
    }

    public void Enter()
    {
        // The parameters (here 1 for state and link.Health) 
        link.SetSprite(LinkSpriteFactory.Instance.CreatePickUpItemOne(1, link.Health));

        //link.PickUpItem();
    }

    public void Update(GameTime gameTime)
    {
        pickUpDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (pickUpDuration <= 0)
        {
            link.ChangeState(new LinkWalkingState(link, currentDirection));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // ...
    }
}