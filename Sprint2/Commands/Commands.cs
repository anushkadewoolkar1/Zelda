using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0;
using Sprint0.Controllers;
using Sprint0.Sprites;
using Zelda.enums;

namespace Sprint0.Commands
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
            currLink.changeState(_targetState);
        }
    }
}
