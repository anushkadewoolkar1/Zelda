using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IBlock
{
    Vector2 GetPosition();  //returns (x, y) position
    bool IsSolid();         //whether the block is solid (collidable)
    bool IsPushable();      //whether the block can be pushed
    bool Push(Vector2 direction); //push the block in a given direction
    void Draw(SpriteBatch spriteBatch); //draw the block
}