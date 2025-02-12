using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using Sprint0.Sprites;

public class LinkAttackingState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private SwordType swordType;
    private float attackDuration;
    private int attackFrame;

    public LinkAttackingState(Link link, Direction direction)
    {
        this.link = link;
        this.currentDirection = direction;
        this.swordType = swordType;
        attackDuration = 0.5f; // Attack lasts for half a second.
        attackFrame = 1;
    }

    public void Enter()
    {
        attackDuration = .5f;
       
        switch (currentDirection)
        {
            case Direction.Up:
                if (swordType == SwordType.WoodenSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateUpAttackWoodenSword(attackFrame, 1, link.Health));
                else if (swordType == SwordType.WhiteSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateUpAttackWhiteSword(attackFrame, 1, link.Health));
                else if (swordType == SwordType.MagicalSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateUpAttackMagicalSword(attackFrame, 1, link.Health));
                break;
            case Direction.Down:
                if (swordType == SwordType.WoodenSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateDownAttackWoodenSword(attackFrame, 1, link.Health));
                else if (swordType == SwordType.WhiteSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateDownAttackWhiteSword(attackFrame, 1, link.Health));
                else if (swordType == SwordType.MagicalSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateDownAttackMagicalSword(attackFrame, 1, link.Health));
                break;
            case Direction.Left:
                if (swordType == SwordType.WoodenSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateLeftAttackWoodenSword(attackFrame, 1, link.Health));
                else if (swordType == SwordType.WhiteSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateLeftAttackWhiteSword(attackFrame, 1, link.Health));
                else if (swordType == SwordType.MagicalSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateLeftAttackMagicalSword(attackFrame, 1, link.Health));
                break;
            case Direction.Right:
                if (swordType == SwordType.WoodenSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateRightAttackWoodenSword(attackFrame, 1, link.Health));
                else if (swordType == SwordType.WhiteSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateRightAttackWhiteSword(attackFrame, 1, link.Health));
                else if (swordType == SwordType.MagicalSword)
                    link.SetSprite(LinkSpriteFactory.Instance.CreateRightAttackMagicalSword(attackFrame, 1, link.Health));
                break;
        }

        link.PerformAttack();
    }

    public void Update(GameTime gameTime)
    {
        attackDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (attackDuration <= 0)
        {
            // After attacking, return idle
            link.ChangeState(new LinkWalkingState(link, currentDirection));
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