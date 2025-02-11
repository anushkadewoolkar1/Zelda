using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Sprites;
using Sprint0.Controllers;
using Sprint0.Commands;
using System.Collections.Generic;

namespace Sprint0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Current sprite
        private ISprite _currentSprite;

        // text sprite
        private ISprite _textSprite;

        // Controllers
        private IController _keyboardController;

        // Textures

        // Sprite font
        private SpriteFont _spriteFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 4000;
            _graphics.PreferredBackBufferHeight = 2000;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures

            //Load sprite font
            _spriteFont = Content.Load<SpriteFont>("DefaultFont");

            // Text sprite 
            _textSprite = new TextSprite(
                font: _spriteFont,
                text: "Credits\nProgram Made By: Kyle Dietrich\nSprites from: https://www.mariouniverse.com/sprites-nes-smb/",
                position: new Vector2(200, 300),
                color: Color.Black
            );

            // Set the initial sprite
            _currentSprite = sprite1; // Non-moving, non-animated

            // Create Commands 
            var quitCommand = new QuitCommand(this);

            // Set up KeyboardController with dictionary
            var keyboardCommandMap = new Dictionary<Keys, ICommand>
            {
                { Keys.D0, quitCommand },
                
                { Keys.D1, setSpriteNonMovingNonAnim },
                
                { Keys.D2, setSpriteNonMovingAnim },
                
                { Keys.D3, setSpriteMovingNonAnim },
                
                { Keys.D4, setSpriteMovingAnim },
                
            };
            _keyboardController = new KeyboardController(keyboardCommandMap);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardController.Update();

            _currentSprite.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentSprite.Draw(_spriteBatch);

            _textSprite.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public ISprite CurrentSprite 
        {
            get => _currentSprite;
            set => _currentSprite = value;
        }
    }
}
