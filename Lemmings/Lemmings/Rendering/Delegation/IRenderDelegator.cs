using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Rendering.Delegation
{
    internal interface IRenderDelegator
    {
        #region Public Methods

        void ClearDelegator();

        void Delegate(IRenderDelegatable element, IDelegateDrawSettings settings);

        void DrawDelegates(SpriteBatch spriteBatch);

        void SetupDelegator();

        #endregion Public Methods
    }
}