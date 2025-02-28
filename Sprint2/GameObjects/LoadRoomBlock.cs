using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Zelda.CollisionMap;
using Sprint0.ILevel;

public class LoadRoomBlock : Block
{
    private Level level;
    private int targetX, targetY;

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
        if (Vector2.Distance(playerPosition, GetPosition()) < 10) 
        {
            level.LoadRoom(targetX, targetY);
        }
    }
}
