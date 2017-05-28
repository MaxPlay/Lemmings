using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Rendering.Delegation
{
    public interface IRenderDelegatable
    {
        #region Public Properties

        DelegationType Delegation { get; }
        bool DelegationPossible { get; }

        #endregion Public Properties

        #region Public Methods

        void Draw(SpriteBatch spriteBatch, IDelegateDrawSettings settings);

        #endregion Public Methods
    }
}