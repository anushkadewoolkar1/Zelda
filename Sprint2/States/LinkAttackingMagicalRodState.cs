using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda.Enums;
using MainGame.Sprites;
using SpriteFactory;

public class LinkAttackingMagicalRodState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private float attackDuration;
    private int attackFrame; // For animation frames 

    public LinkAttackingMagicalRodState(Link link, Direction direction)
    {
        this.link = link;
        this.currentDirection = direction;
        attackDuration = 0.5f; // Adjust duration as needed for the rod attack
        attackFrame = 0;
    }

    public void Enter()
    {
        
        switch (currentDirection)
        {
            case Direction.Up:
                link.SetSprite(LinkSpriteFactory.Instance.CreateUpAttackMagicalRod(attackFrame, 1, link.Health));
                break;
            case Direction.Down:
                link.SetSprite(LinkSpriteFactory.Instance.CreateDownAttackMagicalRod(attackFrame, 1, link.Health));
                break;
            case Direction.Left:
                link.SetSprite(LinkSpriteFactory.Instance.CreateLeftAttackMagicalRod(attackFrame, 1, link.Health));
                break;
            case Direction.Right:
                link.SetSprite(LinkSpriteFactory.Instance.CreateRightAttackMagicalRod(attackFrame, 1, link.Health));
                break;
        }

        // Execute Link's attack logic for the rod.
        //link.PerformAttack();

        // Spawn a projectile or apply ranged effects here maybe
    }

    public void Update(GameTime gameTime)
    {
        attackDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (attackDuration <= 0)
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
