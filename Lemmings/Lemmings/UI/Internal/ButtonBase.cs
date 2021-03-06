﻿using Lemmings.Localization;
using Lemmings.Rendering;
using Lemmings.Rendering.Delegation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.UI.Internal
{
    public class ButtonBase : UIElement, IInteractableUI, IDrawableUI
    {
        #region Protected Fields

        protected Color background;
        protected Color backgroundHover;
        protected Rectangle bounds;
        protected string font;
        protected Color foreground;
        protected Color foregroundHover;
        protected bool hover;
        protected string localizationKey;

        protected Sprite sprite;
        protected string text;

        protected Vector2 textDimension;

        protected Vector2 textLocation;

        #endregion Protected Fields

        #region Public Constructors

        public ButtonBase(UIManager manager, IUIElement parent) : base(manager, parent)
        {
            background = Color.LightGray;
            backgroundHover = Color.White;
            foreground = foregroundHover = Color.Black;
            text = string.Empty;
            font = string.Empty;
            Localizer.CultureChanged += Localizer_CultureChanged;
        }

        #endregion Public Constructors

        #region Public Events

        public event UIEventHandler Click;

        public event UIEventHandler Focus;

        public event UIEventHandler LostFocus;

        #endregion Public Events

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
        }

        public Point Dimension
        {
            get { return new Point(bounds.Width, bounds.Height); }
            set
            {
                bounds.Width = value.X;
                bounds.Height = value.Y;
                MeasureString();
            }
        }

        public string Font
        {
            get { return font; }
            set
            {
                font = value;
                MeasureString();
            }
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

        public string LocalizationKey
        {
            get { return localizationKey; }
            set { localizationKey = value; Text = Localizer.GetString(localizationKey); }
        }

        public Sprite Sprite
        {
            get { return sprite; }
            set { sprite = value; }
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

        #endregion Public Properties

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawButton(spriteBatch);
            DrawText(spriteBatch);
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            Interaction();

            base.Update(gameTime);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void DrawButton(SpriteBatch spriteBatch)
        {
            if (sprite is SlicedSprite)
            {
                if (root is IRenderDelegator)
                    ((IRenderDelegator)root).Delegate(sprite, new SpriteDrawSettings() { Color = hover ? backgroundHover : background, Bounds = bounds });
            }
            else
                sprite?.Draw(spriteBatch, bounds, hover ? backgroundHover : background);
        }

        protected virtual void DrawText(SpriteBatch spriteBatch)
        {
            if (Assetmanager.GetFont(font) == null)
                return;

            if (sprite is SlicedSprite)
            {
                if (root is IRenderDelegator)
                    ((IRenderDelegator)root).Delegate(new DelegatableFont() { FontName = font }, new FontSettings() { Color = hover ? foregroundHover : foreground, Position = textLocation, Text = text });
            }
            else
                spriteBatch.DrawString(Assetmanager.GetFont(font), text, textLocation, hover ? foregroundHover : foreground);
        }

        protected virtual void Interaction()
        {
            Point cursor = Input.MousePosition();
            bool hovered = bounds.Contains(cursor);

            if (hovered & !hover)
                OnFocus();
            else if (hover & !hovered)
                OnLostFocus();

            hover = hovered;
            if (hover && Input.MousePressed(MouseButton.Left))
                OnClick();
        }

        protected virtual void MeasureString()
        {
            if (text == null)
                text = string.Empty;
            textDimension = Assetmanager.GetFont(font).MeasureString(text);
            textLocation = new Vector2(bounds.Center.X, bounds.Center.Y) - textDimension * 0.5f;
        }

        protected void OnClick()
        {
            Click?.Invoke(this);
        }

        protected void OnFocus()
        {
            Focus?.Invoke(this);
        }

        protected void OnLostFocus()
        {
            LostFocus?.Invoke(this);
        }

        protected override void PositionUpdated()
        {
            bounds.X = (int)Position.X;
            bounds.Y = (int)Position.Y;
        }

        #endregion Protected Methods

        #region Private Methods

        private void Localizer_CultureChanged(string culture)
        {
            if (string.IsNullOrWhiteSpace(localizationKey))
                return;

            Text = Localizer.GetString(localizationKey);
        }

        #endregion Private Methods
    }
}