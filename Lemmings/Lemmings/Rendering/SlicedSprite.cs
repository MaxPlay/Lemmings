using Lemmings.Exceptions;
using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Rendering
{
    public class SlicedSprite : Sprite
    {
        #region Private Fields

        private int bottom;
        private int bottomHeight;
        private int centerHeight;
        private int centerWidth;
        private int left;
        private int right;
        private int rightWidth;
        private Rectangle[] source;
        private int top;

        #endregion Private Fields

        #region Public Constructors

        public SlicedSprite(string name, string texture) : base(name, texture)
        {
            source = new Rectangle[9];
            left = top = 0;
            right = bounds.Width;
            bottom = bounds.Height;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Bottom
        {
            get { return bottom; }
            set { bottom = value; UpdateSources(); }
        }

        public int Left
        {
            get { return left; }
            set { left = value; UpdateSources(); }
        }

        public int Right
        {
            get { return right; }
            set { right = value; UpdateSources(); }
        }

        public int Top
        {
            get { return top; }
            set { top = value; UpdateSources(); }
        }

        #endregion Public Properties

        #region Public Methods

        public new void Draw(SpriteBatch spriteBatch, Rectangle bounds)
        {
            Draw(spriteBatch, bounds, Color.White);
        }

        public new void Draw(SpriteBatch spriteBatch, Rectangle bounds, Color color)
        {
            int width = (bounds.Width < this.bounds.Width) ? this.bounds.Width : bounds.Width;
            int height = (bounds.Height < this.bounds.Height) ? this.bounds.Height : bounds.Height;

            int centerWidth = width - left - rightWidth;
            int centerHeight = height - top - bottomHeight;

            Texture2D tex = Assetmanager.GetTexture(texture);

            spriteBatch.Draw(tex, new Rectangle(bounds.Left, bounds.Top, left, top), source[(int)Handle.TopLeft], color);
            spriteBatch.Draw(tex, new Rectangle(bounds.Left + left, bounds.Top, centerWidth, top), source[(int)Handle.TopCenter], color);
            spriteBatch.Draw(tex, new Rectangle(bounds.Left + left + centerWidth, bounds.Top, rightWidth, top), source[(int)Handle.TopRight], color);

            spriteBatch.Draw(tex, new Rectangle(bounds.Left, bounds.Top + top, left, centerHeight), source[(int)Handle.MiddleLeft], color);
            spriteBatch.Draw(tex, new Rectangle(bounds.Left + left, bounds.Top + top, centerWidth, centerHeight), source[(int)Handle.Center], color);
            spriteBatch.Draw(tex, new Rectangle(bounds.Left + left + centerWidth, bounds.Top + top, rightWidth, centerHeight), source[(int)Handle.MiddleRight], color);

            spriteBatch.Draw(tex, new Rectangle(bounds.Left, bounds.Top + top + centerHeight, left, bottomHeight), source[(int)Handle.BottomLeft], color);
            spriteBatch.Draw(tex, new Rectangle(bounds.Left + left, bounds.Top + top + centerHeight, centerWidth, bottomHeight), source[(int)Handle.BottomCenter], color);
            spriteBatch.Draw(tex, new Rectangle(bounds.Left + left + centerWidth, bounds.Top + top + centerHeight, rightWidth, bottomHeight), source[(int)Handle.BottomRight], color);
        }

        #endregion Public Methods

        #region Private Methods

        private void UpdateSources()
        {
            if (left > right)
                throw new SlicedSpriteFormatException("Horizontal");
            if (top > bottom)
                throw new SlicedSpriteFormatException("Vertical");

            centerWidth = right - left;
            rightWidth = bounds.Width - right;
            centerHeight = bottom - top;
            bottomHeight = bounds.Height - bottom;

            source[(int)Handle.TopLeft] = new Rectangle(0, 0, left, top);
            source[(int)Handle.TopCenter] = new Rectangle(left, 0, centerWidth, top);
            source[(int)Handle.TopRight] = new Rectangle(right, 0, rightWidth, top);

            source[(int)Handle.MiddleLeft] = new Rectangle(0, top, left, centerHeight);
            source[(int)Handle.Center] = new Rectangle(left, top, centerWidth, centerHeight);
            source[(int)Handle.MiddleRight] = new Rectangle(right, top, rightWidth, centerHeight);

            source[(int)Handle.BottomLeft] = new Rectangle(0, bottom, left, bottomHeight);
            source[(int)Handle.BottomCenter] = new Rectangle(left, bottom, centerWidth, bottomHeight);
            source[(int)Handle.BottomRight] = new Rectangle(right, bottom, rightWidth, bottomHeight);
        }

        #endregion Private Methods
    }
}