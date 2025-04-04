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

    public LinkAttackingState(Link link, Direction direction, SwordType swordType)
    {
        this.link = link;
        this.currentDirection = direction;
        this.swordType = swordType;
        attackDuration = 0.5f; // Attack lasts for half a second.
        attackFrame = 1;
    }

    public void Enter()
    {
        this.currentDirection = this.link.currentDirection;
        
        if (this.link.swordBeam)
        {
            return;
        }

        attackDuration = 0.5f;

        SetAttack();

        link.PerformAttack(currentDirection);
    }

    public void Update(GameTime gameTime)
    {

        attackDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.link.linkAttacking = true;
        this.link.AddGameObject(new HitBox(this.link.Position, this.link.currentDirection));
        if (attackDuration > .375f)
        {
            attackFrame = 2;
            SetAttack();
        }
        else if (attackDuration > .25f)
        {
            attackFrame = 3;
            SetAttack();
        } else if (attackDuration > .125f) {
            attackFrame = 4;
            SetAttack();
        } else
        {
            this.link.RemoveGameObject(new HitBox(this.link.Position, this.link.currentDirection));
            this.link.linkAttacking = false;
            // After attacking, return idle
            link.ChangeState(new LinkIdleState(link, currentDirection));
        }
    }

    private void SetAttack()
    {
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