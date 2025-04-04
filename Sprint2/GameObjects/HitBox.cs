using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Zelda.CollisionMap;
using Sprint0.CollisionHandling;
using Sprint0.Display;
using Zelda.Enums;
public class HitBox : IGameObject

{
    private Rectangle hitRectangle;
    private Vector2 _direction;
    private int SCALE = 32;
    public int scalePropertyHit
    {
        get
        {
            return SCALE + 1;
        }
    }

    //Keeps track of the current direction of the hitbox and creates a hitbox in that direction
    public HitBox(Vector2 startPosition, Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                hitRectangle = new Rectangle((int)startPosition.X - (SCALE), (int)startPosition.Y, SCALE, SCALE);
                _direction = new Vector2(-1, 0);
                break;
            case Direction.Right:
                hitRectangle = new Rectangle((int)startPosition.X + (SCALE / 2), (int)startPosition.Y, SCALE, SCALE);
                _direction = new Vector2(1, 0);
                break;
            case Direction.Up:
                hitRectangle = new Rectangle((int)startPosition.X, (int)startPosition.Y - (SCALE), SCALE, SCALE);
                _direction = new Vector2(0, -1);
                break;
            default:
                hitRectangle = new Rectangle((int)startPosition.X, (int)startPosition.Y + (SCALE / 2), SCALE, SCALE);
                _direction = new Vector2(0, 1);
                break;
        }
    }

    public Rectangle BoundingBox
    {
        get
        {
            return hitRectangle;
        }
    }

    public Vector2 Velocity
    {
        get
        {
            return _direction;
        }
    }

    public new void Destroy()
    {
        // Do nothing
    }
}
