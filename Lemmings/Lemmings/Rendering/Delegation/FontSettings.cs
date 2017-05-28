using Microsoft.Xna.Framework;

namespace Lemmings.Rendering.Delegation
{
    public struct FontSettings : IDelegateDrawSettings
    {
        #region Public Fields

        public Color Color;
        public Vector2 Position;
        public string Text;

        #endregion Public Fields
    }
}