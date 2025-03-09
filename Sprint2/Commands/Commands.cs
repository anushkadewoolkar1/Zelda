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
using Sprint0;
using Sprint0.Controllers;
using Sprint0.Sprites;
using Sprint0.States;
using Zelda.Enums;

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

    public class CycleEnemy : ICommand
    {
        private Enemy _enemy;
        private Direction _direction;

        public CycleEnemy(Enemy enemy, Direction direction)
        {
            _enemy = enemy;
            _direction = direction;
        }

        public void Execute()
        {
            if (_direction == Direction.Left)
            {
                _enemy.ChangeEnemyBackward();
            }
            else
            {
                _enemy.ChangeEnemyForward();
            }
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
            _link.ChangeState(new LinkUsingItemState(_link, _link.currentDirection, _item));
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

}


