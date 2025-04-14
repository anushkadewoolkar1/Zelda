using System.Diagnostics;
using MainGame.Commands;

namespace MainGame.Cheats
{
    public class DecreaseLinkSize : ICommand
    {
        private readonly Link _link;
        private const float DecreaseAmount = 0.1f; // 10%

        public DecreaseLinkSize(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Scale -= DecreaseAmount;
            System.Diagnostics.Debug.WriteLine("Cheat Activated: Link is smaller!");
        }
    }
}