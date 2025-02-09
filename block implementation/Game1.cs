using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace testmonogame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D[] blockTextures;
    private Block myBlock;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        blockTextures = new Texture2D[]
        {
            Content.Load<Texture2D>("block1"), 
            Content.Load<Texture2D>("block2"),
            Content.Load<Texture2D>("block3"),
            Content.Load<Texture2D>("block4"), 
            Content.Load<Texture2D>("block5"),
            Content.Load<Texture2D>("block6"),
            Content.Load<Texture2D>("block7"), 
            Content.Load<Texture2D>("block8"),
            Content.Load<Texture2D>("block9"),
            Content.Load<Texture2D>("block10")
        };

        myBlock = new Block(new Vector2(100, 100), blockTextures);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            myBlock.Update();  

            base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            myBlock.Draw(_spriteBatch);  
            _spriteBatch.End();

            base.Draw(gameTime);
    }
}
