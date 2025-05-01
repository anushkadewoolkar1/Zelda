using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MainGame.CollisionHandling;
using MainGame.Display;
using ZeldaGame.Zelda.CollisionMap;
using System;

public class Block : IBlock, IGameObject
{
    // Removed Keyboard property to refactor input behavior to be the responsibility of Keyboard/Command classes (PP):
    private KeyboardState _previousState;
    private Vector2 pixelPosition;
    protected Texture2D[] textures;
    private int currentTextureIndex = 0;
    protected TileMap tileMap = TileMap.GetInstance();

    public LevelManager MyLevel { get; set; }
    public Vector2 LoadRoomSet { get; set; }


    private const float scaleFactor = 0.3f;
    private const int BOUNDING_BOX_DIM_X = 28;
    private const int BOUNDING_BOX_DIM_Y = 23;
    public Boolean needKey { get; set; }

    public Block(Vector2 startPosition, Texture2D[] blockTextures, LevelManager level)
    {
        //tilePosition = startPosition;
        pixelPosition = startPosition;
        textures = blockTextures;
        System.Diagnostics.Debug.WriteLine("Loaded Block Textures:");
        for (int i = 0; i < textures.Length; i++)
        {
            System.Diagnostics.Debug.WriteLine($"Index {i}: {textures[i].Name}");
        }
        
        LoadRoomSet = new Vector2(-1, -1);
        MyLevel = level;
        needKey = false;
    }

    public Vector2 GetPosition() => new Vector2(pixelPosition.X, pixelPosition.Y - 3);
    public bool IsSolid() => true;
    public bool IsPushable() => false;
    public bool Push(Vector2 direction) => false;

    public void Update()
    {
        
    }

    // Temporary method (as for now) to reflect Sprint 2 Functionality (PP):
    public void ShiftByXPos(int x)
    {
        currentTextureIndex = (currentTextureIndex + x + textures.Length) % textures.Length;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
    }

    // IGameObject Implementatio (KD)
    public Rectangle BoundingBox
    {
        get
        {
            if (textures.Length > 0)
            {
                return new Rectangle((int)pixelPosition.X, (int)pixelPosition.Y - 3,  BOUNDING_BOX_DIM_X, BOUNDING_BOX_DIM_Y);
            }
            else
            {
                // return empty if no texture exists
                return Rectangle.Empty;
            }
        }
    }

    public void Destroy()
    {

    }

    // No velocity for blocks
    public Vector2 Velocity => Vector2.Zero;


    public void LoadRoom()
    {

        if (LoadRoomSet.X == -1 && LoadRoomSet.Y == -1)
        {
            return;
        }
        MyLevel.LoadRoom((int)LoadRoomSet.X, (int)LoadRoomSet.Y);
    }

    public void NeedKey()
    {
        needKey = true;
    }
}