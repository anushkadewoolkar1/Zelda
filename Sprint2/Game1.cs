﻿using Microsoft.Xna.Framework;
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


namespace Sprint0
{
    public class Game1 : Game
    {
        public bool restart;

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

        Enemy enemySprite;


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
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
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

            restart = false;

            // Create the block at position (200, 200)
            _block = new Block(new Vector2(200, 200), blockTextures);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ProjectileSpriteFactory.Instance.LoadProjectileTextures(Content);

            ItemSpriteFactory.Instance.ItemTextures(Content);
            itemSprite = ItemSpriteFactory.Instance.FetchItemSprite("ZeldaSpriteArrow");

            LinkSpriteFactory.Instance.LoadLinkTextures(Content);

            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            enemySprite = new Enemy();

            //Load sprite font
            _spriteFont = Content.Load<SpriteFont>("DefaultFont");

            linkSprite = new Link();

            // Create Commands 
            var quitCommand = new ExitCommand(this, false);
            var resetCommand = new ExitCommand(this, true);
            var idleStateCommand = new ChangeLinkState(linkSprite, new LinkIdleState((linkSprite), Zelda.Enums.Direction.Down)); // Will be changed to current direction, directly accessing Link's direction value once implemented
            var attackLeftCommand = new ChangeLinkState(linkSprite, new LinkAttackingState(linkSprite, Zelda.Enums.Direction.Left, SwordType.WoodenSword));
            var attackRightCommand = new ChangeLinkState(linkSprite, new LinkAttackingState(linkSprite, Zelda.Enums.Direction.Right, SwordType.WoodenSword));
            var moveUpCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Up));
            var moveDownCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Down));
            var moveLeftCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Left));
            var moveRightCommand = new ChangeLinkState(linkSprite, new LinkWalkingState(linkSprite, Zelda.Enums.Direction.Right));
            var linkDamagedCommand = new LinkDamaged(linkSprite);
            var enemyCycleLeftCmd = new CycleEnemy(enemySprite, Direction.Left);
            var enemyCycleRightCmd = new CycleEnemy(enemySprite, Direction.Right);
            var itemCycleLeftCmd = new CycleItem(itemSprite, Direction.Left);
            var itemCycleRightCmd = new CycleItem(itemSprite, Direction.Right);
            var blockCycleLeftCmd = new CycleBlock(_block, Direction.Left);
            var blockCycleRightCmd = new CycleBlock(_block, Direction.Right);
            var useItemArrow = new LinkUseItem(linkSprite, ItemSpriteFactory.Instance.FetchItemSprite("ZeldaSpriteArrow"));
            var useItemBmrg = new LinkUseItem(linkSprite, ItemSpriteFactory.Instance.FetchItemSprite("ZeldaSpriteBoomerang"));
            var useItemBomb = new LinkUseItem(linkSprite, ItemSpriteFactory.Instance.FetchItemSprite("ZeldaSpriteBomb"));

            // Set up KeyboardController with dictionary
            var keyboardCommandMap = new Dictionary<Keys, ICommand>
            {
                { Keys.Q, quitCommand },

                { Keys.R, resetCommand },

                { Keys.None, idleStateCommand },

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

                { Keys.O, enemyCycleLeftCmd },

                { Keys.P, enemyCycleRightCmd },

                { Keys.I, itemCycleRightCmd },

                { Keys.U, itemCycleLeftCmd },

                { Keys.E, linkDamagedCommand },

                { Keys.T, blockCycleLeftCmd },

                { Keys.Y, blockCycleRightCmd },

                { Keys.D1, useItemArrow },

                { Keys.D2, useItemBmrg },

                { Keys.D3, useItemBomb }

            };

            _keyboardController = new KeyboardController(keyboardCommandMap);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _keyboardController.Update();

            //_currentSprite.Update(gameTime);

            enemySprite.Update(gameTime);

            itemSprite.Update(gameTime);

            linkSprite.Update(gameTime);

            _block.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            linkSprite.Draw(_spriteBatch);

            enemySprite.DrawCurrentSprite(_spriteBatch);

            _block.Draw(_spriteBatch);

            itemSprite.Draw(_spriteBatch, new Vector2(300, 100));

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
