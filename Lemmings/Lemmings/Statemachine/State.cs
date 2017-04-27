using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Statemachine
{
    public abstract class State
    {
        #region Protected Fields

        protected StateMachine statemachine;

        #endregion Protected Fields

        #region Public Constructors

        public State(string name, StateMachine statemachine)
        {
            this.statemachine = statemachine;
            statemachine.AddState(name, this);
        }

        #endregion Public Constructors

        #region Public Methods

        public abstract void Draw(SpriteBatch spriteBatch);

        public abstract void Initialize();

        public abstract void Unload();

        public abstract void Update(GameTime gameTime);

        #endregion Public Methods
    }
}