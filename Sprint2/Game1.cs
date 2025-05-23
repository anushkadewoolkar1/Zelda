﻿using Microsoft.Xna.Framework;
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
using MainGame.Shader;

namespace MainGame
{
    public partial class Game1 : Game
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

        private GameAudio _audio;

        private TileMap _tileMap;
        private CollisionManager _collisionManager = new();

        private StartMenu _startMenu;
        private DeathScreen _deathScreen;
        private WinScreen _winScreen;
        private SettingsMenu _settings;
        private InventoryHUD _inventory;
        private HUD _hud;
        private List<BaseMenu> menus = new List<BaseMenu>();

        private List<IGameObject> _gameObjects = new();
        private List<Enemy> _enemySprites = new();

        private Block _block;

        private Item _item, _item2;
        private ItemSprite _itemSprite;

        private ShaderManager _shaderManager;

        public Link linkSprite { get; set; }
        public IDisplay currDisplay;
        public LevelManager levelMap;
        public bool isInventoryOpen { get; set; }
        public bool isSettingsOpen { get; set; }

        private CheatCodeManager _cheatCodeManager;
        public int GamesPlayed { get; set; } = 0;
        public int GamesWon { get; set; } = 0;
        public int GamesLost { get; set; } = 0;

        public bool showStats = false;

        // For red bounding box drawing
        private Texture2D pixel;

        Texture2D _baseTexture;

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

        protected override void Update(GameTime gameTime)
        {
            
            _mouseController.Update(levelMap);

            _gamePadConnected = GamePad.GetState(0).IsConnected;
            if (_gamePadConnected) _gamePadController.Update();
            else _keyboardController.Update();

            levelMap.Update(gameTime);
            levelMap.GameState(GameState);

            foreach (BaseMenu menu in menus)
            {
                menu.UpdateGameState(GameState);
            }
            _settings.UpdateGameState(GameState);
            _hud.UpdateGameState(GameState);

            _cheatCodeManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // render the zelda game by itself to _renderTarget
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            levelMap.Draw(_spriteBatch);
            if (isInventoryOpen) _inventory.Draw(_spriteBatch);
            if (isSettingsOpen) _settings.Draw(_spriteBatch);
            if (showStats)
            {
                string statsText = $"Games Played: {GamesPlayed}\nGames Won: {GamesWon}\nGames Lost: {GamesLost}";
                _spriteBatch.DrawString(_spriteFont, statsText, new Vector2(10, 10), Color.White);
            }
            
            foreach (BaseMenu menu in menus)
            {
                menu.Draw(_spriteBatch);
            }
            _hud.Draw(_spriteBatch);
            _spriteBatch.End();

            // switch the render to window and clear it
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            _shaderManager.Update(gameTime);
            _shaderManager.Draw(_spriteBatch, _renderTarget);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public ISprite CurrentSprite
        {
            get => _currentSprite;
            set => _currentSprite = value;
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
            GamesPlayed+=1;
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