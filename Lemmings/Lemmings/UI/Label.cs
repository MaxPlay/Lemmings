using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.UI
{
    public class Label : UIElement
    {
        #region Private Fields

        private Color background;

        private Rectangle bounds;

        private string font;

        private Color foreground;

        private string text;

        #endregion Private Fields

        #region Public Constructors

        public Label(UIManager manager) : base(manager)
        {
        }

        public Label(UIManager manager, IUIElement parent) : base(manager, parent)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public Color Background
        {
            get { return background; }
            set { background = value; }
        }

        public Rectangle Bounds
        {
            get { return bounds; }
        }

        public string Font
        {
            get { return font; }
            set { font = value; }
        }

        public Color Foreground
        {
            get { return foreground; }
            set { foreground = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; MeasureBounds(); }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (background.A != 0)
                spriteBatch.Draw(Game1.Pixel, bounds, background);
            DrawText(spriteBatch);
            base.Draw(spriteBatch);
        }

        #endregion Public Methods

        #region Private Methods

        private void DrawText(SpriteBatch spriteBatch)
        {
            if (Assetmanager.GetFont(font) == null)
                return;

            spriteBatch.DrawString(Assetmanager.GetFont(font), text, position, foreground);
        }

        private void MeasureBounds()
        {
            SpriteFont font = Assetmanager.GetFont(this.font);
            if (font == null)
                return;

            Vector2 dimension = font.MeasureString(text);

            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;
            bounds.Width = (int)dimension.X;
            bounds.Height = (int)dimension.Y;
        }

        #endregion Private Methods
    }
}