using Lemmings.Rendering;
using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.UI
{
    public class Checkbox : ButtonBase
    {
        #region Private Fields

        private Sprite checkedSprite;
        private CheckState checkState;

        private float intermediateTransparency;

        private int labelDistance;

        #endregion Private Fields

        #region Public Constructors

        public Checkbox(UIManager manager, IUIElement parent) : base(manager, parent)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public Sprite CheckedSprite
        {
            get { return checkedSprite; }
            set { checkedSprite = value; }
        }

        public CheckState CheckState
        {
            get { return checkState; }
            set { checkState = value; }
        }

        public float IntermediateTransparency
        {
            get { return intermediateTransparency; }
            set { intermediateTransparency = MathHelper.Clamp(value, 0, 1); }
        }

        public int LabelDistance
        {
            get { return labelDistance; }
            set { labelDistance = value; }
        }

        #endregion Public Properties

        #region Protected Methods

        protected override void DrawButton(SpriteBatch spriteBatch)
        {
            base.DrawButton(spriteBatch);
            if (checkState == CheckState.Checked)
                checkedSprite?.Draw(spriteBatch, bounds, foreground * ((checkState == CheckState.Intermediate) ? intermediateTransparency : 1));
        }

        protected override void DrawText(SpriteBatch spriteBatch)
        {
            if (Assetmanager.GetFont(font) == null)
                return;

            spriteBatch.DrawString(Assetmanager.GetFont(font), text, new Vector2(bounds.Right + labelDistance, textLocation.Y), hover ? foregroundHover : foreground);
        }

        #endregion Protected Methods
    }
}