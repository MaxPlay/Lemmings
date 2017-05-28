using Lemmings.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.UI.Internal
{
    public interface IDrawableUI
    {
        #region Public Properties

        Color Background { get; set; }
        Color BackgroundHover { get; set; }
        Rectangle Bounds { get; }
        Point Dimension { get; set; }
        Color Foreground { get; set; }
        Color ForegroundHover { get; set; }
        bool Hover { get; }
        Sprite Sprite { get; set; }

        #endregion Public Properties

        #region Public Methods

        void Draw(SpriteBatch spriteBatch);

        #endregion Public Methods
    }
}