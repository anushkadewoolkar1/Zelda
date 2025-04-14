using System.Diagnostics;
using MainGame.Commands;

namespace MainGame.Cheats
{
    public class SpreadArrowAttack : ICommand
    {
        private readonly Link _link;

        public SpreadArrowAttack(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.FireSpreadArrows();
            System.Diagnostics.Debug.WriteLine("Cheat Activated: Spread arrow attack!");
        }
    }
}