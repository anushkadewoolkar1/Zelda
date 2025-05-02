using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MainGame.Sprites;
using MainGame.States;
using MainGame.CollisionHandling;
using MainGame.Display;
using Zelda.Enums;
using ZeldaGame.Zelda.CollisionMap;
using MainGame.Forces;
using MainGame.Visibility;
using MainGame.Shader;
using SpriteFactory;
using Zelda.Inventory;
using ZeldaGame.HUD;
using MainGame.Controllers;


//everything init related is called here.
namespace MainGame
{
    public partial class Game1 : Game
    {
        protected override void LoadContent()
        {
            _baseTexture = new Texture2D(GraphicsDevice, 1, 1);
            _baseTexture.SetData(new[] { Color.White });
            TileMap.Initialize(256, 240);
            _tileMap = TileMap.GetInstance();

            _audio = GameAudio.Instance;
            _audio.LoadAllAudio(Content);

            var blockTextures = LoadBlockTextures();
            _block = new Block(new Vector2(15, 1), blockTextures, levelMap);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadDynamicObjects();

            _cheatCodeManager = new CheatCodeManager(linkSprite, levelMap);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void LoadDynamicObjects()
        {
            _gameObjects.Clear();

            ProjectileSpriteFactory.Instance.LoadProjectileTextures(Content);
            ItemSpriteFactory.Instance.ItemTextures(Content);
            LinkSpriteFactory.Instance.LoadLinkTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            EnemyProjectileManager.Instance.LoadAllTextures(Content);

            _spriteFont = Content.Load<SpriteFont>("DefaultFont");
            _item = new Item().CreateItem(ItemType.Arrow, 11, 6);
            _item2 = new Item().CreateItem(ItemType.Arrow, 1, 6);
            _itemSprite = _item.GetItemSprite();

            linkSprite = new Link(_gameObjects);
            LinkSpriteFactory.Instance.SetLink(linkSprite);
            linkSprite._audio = _audio;

            levelMap = new LevelManager(Content, _gameObjects);
            levelMap.AddLink(linkSprite);
            levelMap.LoadRoom(2, 5);

            _startMenu = new StartMenu(Content);
            _deathScreen = new DeathScreen(Content);
            _winScreen = new WinScreen(Content);
            menus.Add(_startMenu);
            menus.Add(_deathScreen);
            menus.Add(_winScreen);

            _settings = new SettingsMenu(Content, GraphicsDevice);
            _inventory = new Inventory(Content, GraphicsDevice, linkSprite, levelMap);
            _hud = new HUD(Content, linkSprite, levelMap);

            _shaderManager = new ShaderManager(Content, GraphicsDevice);

            SetupControllers();

            _gameObjects.AddRange(new IGameObject[] { _item, _item2, linkSprite });
            _enemySprites.ForEach(e => _gameObjects.Add(e));

            levelMap.CollisionManager(_collisionManager);
            levelMap.Game(this);
            GameState = GameState.StartMenu;
            EnemyProjectileManager.Instance.SetObjects(_gameObjects);
        }

        private void SetupControllers()
        {
            var commands = new CommandFactory(this, linkSprite, _itemSprite, _block, _audio, _shaderManager);

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
    }
}
