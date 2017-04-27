﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.UI.Internal
{
    public class ButtonBase : UIElement
    {
        #region Protected Fields

        protected Color background;
        protected Color backgroundHover;
        protected Rectangle bounds;
        protected string font;
        protected Color foreground;
        protected Color foregroundHover;
        protected bool hover;
        protected string text;

        protected Vector2 textDimension;

        protected Vector2 textLocation;

        protected int texture;

        #endregion Protected Fields

        #region Public Constructors

        public ButtonBase(UIManager manager, IUIElement parent) : base(manager, parent)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public Color Background
        {
            get { return background; }
            set { background = value; }
        }

        public Color BackgroundHover
        {
            get { return backgroundHover; }
            set { backgroundHover = value; }
        }

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
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

        public Color ForegroundHover
        {
            get { return foregroundHover; }
            set { foregroundHover = value; }
        }

        public bool Hover
        {
            get { return hover; }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                MeasureString();
            }
        }

        public int Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawButton(spriteBatch);
            DrawText(spriteBatch);
            base.Draw(spriteBatch);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void DrawButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assetmanager.GetTexture(texture), bounds, hover ? backgroundHover : background);
        }

        protected virtual void DrawText(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Assetmanager.GetFont(font), text, textLocation, hover ? foregroundHover : foreground);
        }

        protected virtual void MeasureString()
        {
            textDimension = Assetmanager.GetFont(font).MeasureString(text);
            textLocation = new Vector2(bounds.Center.X, bounds.Center.Y) - textDimension * 0.5f;
        }

        #endregion Protected Methods
    }
}