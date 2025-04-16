using System.Diagnostics;
using MainGame.Commands;
using MainGame.Sprites;

namespace MainGame.Cheats
{
    public class DecreaseLinkSize : ICommand
    {
        private readonly Link _link;
        private const float DecreaseAmount = 0.3f; 

        public DecreaseLinkSize(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            if (_link.CurrentSprite is LinkSprite linkSprite)
            {
                _link.LinkScale -= DecreaseAmount;
                System.Diagnostics.Debug.WriteLine("Cheat Activated: Link is smaller!");
            }
            
        }
    }
}