using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.CollisionHandling;
using Sprint0.Display;
using ZeldaGame.Zelda.CollisionMap;

public class Block : IBlock, IGameObject
{
    // Removed Keyboard property to refactor input behavior to be the responsibility of Keyboard/Command classes (PP):
    private KeyboardState _previousState;
    private Vector2 tilePosition;
    private Vector2 pixelPosition;
    protected Texture2D[] textures;
    private int currentTextureIndex = 0;
    protected TileMap tileMap = TileMap.GetInstance();

    public Level myLevel { get; set; }
    public Vector2 loadRoom { get; set; }


    private const float scaleFactor = 0.3f;
    private const int BOUNDING_BOX_DIM = 22;

    public Block(Vector2 startPosition, Texture2D[] blockTextures, Level level)
    {
        //tilePosition = startPosition;
        pixelPosition = startPosition;
        textures = blockTextures;
        System.Diagnostics.Debug.WriteLine("Loaded Block Textures:");
        for (int i = 0; i < textures.Length; i++)
        {
            System.Diagnostics.Debug.WriteLine($"Index {i}: {textures[i].Name}");
        }

        loadRoom = new Vector2(-1, -1);
        myLevel = level;
    }

    public Vector2 GetPosition() => tilePosition;
    public bool IsSolid() => true;
    public bool IsPushable() => false;
    public bool Push(Vector2 direction) => false;

    public void Update()
    {
        
        KeyboardState state = Keyboard.GetState();

        // Change to previous texture when T is input
        if (state.IsKeyDown(Keys.T) && _previousState.IsKeyUp(Keys.T))
        {
            currentTextureIndex = (currentTextureIndex - 1 + textures.Length) % textures.Length;
        }

        if (state.IsKeyDown(Keys.Y) && _previousState.IsKeyUp(Keys.Y))
        {
            currentTextureIndex = (currentTextureIndex + 1) % textures.Length;
        }

        _previousState = state;
        
    }

    // Temporary method (as for now) to reflect Sprint 2 Functionality (PP):
    public void ShiftByXPos(int x)
    {
        currentTextureIndex = (currentTextureIndex + x + textures.Length) % textures.Length;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
        if (textures.Length > 0)
        {

            //Vector2 pixelPosition = tileMap.GetTileCenter(tilePosition);            
            //spriteBatch.Draw(textures[currentTextureIndex], pixelPosition, null, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
            float scaleFactor = 0.2f;
            return;
            spriteBatch.Draw(textures[currentTextureIndex], pixelPosition, null, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);

        }
    }

    // IGameObject Implementatio (KD)
    public Rectangle BoundingBox
    {
        get
        {
            if (textures.Length > 0)
            {
                //Vector2 pixelPosition = tileMap.GetTileCenter(tilePosition);
               // int width = (int)(textures[0].Width * scaleFactor);
               // int height = (int)(textures[0].Height * scaleFactor);
               // return new Rectangle((int)pixelPosition.X, (int)pixelPosition.Y, width, height);
                return new Rectangle((int)pixelPosition.X, (int)pixelPosition.Y,  BOUNDING_BOX_DIM, BOUNDING_BOX_DIM);
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
        System.Diagnostics.Debug.WriteLine($"Trying to load Room({loadRoom.X},{loadRoom.Y})");

        if (loadRoom.X == -1 && loadRoom.Y == -1)
        {
            return;
        }
        myLevel.LoadRoom((int)loadRoom.X, (int)loadRoom.Y);
    }
}