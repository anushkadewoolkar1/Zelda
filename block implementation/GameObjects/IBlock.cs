using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IBlock
{
    Vector2 GetPosition();  // Returns (x, y) position
    bool IsSolid();         // Whether the block is solid (collidable)
    bool IsPushable();      // Whether the block can be pushed
    bool Push(Vector2 direction); // Push the block in a given direction
    void Draw(SpriteBatch spriteBatch); // Render the block
}
