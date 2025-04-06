using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using MainGame.Sprites;
using SpriteFactory;
using MainGame.States;

public class LinkUsingItemState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private ItemType item;
    private float useItemDuration;

    public LinkUsingItemState(Link link, Direction direction, ItemType item)
    {
        this.link = link;
        this.currentDirection = direction;
        this.item = item;
        //useItemDuration = 0.5f; // Using an item lasts for half a second.
    }

    public void Enter()
    {
        this.currentDirection = this.link.currentDirection;
        if (this.link.CurrentItem.Exists(x => x.Equals(this.item))) {
            this.link.chooseItem = this.link.CurrentItem.IndexOf(this.item);
        } else
        {
            this.link.CurrentItem.Add(this.item);
            this.link.chooseItem = this.link.CurrentItem.Count - 1;
        }
        this.link.linkUseItem = true;

        useItemDuration = 0.2f; // Using an item lasts for half a second.
        // Set the using item animation 
        switch (currentDirection)
        {
            case Direction.Up:
                // For Up, the factory method takes (state, LinkHurt) as parameters.
                link.SetSprite(LinkSpriteFactory.Instance.CreateUseItemUp(1, link.LinkHurt));
                break;
            case Direction.Down:
                // For Down, assume the factory method takes (frame, state, LinkHurt) parameters.
                link.SetSprite(LinkSpriteFactory.Instance.CreateUseItemDown(1, 1, link.LinkHurt));
                break;
            case Direction.Left:
                link.SetSprite(LinkSpriteFactory.Instance.CreateUseItemLeft(1, link.LinkHurt));
                break;
            case Direction.Right:
                link.SetSprite(LinkSpriteFactory.Instance.CreateUseItemRight(1, link.LinkHurt));
                break;
        }
        link.UseItem();
    }

    public void Update(GameTime gameTime)
    {
        useItemDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (useItemDuration <= 0)
        {
            // Return to Idle once done.
            this.link.linkUseItem = false;
            link.ChangeState(new LinkIdleState(link, currentDirection));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // Exit...
    }
}