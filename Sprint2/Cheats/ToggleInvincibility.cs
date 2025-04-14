using System.Diagnostics;
using MainGame.Commands;
using Zelda.Enums;

namespace MainGame.Cheats
{
    public class ToggleInivicibility : ICommand
    {
        private readonly Link _link;
        private bool _Isactive;

        public ToggleInivicibility(Link link)
        {
            _link = link;
            _Isactive = false;
        }

        public void Execute()
        {
            if (!_Isactive)
            {
                _link.StartInvulnerability();
                _link.invulnerabilityTimer = float.MaxValue;
                _Isactive = true;
                System.Diagnostics.Debug.WriteLine("Cheat Activated: Link is invincible");
            }
            else
            {
                _link.EndInvulnerability();
                _Isactive = false;
                System.Diagnostics.Debug.WriteLine("Cheat Deactivated: Link is no longer invincible");
            }
        }
    }
}