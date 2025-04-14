using System.Diagnostics;
using MainGame.Commands;

namespace MainGame.Cheats
{
    public class IncreaseLinkSize : ICommand
    {
        private readonly Link _link;
        private const float IncreaseAmount = 0.1f; // 10%

        public IncreaseLinkSize(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            _link.Scale += IncreaseAmount;
            System.Diagnostics.Debug.WriteLine("Cheat Activated: Link is bigger!");
        }
    }
}
    
