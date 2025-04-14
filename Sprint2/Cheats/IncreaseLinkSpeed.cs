using System.Diagnostics;
using MainGame.Commands;

namespace MainGame.Cheats
{
    public class IncreaseLinkSpeed : ICommand
    {
        private readonly Link _link;
        private const float IncreaseAmount = 1.0f;

        public IncreaseLinkSpeed(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.SpeedMultiplier += IncreaseAmount;
            System.Diagnostics.Debug.WriteLine("Cheat Activated: Link is faster!");
        }
    }
}
