using Lemmings.Statemachine;
using Lemmings.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.States
{
    public class Editor : State
    {
        #region Private Fields

        private UIManager ui;

        #endregion Private Fields

        #region Public Constructors

        public Editor(string name, StateMachine statemachine) : base(name, statemachine)
        {
            ui = new UIManager();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        public override void Initialize()
        {
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        #endregion Public Methods
    }
}