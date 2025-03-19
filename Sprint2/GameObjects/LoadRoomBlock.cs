using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Zelda.CollisionMap;
using Sprint0.Display;

public class LoadRoomBlock : Block
{
    private Level level;
    private int targetX, targetY;

    private const int COLLISION_THRESH = 10;

    public LoadRoomBlock(Vector2 startPosition, Texture2D[] blockTextures, Level level, int x, int y)
        : base(startPosition, blockTextures)
    {
        this.level = level;
        targetX = x;
        targetY = y;
    }

    public virtual bool IsSolid() => true;

    public void CheckCollision(Vector2 playerPosition)
    {
        if (Vector2.Distance(playerPosition, GetPosition()) < COLLISION_THRESH) 
        {
            level.LoadRoom(targetX, targetY);
        }
    }
}
