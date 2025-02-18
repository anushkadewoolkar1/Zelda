using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Block : IBlock
{
    // Removed Keyboard property to refactor input behavior to be the responsibility of Keyboard/Command classes (PP):
    private Vector2 position;
    private Texture2D[] textures;
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
        /*
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
        */
    }

    // Temporary method (as for now) to reflect Sprint 2 Functionality (PP):
    public void shiftByXPos(int x)
    {
        currentTextureIndex = (currentTextureIndex + x + textures.Length) % textures.Length;
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