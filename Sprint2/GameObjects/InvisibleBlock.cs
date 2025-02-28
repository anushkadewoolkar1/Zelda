using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Zelda.CollisionMap;

public class InvisibleBlock : Block
{
    public InvisibleBlock(Vector2 startPosition, Texture2D[] blockTextures)
        : base(startPosition, blockTextures)
    {
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (textures.Length > 0)
        {
            Vector2 pixelPosition = tileMap.GetTileCenter(GetPosition());
            float scaleFactor = 0.3f;

            //Use the transparent block texture 
            spriteBatch.Draw(textures[0], pixelPosition, null, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
        }
    }
}
