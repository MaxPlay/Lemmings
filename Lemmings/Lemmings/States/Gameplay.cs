using Lemmings.Statemachine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.States
{
    public class Gameplay : State
    {
        #region Public Constructors

        public Gameplay(string name, StateMachine statemachine) : base(name, statemachine)
        {
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