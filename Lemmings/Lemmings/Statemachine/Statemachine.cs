using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Lemmings.Statemachine
{
    public class StateMachine
    {
        #region Private Fields

        private string currentState;
        private bool exiting;

        private bool running;

        private Dictionary<string, State> states;

        private string targetState;

        #endregion Private Fields

        #region Public Constructors

        public StateMachine()
        {
            states = new Dictionary<string, State>();
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Exiting
        {
            get { return exiting; }
        }

        #endregion Public Properties

        #region Public Methods

        public void AddState(string name, State state)
        {
            if (states.ContainsKey(name))
                return;

            states.Add(name, state);
            if (states.Count == 1)
            {
                currentState = name;
                targetState = name;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            states[currentState].Draw(spriteBatch);
        }

        public void ExitGame()
        {
            exiting = true;
        }

        public void SetCurrentState(string name)
        {
            if (states.ContainsKey(name))
                targetState = name;
        }

        public void Start()
        {
            running = true;
            states[currentState].Initialize();
        }

        public void Stop()
        {
            running = false;
            states[currentState].Unload();
        }

        public void Update(GameTime gameTime)
        {
            if (states.Count == 0 || !running)
                return;

            if (targetState != currentState)
            {
                states[targetState].Initialize();
                states[currentState].Unload();
                currentState = targetState;
            }
            states[targetState].Update(gameTime);
        }

        #endregion Public Methods
    }
}