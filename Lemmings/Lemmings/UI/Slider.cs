using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lemmings.UI
{
    public class Slider : UIElement, IInteractableUI, IDrawableUI
    {
        #region Private Fields

        private Color background;
        private Color backgroundHover;
        private Rectangle bounds;
        private Vector2 draggedPosition;
        private Color foreground;
        private Color foregroundHover;
        private SliderHandle handle;
        private bool hover;
        private bool pressed;
        private Vector2 pressedPosition;
        private int steps;
        private int texture;

        private float value;

        #endregion Private Fields

        #region Public Constructors

        public Slider(UIManager manager, IUIElement parent) : base(manager, parent)
        {
            background = Color.LightGray;
            backgroundHover = Color.White;
            foreground = foregroundHover = Color.Black;
            handle = new SliderHandle();
            steps = -1;
        }

        #endregion Public Constructors

        #region Public Delegates

        public delegate void SliderEventHandler(float value);

        #endregion Public Delegates

        #region Public Events

        public event UIEventHandler Click;

        public event UIEventHandler Focus;

        public event UIEventHandler LostFocus;

        public event SliderEventHandler ValueUpdated;

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
            set { bounds = value; }
        }

        public Point Dimension
        {
            get { return new Point(bounds.Width, bounds.Height); }
            set
            {
                bounds.Width = value.X;
                bounds.Height = value.Y;
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

        public SliderHandle Handle { get { return handle; } }

        public bool Hover
        {
            get { return hover; }
            set { hover = value; }
        }

        public int Steps
        {
            get { return steps; }
            set { steps = value; }
        }

        public int Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public float Value
        {
            get { return value; }
            set
            {
                float newValue = MathHelper.Clamp(value, 0, 1);

                if (newValue != this.value)
                {
                    this.value = newValue;
                    handle.Value = this.value;
                    OnValueUpdated();
                }
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assetmanager.GetTexture(texture), bounds, hover ? backgroundHover : background);
            handle.Draw(spriteBatch, hover ? foregroundHover : foreground);
            base.Draw(spriteBatch);
        }

        public void RecalculateBounds()
        {
            Texture2D tex = Assetmanager.GetTexture(texture);
            bounds = new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
            handle.RecalculateBounds();
            handle.StartPosition = new Vector2(bounds.Left - handle.Bounds.Width / 2f, bounds.Center.Y - handle.Bounds.Height / 2);
            handle.EndPosition = new Vector2(bounds.Right - handle.Bounds.Width / 2f, bounds.Center.Y - handle.Bounds.Height / 2);
            handle.Value = value;
        }

        public void SetHandleTexture(int texture)
        {
            handle.Texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            Interaction();

            base.Update(gameTime);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Interaction()
        {
            Point cursor = Input.MousePosition();
            bool hovered = (bounds.Contains(cursor) || handle.Bounds.Contains(cursor));

            if (hovered & !hover)
                OnFocus();
            else if (hover & !hovered)
                OnLostFocus();

            hover = hovered;
            if (hover && Input.MousePressed(MouseButton.Left))
                OnClick();

            bool newPressed = hover && Input.MouseDown(MouseButton.Left);
            if (!newPressed)
            {
                pressed = false;
                return;
            }

            if (!pressed)
            {
                pressedPosition = new Vector2(cursor.X, cursor.Y);
                Value = GetPressedValue();
            }
            else
            {
                draggedPosition = new Vector2(cursor.X, cursor.Y);
                Value = GetValue();
            }

            Debug.Log(value);

            pressed = newPressed;
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

        protected void OnValueUpdated()
        {
            ValueUpdated?.Invoke(value);
        }

        #endregion Protected Methods

        #region Private Methods

        private float GetPressedValue()
        {
            float offset = (pressedPosition.X - handle.StartPosition.X) - handle.Bounds.Width / 2f;
            if (offset == 0)
                return 0;

            if (steps == -1)
                return offset / (handle.EndPosition.X - handle.StartPosition.X);
            else
                return (int)Math.Round(offset / (handle.EndPosition.X - handle.StartPosition.X) * steps) / (float)steps;
        }

        private float GetValue()
        {
            if (handle.StartPosition.X > draggedPosition.X - handle.Bounds.Width / 2f)
                return 0;
            if (handle.EndPosition.X < draggedPosition.X - handle.Bounds.Width / 2f)
                return 1;

            float offset = (draggedPosition.X - handle.StartPosition.X) - handle.Bounds.Width / 2f;
            if (offset == 0)
                return 0;

            if (steps == -1)
                return offset / (handle.EndPosition.X - handle.StartPosition.X);
            else
                return (int)Math.Round(offset / (handle.EndPosition.X - handle.StartPosition.X) * steps) / (float)steps;
        }

        #endregion Private Methods

        #region Public Structs

        public struct SliderHandle
        {
            #region Private Fields

            private Rectangle bounds;

            private Vector2 end;

            private Vector2 position;

            private Vector2 start;

            private int texture;

            #endregion Private Fields

            #region Public Properties

            public Rectangle Bounds
            {
                get { return bounds; }
                set { bounds = value; }
            }

            public Vector2 EndPosition
            {
                get { return end; }
                set { end = value; }
            }

            public Vector2 Position
            {
                get { return position; }
            }

            public Vector2 StartPosition
            {
                get { return start; }
                set { start = value; }
            }

            public int Texture
            {
                get { return texture; }
                set { texture = value; }
            }

            public float Value
            {
                set
                {
                    position = Vector2.Lerp(start, end, value);
                    bounds.X = (int)position.X;
                    bounds.Y = (int)position.Y;
                }
            }

            #endregion Public Properties

            #region Public Methods

            public void Draw(SpriteBatch spriteBatch, Color color)
            {
                spriteBatch.Draw(Assetmanager.GetTexture(texture), position, color);
            }

            public void RecalculateBounds()
            {
                bounds = Assetmanager.GetTexture(texture).Bounds;
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
            }

            #endregion Public Methods
        }

        #endregion Public Structs
    }
}