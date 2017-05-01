using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;

namespace Lemmings.UI
{
    public class RadioButton : Checkbox
    {
        #region Private Fields

        private RadioButtonGroup group;

        #endregion Private Fields

        #region Public Constructors

        public RadioButton(UIManager manager, IUIElement parent) : base(manager, parent)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public RadioButtonGroup Group
        {
            get { return group; }
            set { group?.Unregister(this); group = value; value?.Register(this); }
        }

        #endregion Public Properties

        #region Protected Methods

        protected override void Interaction()
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

        #endregion Protected Methods
    }
}