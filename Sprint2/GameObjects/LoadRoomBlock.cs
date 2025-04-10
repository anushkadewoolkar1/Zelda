using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Zelda.CollisionMap;
using MainGame.Display;

public class LoadRoomBlock : Block
{
    private LevelManager level;
    private int targetX, targetY;

    private const int COLLISION_THRESH = 10;

    public LoadRoomBlock(Vector2 startPosition, Texture2D[] blockTextures, LevelManager level, int x, int y)
        : base(startPosition, blockTextures, level)
    {
        this.level = level;
        targetX = x;
        targetY = y;

        base.myLevel = level;
        base.loadRoom = new Vector2(x, y);
    }

    public virtual bool IsSolid() => true;
}
