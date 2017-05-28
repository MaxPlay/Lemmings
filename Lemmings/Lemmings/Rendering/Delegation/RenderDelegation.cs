using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Rendering.Delegation
{
    public struct RenderDelegation
    {
        #region Public Fields

        public IRenderDelegatable Delegatable;
        public IDelegateDrawSettings Settings;

        #endregion Public Fields

        #region Public Constructors

        public RenderDelegation(IRenderDelegatable delegatable, IDelegateDrawSettings settings)
        {
            Delegatable = delegatable;
            Settings = settings;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            Delegatable.Draw(spriteBatch, Settings);
        }

        #endregion Public Methods
    }
}