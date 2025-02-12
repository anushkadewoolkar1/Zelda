using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Block : IBlock
{
    private Vector2 position;
    private Texture2D[] textures;

    private KeyboardState _previousState;
    private int currentTextureIndex = 0;

    public Block(Vector2 startPosition, Texture2D[] blockTextures)
    {
        position = startPosition;
        textures = blockTextures;
    }

    public Vector2 GetPosition() => position;
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

    public void Draw(SpriteBatch spriteBatch)
    {
        if (textures.Length > 0)
        {
            float scaleFactor = 0.3f;
            spriteBatch.Draw(textures[currentTextureIndex], position, null, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
        }
    }
}