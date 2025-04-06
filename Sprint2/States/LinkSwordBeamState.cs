using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zelda.Enums;
using MainGame.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class LinkSwordBeamState : ILinkState
{
    private Link link;
    private Direction currentDirection;
    private float beamDuration;
    private int beamFrame; // For animation

    public LinkSwordBeamState(Link link, Direction direction)
    {
        this.link = link;
        this.currentDirection = direction;
        beamDuration = 0.5f; // Beam lasts for half a second 
        beamFrame = 0;
    }

    public void Enter()
    {

        // Set the sword beam sprite via the sprite factory.
        // once createSwordBeam is in sprite factory.)
        //link.SetSprite(LinkSpriteFactory.Instance.CreateSwordBeam(currentDirection, beamFrame, 1, link.Health));

        // Fire the sword beam projectile.
        // (Assume you have a SwordBeam projectile class and a projectile manager.)
        // SwordBeam beam = new SwordBeam(link.Position, currentDirection, beamSprite);
        // ProjectileManager.Instance.AddProjectile(beam);
        
    }

    public void Update(GameTime gameTime)
    {
        beamDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (beamDuration <= 0)
        {
            // After the beam attack, transition back to the walking state 
            link.ChangeState(new LinkWalkingState(link, currentDirection));
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        link.DrawCurrentSprite(spriteBatch);
    }

    public void Exit()
    {
        // Any cleanup if needed.
    }
}