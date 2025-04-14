using System;
using System.Collections.Generic;
using System.Windows.Input;
using MainGame.CheatCommands;
using MainGame.Cheats;
using MainGame.Commands;
using MainGame.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


public class CheatCodeManager
{
    // Buffer for recent key presses
    private List<string> recentKeys;
    // max allowed delay in (ms) between key presses before buffer is cleared
    private double maxDelayBetweenKeys;
    private double timeSinceLastKey;

    // Cheat sequences mapped to cheat commmands
    private Dictionary<string, MainGame.Commands.ICommand> cheatCommands;

    public CheatCodeManager(Link link, LevelManager levelManager)
    {
        if (link == null)
            throw new ArgumentNullException(nameof(link));
        if (levelManager == null)
            throw new ArgumentNullException(nameof(levelManager));


        recentKeys = new List<string>();
        maxDelayBetweenKeys = 1000; // 1 seconds between keys 
        timeSinceLastKey = 0;

        // The keys here are the sequences for the cheat codes
        this.cheatCommands = new Dictionary<string, MainGame.Commands.ICommand>()
        {
            { "UPUPDOWNDOWNLEFTRIGHTLEFTRIGHT", new ToggleInivicibility(link) }, // doesnt work yet
            { "UPDOWNUPDOWN", new IncreaseLinkSpeed(link) },
            { "DOWNUPDOWNUP", new DecreaseLinkSpeed(link) },
            { "LEFTRIGHTRIGHTLEFT", new IncreaseLinkSize(link) }, // doesnt work yet
            { "RIGHTLEFTLEFTRIGHT", new DecreaseLinkSize(link) }, // doesnt work yet
            { "UPDOWNLEFTRIGHT", new SpreadArrowAttack(link) },
            { "UPDOWNRIGHTLEFTDOWNRIGHTUPDOWNRIGHTLEFT", new SpawnTriForce(levelManager) } // doesnt work yet
        };
    }

    public void Update(GameTime gameTime)
    {
        timeSinceLastKey += gameTime.ElapsedGameTime.TotalMilliseconds;

        KeyboardState state = Keyboard.GetState();
        Keys[] pressedKeys = state.GetPressedKeys();

        if (pressedKeys.Length > 0)
        {
            foreach (var key in pressedKeys)
            {
                string keyStr = key.ToString().ToUpper();
                if (recentKeys.Count == 0 || recentKeys[recentKeys.Count - 1] != keyStr)
                {
                    recentKeys.Add(keyStr);
                    timeSinceLastKey = 0; // recent timer on key press
                }
            }

            string currentSequence = string.Join("", recentKeys);

            foreach (var cheat in cheatCommands)
            {
                if (currentSequence.EndsWith(cheat.Key, StringComparison.OrdinalIgnoreCase))
                {
                    // Cheat found - execute
                    cheat.Value.Execute();

                    recentKeys.Clear();
                    break;
                }
            }
        }
        // If to much time passes clear buffer
        if (timeSinceLastKey > maxDelayBetweenKeys)
        {
            recentKeys.Clear();
        }
    }
}

