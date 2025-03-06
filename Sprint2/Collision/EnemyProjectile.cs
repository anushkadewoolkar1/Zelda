using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.CollisionHandling; 
using Sprint0.Sprites;

namespace Sprint0.Collision
{ 

    public class EnemyProjectile : IGameObject
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        // representation of projectile for collision
        public EnemyProjectileSprite Sprite { get; private set; }

        public EnemyProjectile(Vector2 startPosition, Vector2 initialVelocity, EnemyProjectileSprite sprite)
        {
            Position = startPosition;
            Velocity = initialVelocity;
            Sprite = sprite;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * dt;
            Sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position);
        }

        public Rectangle BoundingBox
        {
            get
            {
                int width = Sprite.spriteSize;
                int height = Sprite.spriteSize * 2;
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        public void Destroy()
        {
            Sprite = null;
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
        }
    }
}
