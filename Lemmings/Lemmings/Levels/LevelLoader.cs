using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lemmings.Levels
{
    public class LevelLoader
    {
        #region Private Fields

        private int currentLevel;
        private List<string> levels;

        #endregion Private Fields

        #region Public Constructors

        public LevelLoader()
        {
            if (!File.Exists("levels.lvls"))
                throw new FileNotFoundException("The file that lists the levels could not be found.", "levels.lvls");

            levels = File.ReadAllLines("levels.lvls").ToList();
        }

        #endregion Public Constructors

        #region Public Methods

        public string GetCurrentLevel()
        {
            return levels[currentLevel];
        }

        public void SetCurrentLevel(int id)
        {
            currentLevel = id;
        }

        public void SetCurrentLevel(string name)
        {
            currentLevel = levels.IndexOf(name);
        }

        #endregion Public Methods
    }
}