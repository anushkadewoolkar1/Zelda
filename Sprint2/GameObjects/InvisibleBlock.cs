using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZeldaGame.Zelda.CollisionMap;
using Sprint0.CollisionHandling;
using Sprint0.Display;
public class InvisibleBlock : Block, IGameObject

{

    private const int SOLID_BLOCK_DIM = 1;
    private const int BLOCK_SCALE = 1;
    private const int BOUNDING_BOX_DIM = 14;

    Vector2 pixelPosition;
    public InvisibleBlock(Vector2 startPosition, Texture2D[] blockTextures, Level level)
        : base(startPosition, blockTextures, level)
    {
        pixelPosition = startPosition;

        base.myLevel = level;
        base.loadRoom = new Vector2(-1, -1);

    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (textures.Length > 0)
        {
            //Vector2 pixelPosition = tileMap.GetTileCenter(GetPosition());
            //float scaleFactor = 0.3f;

            //Use the transparent block texture 
            //spriteBatch.Draw(textures[0], pixelPosition, null, Color.White, 0f, Vector2.Zero, scaleFactor, SpriteEffects.None, 0f);
            Texture2D solidColor = new Texture2D(spriteBatch.GraphicsDevice, SOLID_BLOCK_DIM, SOLID_BLOCK_DIM);
            solidColor.SetData(new[] { Color.White }); // Make it a solid white


            // Draw it at the correct position, scaling it
            spriteBatch.Draw(solidColor, pixelPosition, null, Color.Red, 0f, Vector2.Zero, BLOCK_SCALE, SpriteEffects.None, 0f);
        }
    }
    public new Rectangle BoundingBox
    {
        get
        {
            return new Rectangle((int)pixelPosition.X, (int)pixelPosition.Y, BOUNDING_BOX_DIM, BOUNDING_BOX_DIM);
        }
    }

    public new Vector2 Velocity
    {
        get
        {
            return new Vector2(0, 0);
        }
    }

    public new void Destroy()
    {
    }
}
