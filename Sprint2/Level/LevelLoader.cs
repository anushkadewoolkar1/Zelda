
/*
using System;
using System.Collections.Generic;
using System.IO;

namespace MainGame.Display
{
    public class LevelFileReader
    {
        // Contains all tokens (room coordinate markers and object definitions) read from the level file.
        public List<string> Objects { get; private set; }

        // must be "../../../Content/LevelFile.txt"
        public LevelFileReader(string levelFilePath)
        {
            Objects = new List<string>();
            foreach (var line in File.ReadLines(levelFilePath))
            {
                var tokens = line.Split(',');
                for (int i = 0; i < tokens.Length; i++)
                {
                    Objects.Add(tokens[i]);
                }
            }
        }
    }
}
*/
