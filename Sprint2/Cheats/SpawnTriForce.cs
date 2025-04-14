using System.Diagnostics;
using MainGame.Commands;
using MainGame.Display;

namespace MainGame.CheatCommands
{
    public class SpawnTriForce : ICommand
    {
        private readonly LevelManager _levelManager;

        public SpawnTriForce(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public void Execute()
        {
            _levelManager.SpawnTriForce();
            Debug.WriteLine("Cheat activated: TriForce spawned");
        }
    }
}