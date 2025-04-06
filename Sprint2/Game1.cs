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
using Sprint0.Display;
using Sprint0.CollisionHandling;
using System.Runtime.Intrinsics.X86;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using Zelda.Inventory;


namespace Sprint0
{
    public class Game1 : Game
    {
        Point gameResolution = new Point(512, 480);

        RenderTarget2D renderTarget;
        Rectangle renderTargetDestination;

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
        private PlayerController _keyboardController;
        private PlayerController _gamePadController;
        private bool gamePadConnected = false;
        private DebugController _mouseController;

        // Textures

        // Sprite font
        private SpriteFont _spriteFont;

        private Item item;
        private Item item2;
        private ItemSprite itemSprite;

        public bool isInventoryOpen { get; set; }
        private Inventory _inventory;
        public bool isSettingsOpen { get; set; }
        private SettingsMenu _settings;

        Link linkSprite;

        List<Enemy> enemySprites = new List<Enemy>();
        Enemy enemySprite;

        private TileMap _tileMap;
        private Block _invisibleBlock;
        private LoadRoomBlock _loadRoomBlock;

        public IDisplay currDisplay;
        public Level levelMap;


        private StartMenu _startMenu;
        private DeathScreen _deathScreen;
        private WinScreen _winScreen;

        public GameState GameState { get; set; }

        // Collision
        private List<IGameObject> gameObjects = new List<IGameObject>();
        private CollisionManager collisionManager = new CollisionManager();


        //block
        private Block _block;

        // sound effects
        private GameAudio _audio;
        private Boolean _audio_playing = false;

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
            _graphics.PreferredBackBufferWidth = gameResolution.X;
            _graphics.PreferredBackBufferHeight = gameResolution.Y;
            _graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(GraphicsDevice, gameResolution.X, gameResolution.Y);
            renderTargetDestination = GetRenderTargetDestination(gameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

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
                Content.Load<Texture2D>("block10"),
                Content.Load<Texture2D>("transparent_block")
            };

            restart = false;

            // song = Content.Load<Song>(@"Sound Effects\Underworld BGM");
            _audio = GameAudio.Instance;
            _audio.LoadAllAudio(Content);


            Texture2D[] invisibleBlockTextures = { Content.Load<Texture2D>("transparent_block") };
            _block = new Block(new Vector2(15, 1), blockTextures, levelMap);
            //_block = new Block(new Vector2(15, 1), blockTextures);
            //_invisibleBlock = new InvisibleBlock(new Vector2(10, 5), invisibleBlockTextures, levelMap);
            _loadRoomBlock = new LoadRoomBlock(new Vector2(100, 100), blockTextures, levelMap, 1, 2);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadDynamicObjects();

        }

        protected override void Update(GameTime gameTime)
        {

            // Update Debug inputs:
            _mouseController.Update(levelMap);

            if ((_gamePadController == null) && (GamePad.GetState(0).IsConnected)) gamePadConnected = true;
            if ((_gamePadController != null) && !(GamePad.GetState(0).IsConnected)) gamePadConnected = false;

            // Only one player command per frame: If GamePad is connected, use it for input. Otherwise, use keyboard:
            if (!gamePadConnected)
            {
                _keyboardController.Update();
            } else
            {
                _gamePadController.Update();
            }

            levelMap.Update(gameTime);

            levelMap.GameState(GameState);
            _startMenu.UpdateGameState(GameState);
            _deathScreen.UpdateGameState(GameState);
            _winScreen.UpdateGameState(GameState);
            _settings.UpdateGameState(GameState);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(renderTarget, renderTargetDestination, Color.White);

            levelMap.Draw(_spriteBatch);
            if (isInventoryOpen)
            {
                _inventory.Draw(_spriteBatch);
            }
            if (isSettingsOpen)
            {
                _settings.Draw(_spriteBatch);
            }

            _startMenu.Draw(_spriteBatch);
            _deathScreen.Draw(_spriteBatch);
            _winScreen.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public ISprite CurrentSprite
        {
            get => _currentSprite;
            set => _currentSprite = value;
        }

        public void LoadDynamicObjects()
        {
            // Clear objects
            gameObjects.Clear();


            ProjectileSpriteFactory.Instance.LoadProjectileTextures(Content);

            ItemSpriteFactory.Instance.ItemTextures(Content);
            item = new Item();
            item = item.CreateItem(ItemType.Arrow, 11, 6);
            itemSprite = item.GetItemSprite();

            LinkSpriteFactory.Instance.LoadLinkTextures(Content);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            levelMap = new Level(Content, gameObjects);
            levelMap.LoadRoom(2, 5); //Could be key to indicate checkpoints after death? (PP)

            //Load sprite font
            _spriteFont = Content.Load<SpriteFont>("DefaultFont");

            linkSprite = new Link(gameObjects);

            _startMenu = new StartMenu(Content);
            _deathScreen = new DeathScreen(Content);
            _winScreen = new WinScreen(Content);

            ICommand quitCommand = new QuitCommand(this);
            ICommand resetCommand = new ResetCommand(this);
            ICommand setIdleCommand = new ChangeLinkState(linkSprite, new LinkIdleState((linkSprite), linkSprite.currentDirection));
            ICommand setAttackCommand = new ChangeLinkState(linkSprite, new LinkAttackingState(linkSprite, linkSprite.currentDirection, SwordType.WoodenSword));
            ICommand setWalkUpCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Up));
            ICommand setWalkLeftCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Left));
            ICommand setWalkRightCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Right));
            ICommand setWalkDownCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Down));
            ICommand useItemArrow = new LinkUseItem(linkSprite, ItemType.Arrow);
            ICommand leaveStartMenu = new LeaveStartMenu(this, _audio);
            ICommand muteBGM = new MuteMusic(_audio);
            ICommand unmuteBGM = new UnmuteMusic(_audio);
            ICommand openInventory = new OpenInventory(this);
            ICommand openSettings = new OpenCloseSettings(this);
            ICommand toggleFullScreen = new ToggleFullScreen(this);
            ICommand lowerVolume = new MasterVolumeDown(_audio);
            ICommand raiseVolume = new MasterVolumeUp(_audio);

            Dictionary<UserInputs, ICommand> levelCommandMap = new Dictionary<UserInputs, ICommand> 
            {
                { UserInputs.NewGame, leaveStartMenu },

                { UserInputs.None, setIdleCommand },

                { UserInputs.AttackMelee, setAttackCommand },

                { UserInputs.UseItem, useItemArrow },

                { UserInputs.MoveUp, setWalkUpCommand },

                { UserInputs.MoveDown, setWalkDownCommand },

                { UserInputs.MoveLeft, setWalkLeftCommand },

                { UserInputs.MoveRight, setWalkRightCommand },

                { UserInputs.ToggleFullscreen, toggleFullScreen },

                { UserInputs.RaiseVolume, raiseVolume },

                { UserInputs.LowerVolume, lowerVolume },

                { UserInputs.ToggleMute, muteBGM },

                { UserInputs.ToggleOptions, openSettings },

                { UserInputs.ToggleInventory, openInventory }

            };

            Dictionary<UserInputs, ICommand> menuCommandMap = new Dictionary<UserInputs, ICommand>
            {
                { UserInputs.None, setIdleCommand },

                { UserInputs.ToggleOptions, openSettings },

                { UserInputs.ToggleInventory, openInventory },

                //{ UserInputs.MoveUp, switchCursorUp },

                //{ UserInputs.MoveDown, switchCursorDown },

                //{ UserInputs.MoveLeft, switchCursorLeft },

                //{ UserInputs.MoveRight, switchCursorRight },

                //{ UserInputs.MenuAction, menuInput },

                //{ UserInputs.ToggleOptions, setDisplay(IDisplay display) }

            };

            _keyboardController = new KeyboardController(levelCommandMap, menuCommandMap);
            _gamePadController = new GamePadController(levelCommandMap, menuCommandMap);

            _mouseController = new MouseController(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);


            item2 = new Item();
            item2 = item2.CreateItem(ItemType.Arrow, 1, 6);
            enemySprites.ForEach(enemySprite => gameObjects.Add(enemySprite));
            gameObjects.Add(item);
            gameObjects.Add(linkSprite);
            gameObjects.Add(item2);

            _inventory = new Inventory(Content, GraphicsDevice, linkSprite);
            _settings = new SettingsMenu(Content, GraphicsDevice);

            levelMap.AddLink(linkSprite);
            levelMap.CollisionManager(collisionManager);
            levelMap.Game(this);
            //gameObjects.Add(_block);
            //gameObjects.Add(_loadRoomBlock);

            GameState = Zelda.Enums.GameState.StartMenu;
        }

        private Rectangle GetRenderTargetDestination(Point resolution, int preferredBackBufferWidth, int preferredBackBufferHeight)
        {
            float resolutionRatio = (float)resolution.X / resolution.Y;
            float screenRatio;
            Point bounds = new Point(preferredBackBufferWidth, preferredBackBufferHeight);
            screenRatio = (float)bounds.X / bounds.Y;
            float scale;
            Rectangle rectangle = new Rectangle();

            //Compares the aspect ratio of the resolution and the screen's ratio and sets scale appropriately
            if (resolutionRatio < screenRatio)
                scale = (float)bounds.Y / resolution.Y;
            else if (resolutionRatio > screenRatio)
                scale = (float)bounds.X / resolution.X;
            else
            {
                rectangle.Size = bounds;
                return rectangle;
            }
            rectangle.Width = (int)(resolution.X * scale);
            rectangle.Height = (int)(resolution.Y * scale);

            //Returns rectangle that either matches screen's X coordinate or Y coordinate
            return rectangle;
        }

        public void ToggleFullScreen()
        {
            if (!_graphics.IsFullScreen)
            {
                _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                _graphics.PreferredBackBufferWidth = gameResolution.X;
                _graphics.PreferredBackBufferHeight = gameResolution.Y;
            }
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
            _graphics.ApplyChanges();

            //Adjusts rectangle to render
            renderTargetDestination = GetRenderTargetDestination(gameResolution, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        public void SwitchDisplay(GameState gameState)
        {
            // Method is not frequently executed, so we can get away with using a switch on all possible states:
            switch (gameState)
            {
                case GameState.Playing:
                    currDisplay = levelMap;
                    break;
                case GameState.StartMenu:
                    //currDisplay = new StartMenu();
                    break;
                case GameState.MainMenu:
                    //currDisplay = new OptionsMenu();
                    break;
                case GameState.Paused:
                    //EnterPausedMode();
                    break;
                case GameState.Death:
                    //If lives is not 0 then:
                    //  SwitchDisplay(GameState.NewGame); New game instance with current data in there
                    //else:
                    //  SwichDisplay(GameOver);
                    break;
                case GameState.NewGame:
                    StartNewLevelInstance();
                    break;
                case GameState.GameOver:
                    //Clear out data relating to the specific game instance, can encapsulate in Game1 method
                    //currDisplay = new GameOverMenu();
                    break;
                case GameState.Credits:
                    //currDisplay = new CreditsMenu();
                    break;
                default:
                    break;
            }
        }

        public void StartNewLevelInstance()
        {
            SwitchDisplay(GameState.NewGame);
            Thread.Sleep(3000); //Stay on pre-level menu for 3 seconds
            LoadDynamicObjects();
            SwitchDisplay(GameState.Playing);
        }

        public void EnterPausedMode()
        {
            //UI elements to overlay on current level (Controls, translucent black filter, etc.)

            while (!Keyboard.GetState().IsKeyDown(Keys.Enter)) { }

            //Revert UI elements
        }
    }
}
