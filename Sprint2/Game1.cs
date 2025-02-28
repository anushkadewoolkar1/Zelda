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
using System;
using System.Net.Http.Headers;
using ZeldaGame.Zelda.CollisionMap;
using Sprint0.ILevel;


namespace Sprint0
{
    public class Game1 : Game
    {
        public bool restart;

        private Texture2D _backgroundTexture;
        private Rectangle _sourceRectangle;

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

        private Item item;
        private ItemSprite itemSprite;

        Link linkSprite;

        Enemy enemySprite;

        private TileMap _tileMap;

        Level levelMap;




        //block
        private Block _block;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsFixedTimeStep = true;
            // fps
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //_graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 1440;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            TileMap.Initialize(256, 240);
            _tileMap = TileMap.GetInstance();

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

            restart = false;

            // Create the block at position (200, 200)
            //_block = new Block(new Vector2(200, 200), blockTextures);
            //_block = new Block(new Vector2(15, 1), blockTextures);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ProjectileSpriteFactory.Instance.LoadProjectileTextures(Content);

            
            item = new Item();
            ItemSpriteFactory.Instance.ItemTextures(Content);
            item = item.CreateItem(ItemType.Arrow, 1, 1);
            itemSprite = item.GetItemSprite(); 

            LinkSpriteFactory.Instance.LoadLinkTextures(Content);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemySprite = new Enemy();

            levelMap = new Level(Content);
            levelMap.LoadRoom(2, 5);

            //Load sprite font
            _spriteFont = Content.Load<SpriteFont>("DefaultFont");

            linkSprite = new Link();

            // Set up KeyboardController with dictionary
            var keyboardCommandMap = new Dictionary<Keys, ICommand>
            {
                //'Q' -> Program Quit:
                { Keys.Q, new ExitCommand(this, false) },

                //'R' -> Program Reset:
                { Keys.R, new ExitCommand(this, true) },

                //No_Key -> Set Player state to default:
                { Keys.None, new ChangeLinkState(linkSprite, new LinkIdleState((linkSprite),linkSprite.currentDirection)) },

                //'Z' -> Player Attack:
                { Keys.Z, new ChangeLinkState(linkSprite, new LinkAttackingState(linkSprite, linkSprite.currentDirection, SwordType.WoodenSword))},

                //'N' -> Player Attack:
                { Keys.N, new ChangeLinkState(linkSprite, new LinkAttackingState(linkSprite, linkSprite.currentDirection, SwordType.WoodenSword)) },

                //'W' -> Player Walk Up:
                { Keys.W, new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Up)) },

                //'Up Arrow' -> Player Walk Up:
                { Keys.Up, new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Up)) },

                //'A' -> Player Walk Left:
                { Keys.A, new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Left)) },

                //'Left Arrow' -> Player Walk Left:
                { Keys.Left, new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Left)) },

                //'S' -> Player Walk Down:
                { Keys.S, new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Down)) },

                //'Down Arrow' -> Player Walk Down:
                { Keys.Down, new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Down)) },

                //'D' -> Player Walk Right:
                { Keys.D, new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Right)) },

                //'Right Arrow' -> Player Walk Right:
                { Keys.Right, new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Right)) },

                //'O' -> Cycle Enemy Left:
                { Keys.O, new CycleEnemy(enemySprite, Direction.Left) },

                //'P' -> Cycle Enemy Right:
                { Keys.P, new CycleEnemy(enemySprite, Direction.Right) },

                //'I' -> Cycle Item Left:
                { Keys.I, new CycleItem(itemSprite, Direction.Left) },

                //'U' -> Cycle Item Right:
                { Keys.U, new CycleItem(itemSprite, Direction.Right) },

                //'E' -> Damage Player:
                { Keys.E, new LinkDamaged(linkSprite) },

                //'T' -> Cycle Block Left:
                { Keys.T, new CycleBlock(_block, Direction.Left) },

                //'Y' -> Cycle Block Right:
                { Keys.Y, new CycleBlock(_block, Direction.Right) },

                //'D1' -> Player Use Arrow Item:
                { Keys.D1, new LinkUseItem(linkSprite, new ItemSprite("ZeldaSpriteArrow", 300, 100)) },

                //'D2' -> Player Use Boomerang Item:
                { Keys.D2, new LinkUseItem(linkSprite, new ItemSprite("ZeldaSpriteBoomerang", 300, 100)) },
                //{ Keys.D2, new LinkUseItem(linkSprite, ItemSpriteFactory.Instance.FetchItemSprite("ZeldaSpriteBoomerang")) },

                //'D3' -> Player Use Bomb Item:
                { Keys.D3, new LinkUseItem(linkSprite, new ItemSprite("ZeldaSpriteBomb", 300, 100)) },
                //{ Keys.D3, new LinkUseItem(linkSprite, ItemSpriteFactory.Instance.FetchItemSprite("ZeldaSpriteBomb")) }

            };

            _keyboardController = new KeyboardController(keyboardCommandMap);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardController.Update();

            //_currentSprite.Update(gameTime);

            levelMap.Update(gameTime);

            enemySprite.Update(gameTime);

            item.Update(gameTime);

            linkSprite.Update(gameTime);

            //_block.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            levelMap.Draw(_spriteBatch);

            linkSprite.Draw(_spriteBatch);

            enemySprite.DrawCurrentSprite(_spriteBatch);

            //_block.Draw(_spriteBatch);

            item.Draw(_spriteBatch);

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
