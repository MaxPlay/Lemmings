using Microsoft.Xna.Framework;

namespace Lemmings.Rendering.Delegation
{
    internal struct SpriteDrawSettings : IDelegateDrawSettings
    {
        #region Public Fields

        public Rectangle Bounds;
        public Color Color;

        #endregion Public Fields
    }
}