using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lemmings.Levels
{
    public class LevelLoader
    {
        private int currentLevel;
        List<string> levels;

        public LevelLoader()
        {
            if (!File.Exists("levels.lvls"))
                throw new FileNotFoundException("The file that lists the levels could not be found.", "levels.lvls");

            levels = File.ReadAllLines("levels.lvls").ToList();
        }

        public void SetCurrentLevel(int id)
        {
            currentLevel = id;
        }

        public void SetCurrentLevel(string name)
        {
            currentLevel = levels.IndexOf(name);
        }

        public string GetCurrentLevel()
        {
            return levels[currentLevel];
        }
    }
}
