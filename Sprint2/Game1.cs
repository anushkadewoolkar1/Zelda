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
        private IController _mouseController;

        // Textures
        private Texture2D _nonMovingNonAnimatedTexture;
        private Texture2D _nonMovingAnimatedTexture;
        private Texture2D _movingNonAnimatedTexture;
        private Texture2D _movingAnimatedTexture;

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
            _nonMovingNonAnimatedTexture = Content.Load<Texture2D>("LuigiStill");
            _nonMovingAnimatedTexture = Content.Load<Texture2D>("LuigiRunningSheet");
            _movingNonAnimatedTexture = Content.Load<Texture2D>("LuigiDead");
            _movingAnimatedTexture = Content.Load<Texture2D>("LuigiRunMoveSheet");

            //Load sprite font
            _spriteFont = Content.Load<SpriteFont>("DefaultFont");

            // Non-moving, non-animated 
            var sprite1 = new NonMovingNonAnimatedSprite(
                texture: _nonMovingNonAnimatedTexture,
                position: new Vector2(400, 200)
            );

            // Non-moving, animated 
            // define the animation frames for your sprite sheet:
            var sprite1Frames = new List<Rectangle>
            {
                new Rectangle(241,  0, 16, 16),
                new Rectangle(272,  0, 16, 16),
                new Rectangle(300,  0, 16, 16),
                
            };
            var sprite2 = new NonMovingAnimatedSprite(
                texture: _nonMovingAnimatedTexture,
                position: new Vector2(400, 200),
                frames: sprite1Frames,
                frameInterval: 0.18 
            );

            // Moving, non-animated
            var sprite3 = new MovingNonAnimatedSprite(
                texture: _movingNonAnimatedTexture,
                startPosition: new Vector2(400, 200),
                velocity: new Vector2(0, 150) // moves down 1 pixel per update 
            );

            // Moving, animated
            var sprite2Frames = new List<Rectangle>
            {        
                new Rectangle(241,  0, 16, 16),
                new Rectangle(272,  0, 16, 16),
                new Rectangle(300,  0, 16, 16),
                
            };
            var sprite4 = new MovingAnimatedSprite(
                texture: _movingAnimatedTexture,
                startPosition: new Vector2(400, 200),
                velocity: new Vector2(2, 0),  // moves right
                frames: sprite2Frames,
                frameInterval: 0.18
            );

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
            var setSpriteNonMovingNonAnim = new SetSpriteCommand(this, sprite1);
            var setSpriteNonMovingAnim = new SetSpriteCommand(this, sprite2);
            var setSpriteMovingNonAnim = new SetSpriteCommand(this, sprite3);
            var setSpriteMovingAnim = new SetSpriteCommand(this, sprite4);

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

            // Set up MouseController
            _mouseController = new MouseController(
                nonMovingNonAnimatedCommand: setSpriteNonMovingNonAnim,
                nonMovingAnimatedCommand: setSpriteNonMovingAnim,
                movingNonAnimatedCommand: setSpriteMovingNonAnim,
                movingAnimatedCommand: setSpriteMovingAnim,
                quitCommand: quitCommand,
                windowWidth: _graphics.PreferredBackBufferWidth,
                windowHeight: _graphics.PreferredBackBufferHeight
            );

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardController.Update();
            _mouseController.Update();

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
