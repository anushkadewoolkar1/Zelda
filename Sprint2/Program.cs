bool RestartGame;
do
{
    using var game = new Sprint0.Game1();
    game.Run();
    RestartGame = game.restart;
} while (RestartGame);
