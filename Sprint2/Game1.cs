using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Sprites;
using Sprint0.Controllers;
using Sprint0.Commands;
using System.Collections.Generic;
using SpriteFactory;
using Zelda.Enums;
using Sprint0.States;


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

        private ItemSprite itemSprite;

        Link linkSprite;

        //block
        private Block _block;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D[] blockTextures = new Texture2D[]
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

            // Create the block at position (200, 200)
            _block = new Block(new Vector2(200, 200), blockTextures);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ItemSpriteFactory.Instance.ItemTextures(Content);  
            itemSprite = ItemSpriteFactory.Instance.FetchItemSprite("ZeldaSpriteArrow");

            LinkSpriteFactory.Instance.LoadLinkTextures(Content);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            //enemySprite = new Enemy();

            

            // Load textures

            //Load sprite font
            _spriteFont = Content.Load<SpriteFont>("DefaultFont");

            /* Text sprite /*
            _textSprite = new TextSprite(
                font: _spriteFont,
                text: "Credits\nProgram Made By: Kyle Dietrich\nSprites from: https://www.mariouniverse.com/sprites-nes-smb/",
                position: new Vector2(200, 300),
                color: Color.Black
            );
            */

            linkSprite = new Link();

            // Set the initial sprite
           // _currentSprite = sprite1; // Non-moving, non-animated

            // Create Commands 
            var quitCommand = new QuitCommand(this);
            var attackLeftCommand = new ChangeLinkState(linkSprite, new LinkAttackingState(linkSprite, Zelda.Enums.Direction.Left));
            var attackRightCommand = new ChangeLinkState(linkSprite, new LinkAttackingState(linkSprite, Zelda.Enums.Direction.Right));
            var moveUpCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Up));
            var moveDownCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Down));
            var moveLeftCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Left));
            var moveRightCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Right));
            // var enemyCycleLeftCmd = new CycleEnemy(_currentEnemy, Directions.Left, _currentEnemy.stateMachine);
            // var enemyCycleRightCmd = new CycleEnemy(_currentEnemy, Directions.Right, _currentEnemy.stateMachine);
            // Set up KeyboardController with dictionary
            var keyboardCommandMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, quitCommand },
                
                { Keys.Z, attackLeftCommand },

                { Keys.N, attackRightCommand },
                
                { Keys.W, moveUpCommand },

                { Keys.Up, moveUpCommand },

                { Keys.A, moveLeftCommand },

                { Keys.Left, moveLeftCommand },

                { Keys.S, moveDownCommand },

                { Keys.Down, moveDownCommand },

                { Keys.D, moveRightCommand },

                { Keys.Right, moveRightCommand },

            };
            _keyboardController = new KeyboardController(keyboardCommandMap);
            /*
             * Commands to still be implemented are:
             * -Reset (needs further discussion on how to implement)
             * -Block/Item Cycle (Probably need an Item/Block StateMachine to access enum values paired to their respective sprites)
             * -Use Items (Implemented using the UseItem method from Link, once Link is able to be assigned items)
             * -Damage (Uses ChangeLinkState command, need for LinkDamagedState class to be completed before input is implemented)
             */
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardController.Update();

            //_currentSprite.Update(gameTime);

            _block.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            linkSprite.Draw(_spriteBatch);

            //enemySprite.DrawCurrentSprite(_spriteBatch);

            //_textSprite.Draw(_spriteBatch);

            _block.Draw(_spriteBatch);

            itemSprite.Draw(_spriteBatch, new Vector2(100, 100));

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
