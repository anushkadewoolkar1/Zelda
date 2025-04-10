using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MainGame;
using MainGame.Controllers;
using MainGame.Sprites;
using MainGame.States;
using MainGame.Display;
using Zelda.Enums;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace MainGame.Commands
{
    public class QuitCommand : ICommand
    {
        private readonly Game1 _game;

        public QuitCommand(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Exit();
        }
    }

    public class SetSpriteCommand : ICommand
    {
        private readonly Game1 _game;
        private readonly ISprite _sprite;

        public SetSpriteCommand(Game1 game, ISprite sprite)
        {
            _game = game;
            _sprite = sprite;
        }

        public void Execute()
        {
            _game.CurrentSprite = _sprite;
        }

    }

    public class ChangeLinkState : ICommand
    {
        private Link _link;
        private ILinkState _targetState;

        public ChangeLinkState(Link currLink, ILinkState state)
        {
            _link = currLink;
            _targetState = state;
        }

        public void Execute()
        {
            _link.ChangeState(_targetState);
        }
    }

    public class LinkDamaged : ICommand
    {
        private Link _link;

        public LinkDamaged(Link currLink)
        {
            _link = currLink;
        }

        public void Execute()
        {
            _link.StartInvulnerability();
        }
    }

    public class CycleItem : ICommand
    {
        private ItemSprite _itemSprite;
        private Direction _direction;

        public CycleItem(ItemSprite itemSprite, Direction direction)
        {
            _itemSprite = itemSprite;
            _direction = direction;
        }

        public void Execute()
        {
            if (_direction == Direction.Left)
            {
                _itemSprite.ItemCycleLeft();
            }
            else
            {
                _itemSprite.ItemCycleRight();
            }
        }
    }

    public class CycleBlock : ICommand
    {
        private Block _block;
        private Direction _direction;

        public CycleBlock(Block block, Direction direction)
        {
            _block = block;
            _direction = direction;
        }

        public void Execute()
        {
            if (_direction == Direction.Left)
            {
                _block.ShiftByXPos(-1);
            }
            else
            {
                _block.ShiftByXPos(1);
            }
        }
    }

    // Temporary implementation for sprint 2 behavior, actual game will simply call Link's UseItem() method:
    public class LinkUseItem : ICommand
    {
        private Link _link;
        private ItemType _item;

        public LinkUseItem(Link link, ItemType item)
        {
            _link = link;
            _item = item;
        }

        public void Execute()
        {
            //_link.PickUpItem(_item);
            _link.ChangeState(new LinkUsingItemState(_link, _link.currentDirection));
        }
    }

    public class ToggleMuteMusic : ICommand
    {
        private GameAudio _audio;
        public ToggleMuteMusic(GameAudio audio)
        {
            _audio = audio;
        }

        public void Execute()
        {
            if (_audio.MediaPlayerState() == MediaState.Playing)
            {
                _audio.MuteBGM();
            }
            else
            {
                _audio.UnmuteBGM();
            }
        }
    }

    public class UnmuteMusic : ICommand
    {
        private GameAudio _audio;
        public UnmuteMusic(GameAudio audio)
        {
            _audio = audio;
        }

        public void Execute()
        {
            _audio.UnmuteBGM();
        }
    }

    public class ResetCommand : ICommand
    {
        private Game1 _game;
        public ResetCommand(Game1 game)
        {
            _game = game;
        }
        public void Execute()
        {
            _game.LoadDynamicObjects();
        }
    }

    public class LeaveStartMenu : ICommand
    {
        private Game1 game;
        private GameAudio audio;

        public LeaveStartMenu(Game1 _game, GameAudio _audio)
        {
            game = _game;
            audio = _audio;
        }

        public void Execute()
        {
            if (game.GameState == Zelda.Enums.GameState.StartMenu)
            {
                game.GameState = Zelda.Enums.GameState.Playing;
                audio.PlayDungeonBGM();
            }
        }
    }

    public class LeaveNotStartMenu : ICommand
    {
        private Game1 game;
        public LeaveNotStartMenu(Game1 _game)
        {
            game = _game;
        }

        public void Execute()
        {
            if (game.GameState == Zelda.Enums.GameState.GameOver)
            {
                game.GameState = Zelda.Enums.GameState.StartMenu;
            }
            if (game.GameState == Zelda.Enums.GameState.Paused)
            {
                game.GameState = Zelda.Enums.GameState.Playing;
            }
        }
    }

    public class LinkCycleItem : ICommand
    {
        private Link _link;

        public LinkCycleItem(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.CycleInventory();
        }
    }

    public class OpenInventory : ICommand
    {
        private Game1 game;
        public OpenInventory(Game1 _game)
        {
            game = _game;
        }

        public void Execute()
        {
            game.isInventoryOpen = !game.isInventoryOpen;
        }
    }

    //Letting this code survive temporarily for continuity purposes, but I plan on reworking (PP):
    public class OpenCloseSettings : ICommand
    {
        private Game1 game;
        public OpenCloseSettings(Game1 _game)
        {
            game = _game;
        }

        public void Execute()
        {
            if (game.GameState == Zelda.Enums.GameState.Playing)
            {
                game.GameState = Zelda.Enums.GameState.Paused;
                game.isSettingsOpen = !game.isSettingsOpen;
            }
            else if (game.GameState == Zelda.Enums.GameState.Paused)
            {
                game.GameState = Zelda.Enums.GameState.Playing;
                game.isSettingsOpen = !game.isSettingsOpen;
            }

        }
    }

    public class ToggleFullScreen : ICommand
    {
        private Game1 game;
        public ToggleFullScreen(Game1 _game)
        {
            game = _game;
        }

        public void Execute()
        {
            game.ToggleFullScreen();
        }
    }

    public class MasterVolumeUp : ICommand
    {
        GameAudio audio;
        public MasterVolumeUp(GameAudio _audio)
        {
            audio = _audio;
        }
        public void Execute()
        {
            audio.RaiseVolume();
        }
    }

    public class MasterVolumeDown : ICommand
    {
        GameAudio audio;
        public MasterVolumeDown(GameAudio _audio)
        {
            audio = _audio;
        }
        public void Execute()
        {
            audio.LowerVolume();
        }
    }

    public class ResetLevel : ICommand
    {
        private Game1 game;
        public ResetLevel(Game1 _game)
        {
            game = _game;
        }

        public void Execute()
        {
            game.ResetLevel();
        }
    }

}

