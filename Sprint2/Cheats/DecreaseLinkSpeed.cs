using System.Diagnostics;
using MainGame.Commands;

namespace MainGame.Cheats
{
    public class DecreaseLinkSpeed : ICommand
    {
        private readonly Link _link;
        private const float DecreaseAmount = 1.0f;

        public DecreaseLinkSpeed(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.SpeedMultiplier -= DecreaseAmount;
            System.Diagnostics.Debug.WriteLine("Cheat Activated: Link is slower!");
        }
    }
}