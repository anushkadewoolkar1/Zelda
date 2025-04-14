using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MainGame.Sprites;
using MainGame.Controllers;
using MainGame.Commands;
using System.Collections.Generic;
using SpriteFactory;
using Zelda.Enums;
using MainGame.States;
using System;
using System.Net.Http.Headers;
using ZeldaGame.Zelda.CollisionMap;
using MainGame.Display;
using MainGame.CollisionHandling;
using System.Runtime.Intrinsics.X86;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using Zelda.Inventory;
using ZeldaGame.HUD;

namespace MainGame
{
    public class Game1 : Game
    {
        private const int TargetFPS = 30;
        private static readonly Point DefaultResolution = new(512, 480);

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D _renderTarget;
        private Rectangle _renderTargetDestination;

        public bool restart;
        public GameState GameState { get; set; }

        private ISprite _currentSprite;
        private SpriteFont _spriteFont;

        private PlayerController _keyboardController;
        private PlayerController _gamePadController;
        private DebugController _mouseController;

        private bool _gamePadConnected;
        private bool _audioPlaying;

        private GameAudio _audio;

        private TileMap _tileMap;
        private CollisionManager _collisionManager = new();

        private StartMenu _startMenu;
        private DeathScreen _deathScreen;
        private WinScreen _winScreen;
        private SettingsMenu _settings;
        private Inventory _inventory;
        private HUD _hud;

        private List<IGameObject> _gameObjects = new();
        private List<Enemy> _enemySprites = new();

        private Block _block;
        private Block _invisibleBlock;
        private LoadRoomBlock _loadRoomBlock;

        private Item _item, _item2;
        private ItemSprite _itemSprite;

        public Link linkSprite { get; set; }
        public IDisplay currDisplay;
        public LevelManager levelMap;
        public bool isInventoryOpen { get; set; }
        public bool isSettingsOpen { get; set; }

        private CheatCodeManager _cheatCodeManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = DefaultResolution.X,
                PreferredBackBufferHeight = DefaultResolution.Y
            };

            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / TargetFPS);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.ApplyChanges();
            _renderTarget = new RenderTarget2D(GraphicsDevice, DefaultResolution.X, DefaultResolution.Y);
            _renderTargetDestination = GetRenderTargetDestination(DefaultResolution);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            TileMap.Initialize(256, 240);
            _tileMap = TileMap.GetInstance();

            _audio = GameAudio.Instance;
            _audio.LoadAllAudio(Content);

            var blockTextures = LoadBlockTextures();
            _block = new Block(new Vector2(15, 1), blockTextures, levelMap);
            _loadRoomBlock = new LoadRoomBlock(new Vector2(100, 100), blockTextures, levelMap, 1, 2);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadDynamicObjects();

            _cheatCodeManager = new CheatCodeManager(linkSprite, levelMap);
        }

        protected override void Update(GameTime gameTime)
        {
            _mouseController.Update(levelMap);

            _gamePadConnected = GamePad.GetState(0).IsConnected;
            if (_gamePadConnected) _gamePadController.Update();
            else _keyboardController.Update();

            levelMap.Update(gameTime);
            levelMap.GameState(GameState);

            _startMenu.UpdateGameState(GameState);
            _deathScreen.UpdateGameState(GameState);
            _winScreen.UpdateGameState(GameState);
            _settings.UpdateGameState(GameState);
            _hud.UpdateGameState(GameState);

            _cheatCodeManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            _spriteBatch.Draw(_renderTarget, _renderTargetDestination, Color.White);
            levelMap.Draw(_spriteBatch);

            if (isInventoryOpen) _inventory.Draw(_spriteBatch);
            if (isSettingsOpen) _settings.Draw(_spriteBatch);

            _startMenu.Draw(_spriteBatch);
            _deathScreen.Draw(_spriteBatch);
            _winScreen.Draw(_spriteBatch);
            _hud.Draw(_spriteBatch);

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
            _gameObjects.Clear();

            ProjectileSpriteFactory.Instance.LoadProjectileTextures(Content);
            ItemSpriteFactory.Instance.ItemTextures(Content);
            LinkSpriteFactory.Instance.LoadLinkTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            _spriteFont = Content.Load<SpriteFont>("DefaultFont");
            _item = new Item().CreateItem(ItemType.Arrow, 11, 6);
            _item2 = new Item().CreateItem(ItemType.Arrow, 1, 6);
            _itemSprite = _item.GetItemSprite();

            linkSprite = new Link(_gameObjects);
            levelMap = new LevelManager(Content, _gameObjects);
            levelMap.AddLink(linkSprite);
            levelMap.LoadRoom(2, 5);

            _startMenu = new StartMenu(Content);
            _deathScreen = new DeathScreen(Content);
            _winScreen = new WinScreen(Content);
            _settings = new SettingsMenu(Content, GraphicsDevice);
            _inventory = new Inventory(Content, GraphicsDevice, linkSprite, levelMap);
            _hud = new HUD(Content, linkSprite);

            SetupControllers();

            _gameObjects.AddRange(new IGameObject[] { _item, _item2, linkSprite });
            _enemySprites.ForEach(e => _gameObjects.Add(e));

            levelMap.CollisionManager(_collisionManager);
            levelMap.Game(this);
            GameState = GameState.StartMenu;
        }

        private void SetupControllers()
        {
            var commands = new CommandFactory(this, linkSprite, _itemSprite, _block, _audio);

            _keyboardController = new KeyboardController(commands.GetLevelCommandMap(), commands.GetMenuCommandMap());
            _gamePadController = new GamePadController(commands.GetLevelCommandMap(), commands.GetMenuCommandMap());
            _mouseController = new MouseController(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        private Texture2D[] LoadBlockTextures()
        {
            var textures = new List<Texture2D>();
            for (int i = 1; i <= 10; i++)
                textures.Add(Content.Load<Texture2D>($"block{i}"));

            textures.Add(Content.Load<Texture2D>("transparent_block"));
            return textures.ToArray();
        }

        private Rectangle GetRenderTargetDestination(Point resolution)
        {
            Point bounds = new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            float screenRatio = (float)bounds.X / bounds.Y;
            float resolutionRatio = (float)resolution.X / resolution.Y;
            float scale = resolutionRatio < screenRatio ? (float)bounds.Y / resolution.Y : (float)bounds.X / resolution.X;

            return resolutionRatio == screenRatio
                ? new Rectangle(Point.Zero, bounds)
                : new Rectangle(0, 0, (int)(resolution.X * scale), (int)(resolution.Y * scale));
        }

        public void ToggleFullScreen()
        {
            _graphics.PreferredBackBufferWidth = _graphics.IsFullScreen ? DefaultResolution.X : GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = _graphics.IsFullScreen ? DefaultResolution.Y : GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
            _graphics.ApplyChanges();
            _renderTargetDestination = GetRenderTargetDestination(DefaultResolution);
        }

        public void SwitchDisplay(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Playing:
                    currDisplay = levelMap;
                    break;
                case GameState.NewGame:
                    StartNewLevelInstance();
                    break;
            }
        }

        public void StartNewLevelInstance()
        {
            SwitchDisplay(GameState.NewGame);
            Thread.Sleep(3000);
            LoadDynamicObjects();
            SwitchDisplay(GameState.Playing);
        }

        public void ResetLevel()
        {
            levelMap = new LevelManager(Content, _gameObjects);
            levelMap.AddLink(linkSprite);
            levelMap.LoadRoom(2, 5);
        }

        public void EnterPausedMode()
        {
            while (!Keyboard.GetState().IsKeyDown(Keys.Enter)) { }
        }
    }
}