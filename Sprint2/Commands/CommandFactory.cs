using System.Collections.Generic;
using MainGame.Commands;
using Zelda.Enums;
using MainGame.Sprites;
using MainGame.Display;
using MainGame.States;
using Microsoft.Xna.Framework;

namespace MainGame.Controllers
{
    public class CommandFactory
    {
        private readonly Game1 _game;
        private readonly Link _link;
        private readonly ItemSprite _itemSprite;
        private readonly Block _block;
        private readonly GameAudio _audio;

        public CommandFactory(Game1 game, Link link, ItemSprite itemSprite, Block block, GameAudio audio)
        {
            _game = game;
            _link = link;
            _itemSprite = itemSprite;
            _block = block;
            _audio = audio;
        }

        public Dictionary<UserInputs, ICommand> GetLevelCommandMap()
        {
            return new Dictionary<UserInputs, ICommand>
             {
                 { UserInputs.NewGame, new LeaveStartMenu(_game, _audio) },
                 { UserInputs.None, new ChangeLinkState(_link, new LinkIdleState(_link, _link.currentDirection)) },
                 { UserInputs.AttackMelee, new ChangeLinkState(_link, new LinkAttackingState(_link, _link.currentDirection, SwordType.WoodenSword)) },
                 { UserInputs.UseItem, new LinkUseItem(_link, ItemType.Arrow) },
                 { UserInputs.MoveUp, new ChangeLinkState(_link, new LinkWalkingState(_link, Direction.Up)) },
                 { UserInputs.MoveDown, new ChangeLinkState(_link, new LinkWalkingState(_link, Direction.Down)) },
                 { UserInputs.MoveLeft, new ChangeLinkState(_link, new LinkWalkingState(_link, Direction.Left)) },
                 { UserInputs.MoveRight, new ChangeLinkState(_link, new LinkWalkingState(_link, Direction.Right)) },
                 { UserInputs.ToggleFullscreen, new ToggleFullScreen(_game) },
                 { UserInputs.RaiseVolume, new MasterVolumeUp(_audio) },
                 { UserInputs.LowerVolume, new MasterVolumeDown(_audio) },
                 { UserInputs.ToggleMute, new ToggleMuteMusic(_audio) },
                 { UserInputs.ToggleOptions, new OpenCloseSettings(_game) },
                 { UserInputs.ToggleInventory, new OpenInventory(_game) },
                 { UserInputs.ResetLevel, new ResetCommand(_game) },
                 { UserInputs.CycleItem, new LinkCycleItem(_link) }
             };
        }

        public Dictionary<UserInputs, ICommand> GetMenuCommandMap()
        {
            return new Dictionary<UserInputs, ICommand>
             {
                 { UserInputs.None, new ChangeLinkState(_link, new LinkIdleState(_link, _link.currentDirection)) },
                 { UserInputs.ToggleOptions, new OpenCloseSettings(_game) },
                 { UserInputs.ToggleInventory, new OpenInventory(_game) }
             };
        }
    }
}