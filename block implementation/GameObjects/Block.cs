using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Block : IBlock
{
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
        KeyboardState state = Keyboard.GetState();

        // Change to previous texture when T is input
        if (state.IsKeyDown(Keys.T))
        {
            currentTextureIndex = (currentTextureIndex - 1 + textures.Length) % textures.Length;
        }

        // Change to next texture when Y is input
        if (state.IsKeyDown(Keys.Y))
        {
            currentTextureIndex = (currentTextureIndex + 1) % textures.Length;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (textures.Length > 0)
        {
            spriteBatch.Draw(textures[currentTextureIndex], position, Color.White);
        }
    }
}
