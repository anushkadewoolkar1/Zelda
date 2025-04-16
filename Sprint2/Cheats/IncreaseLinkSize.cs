using System.Diagnostics;
using MainGame.Commands;
using MainGame.Sprites;

namespace MainGame.Cheats
{
    public class IncreaseLinkSize : ICommand
    {
        private readonly Link _link;
        private const float IncreaseAmount = 0.3f; 

        public IncreaseLinkSize(Link link)
        {
            _link = link;
        }

        public void Execute()
        {
            if(_link.CurrentSprite is LinkSprite linkSprite)
            {
                _link.LinkScale += IncreaseAmount;
                System.Diagnostics.Debug.WriteLine("Cheat Activated: Link is bigger!");
            }
            
        }
    }
}
    
