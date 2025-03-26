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
        private PlayerController _keyboardController;
        private PlayerController _gamePadController;
        private DebugController _mouseController;

        // Textures

        // Sprite font
        private SpriteFont _spriteFont;

        private Item item;
        private Item item2;
        private ItemSprite itemSprite;

        Link linkSprite;

        List<Enemy> enemySprites = new List<Enemy>();
        Enemy enemySprite;

        private TileMap _tileMap;
        private Block _invisibleBlock;
        private LoadRoomBlock _loadRoomBlock;

        public IDisplay currDisplay;
        public Level levelMap;

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
            _invisibleBlock = new InvisibleBlock(new Vector2(10, 5), invisibleBlockTextures, levelMap);
            _loadRoomBlock = new LoadRoomBlock(new Vector2(100, 100), blockTextures, levelMap, 1, 2);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadDynamicObjects();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update Debug inputs:
            _mouseController.Update(levelMap);

            // Only one player command per frame: If GamePad is connected, use it for input. Otherwise, use keyboard:
            if (GamePad.GetState(0).IsConnected)
            {
                _gamePadController.Update();
            } else
            {
                _keyboardController.Update();
            }

            if(!_audio_playing)
            {
                _audio.PlayBGM();
                _audio_playing = true;
            }
            if (MediaPlayer.State != MediaState.Playing)
            {
                // MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
            }

            //_currentSprite.Update(gameTime);

            levelMap.Update(gameTime);

            //enemySprites.ForEach(enemySprite => enemySprite.Update(gameTime));
            levelMap.enemiesList.ForEach(x => x.Update(gameTime));

            item.Update(gameTime);

            //linkSprite.Update(gameObjects, gameTime);

            /*
            _block.Update();
            _loadRoomBlock.CheckCollision(linkSprite.Position);
            */

            collisionManager.CheckDynamicCollisions(gameObjects, levelMap);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            levelMap.Draw(_spriteBatch);

            //linkSprite.Draw(_spriteBatch);

            // enemySprites.ForEach(enemySprite => enemySprite.DrawCurrentSprite(_spriteBatch));
            levelMap.enemiesList.ForEach(x => x.DrawCurrentSprite(_spriteBatch));

            /*
            _block.Draw(_spriteBatch);
            _invisibleBlock.Draw(_spriteBatch);
            _loadRoomBlock.Draw(_spriteBatch);
            */

            item.Draw(_spriteBatch);
            item2.Draw(_spriteBatch);

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
            levelMap.LoadRoom(2, 5);

            //Load sprite font
            _spriteFont = Content.Load<SpriteFont>("DefaultFont");

            linkSprite = new Link(gameObjects);

            ICommand quitCommand = new QuitCommand(this);
            ICommand resetCommand = new ResetCommand(this);
            ICommand setIdleCommand = new ChangeLinkState(linkSprite, new LinkIdleState((linkSprite), linkSprite.currentDirection));
            ICommand setAttackCommand = new ChangeLinkState(linkSprite, new LinkAttackingState(linkSprite, linkSprite.currentDirection, SwordType.WoodenSword));
            ICommand setWalkUpCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Up));
            ICommand setWalkLeftCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Left));
            ICommand setWalkRightCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Right));
            ICommand setWalkDownCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Down));
            ICommand damageLinkCommand = new LinkDamaged(linkSprite);
            ICommand cycleItemLeftCommand = new CycleItem(itemSprite, Direction.Left);
            ICommand cycleItemRightCommand = new CycleItem(itemSprite, Direction.Right);
            ICommand cycleBlockLeftCommand = new CycleBlock(_block, Direction.Left);
            ICommand cycleBlockRightCommand = new CycleBlock(_block, Direction.Right);
            ICommand useItemArrow = new LinkUseItem(linkSprite, ItemType.Arrow);
            ICommand useItemBmrng = new LinkUseItem(linkSprite, ItemType.Boomerang);
            ICommand useItemBomb = new LinkUseItem(linkSprite, ItemType.Bomb);


            // Set up KeyboardController with dictionary
            var keyboardCommandMap = new Dictionary<Keys, ICommand>
            {
                //'Q' -> Program Quit:
                { Keys.Q, quitCommand },

                //'R' -> Program Reset:
                { Keys.R, resetCommand },

                //No_Key -> Set Player state to default:
                { Keys.None, setIdleCommand },

                //'Z' -> Player Attack:
                { Keys.Z, setAttackCommand},

                //'N' -> Player Attack:
                { Keys.N, setAttackCommand},

                //'W' -> Player Walk Up:
                { Keys.W, setWalkUpCommand},

                //'Up Arrow' -> Player Walk Up:
                { Keys.Up, setWalkUpCommand },

                //'A' -> Player Walk Left:
                { Keys.A, setWalkLeftCommand },

                //'Left Arrow' -> Player Walk Left:
                { Keys.Left, setWalkLeftCommand },

                //'S' -> Player Walk Down:
                { Keys.S, setWalkDownCommand },

                //'Down Arrow' -> Player Walk Down:
                { Keys.Down, setWalkDownCommand },

                //'D' -> Player Walk Right:
                { Keys.D, setWalkRightCommand },

                //'Right Arrow' -> Player Walk Right:
                { Keys.Right, setWalkRightCommand },

                //'E' -> Damage Player:
                { Keys.E, damageLinkCommand },

                //'O' -> Cycle Enemy Left:
                //{ Keys.O, cycleEnemyLeftCommand },

                //'P' -> Cycle Enemy Right:
                //{ Keys.P, cycleEnemyRightCommand },

                //'I' -> Cycle Item Left:
                { Keys.I, cycleItemLeftCommand },

                //'U' -> Cycle Item Right:
                { Keys.U, cycleItemRightCommand },

                //'T' -> Cycle Block Left:
                { Keys.T, cycleBlockLeftCommand },

                //'Y' -> Cycle Block Right:
                { Keys.Y, cycleBlockRightCommand },

                //'D1' -> Player Use Arrow Item:
                { Keys.D1, useItemArrow },

                //'D2' -> Player Use Boomerang Item:
                { Keys.D2, useItemBmrng },

                //'D3' -> Player Use Bomb Item:
                { Keys.D3, useItemBomb },

                //'D4' -> Player Use Sword:
                { Keys.D4, setAttackCommand },

            };

            _keyboardController = new KeyboardController(keyboardCommandMap);


            // Set up GamPadController Button dictionary:
            var buttonCommandMap = new Dictionary<GamePadButtonEnums, ICommand>
            {

                //'A' -> Player Attack:
                { GamePadButtonEnums.DownBtn, setAttackCommand },

                //'B' -> Player Use Item:
                { GamePadButtonEnums.RightBtn, useItemArrow }, // Will be set to current item once full item system is implemented

                //'Start' -> Open Menu (Pause Game):
                //{ GamePadButtonEnums.Start, openMenuCommand } // Waiting until I complete menu implementations

                //'Select' -> Open Options (Pause Game):
                //{ GamePadButtonEnums.Select, openOptionsCommand } // Waiting until I complete menu implementation

            };

            var joystickCommandMap = new Dictionary<GamePadJoystickEnums, ICommand>
            {

                //'Up' -> Move Up
                { GamePadJoystickEnums.UpLJS, setWalkUpCommand },

                //'Left' -> Move Left
                { GamePadJoystickEnums.LeftLJS, setWalkLeftCommand },

                //'Right' -> Move Right
                { GamePadJoystickEnums.RightLJS, setWalkRightCommand },

                //'Down' -> Move Down
                { GamePadJoystickEnums.DownLJS, setWalkDownCommand }

            };

            var dPadCommandMap = new Dictionary<GamePadDPadEnums, ICommand>
            {

                { GamePadDPadEnums.None, setIdleCommand },

                //'Up' -> Move Up
                { GamePadDPadEnums.Up, setWalkUpCommand },

                //'Left' -> Move Left
                { GamePadDPadEnums.Left, setWalkLeftCommand },

                //'Right' -> Move Right
                { GamePadDPadEnums.Right, setWalkRightCommand },

                //'Down' -> Move Down
                { GamePadDPadEnums.Down, setWalkDownCommand }

            };


            _mouseController = new MouseController(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);


            _gamePadController = new GamePadController(buttonCommandMap, joystickCommandMap, dPadCommandMap);


            item2 = new Item();
            item2 = item2.CreateItem(ItemType.Arrow, 1, 6);
            enemySprites.ForEach(enemySprite => gameObjects.Add(enemySprite));
            gameObjects.Add(item);
            gameObjects.Add(linkSprite);
            gameObjects.Add(item2);

            levelMap.AddLink(linkSprite);
            //gameObjects.Add(_block);
            //gameObjects.Add(_loadRoomBlock);
        }

    }
}
