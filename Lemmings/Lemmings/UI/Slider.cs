using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using System;

namespace Lemmings.UI
{
    public class Slider : ButtonBase
    {
        #region Public Fields

        public SliderHandle handle;

        #endregion Public Fields

        #region Private Fields

        private Vector2 draggedPosition;
        private bool pressed;
        private Vector2 pressedPosition;
        private int steps;

        private float value;

        #endregion Private Fields

        #region Public Constructors

        public Slider(UIManager manager, IUIElement parent) : base(manager, parent)
        {
            handle = new SliderHandle();
            steps = -1;
        }

        #endregion Public Constructors

        #region Public Delegates

        public delegate void SliderEventHandler(float value);

        #endregion Public Delegates

        #region Public Events

        public event SliderEventHandler ValueUpdated;

        #endregion Public Events

        #region Public Properties

        public int Steps
        {
            get { return steps; }
            set { steps = value; }
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

        #region Protected Methods

        protected override void Interaction()
        {
            base.Interaction();

            Point mousePosition = Input.MousePosition();
            bool newPressed = hover && bounds.Contains(mousePosition) || handle.Bounds.Contains(mousePosition);
            if (!newPressed)
            {
                pressed = false;
                return;
            }

            if (!pressed)
            {
                pressedPosition = new Vector2(mousePosition.X, mousePosition.Y);
                handle.Value = GetPressedValue();
            }
            else
            {
                draggedPosition = new Vector2(mousePosition.X, mousePosition.Y);
                handle.Value = GetValue();
            }

            pressed = newPressed;
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
            #region Public Fields

            public Rectangle Bounds;
            public Vector2 EndPosition;
            public Vector2 Position;
            public Vector2 StartPosition;
            public int Texture;

            #endregion Public Fields

            #region Public Properties

            public float Value { set { Position = Vector2.Lerp(StartPosition, EndPosition, value); } }

            #endregion Public Properties

            #region Public Methods

            public void RecalculateBounds()
            {
                Bounds = Assetmanager.GetTexture(Texture).Bounds;
                Bounds.X = (int)Position.X;
                Bounds.Y = (int)Position.Y;
            }

            #endregion Public Methods
        }

        #endregion Public Structs
    }
}