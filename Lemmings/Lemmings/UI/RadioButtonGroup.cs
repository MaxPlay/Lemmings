using Lemmings.UI.Internal;
using System.Collections.Generic;

namespace Lemmings.UI
{
    public class RadioButtonGroup
    {
        #region Private Fields

        private List<RadioButton> buttons;

        #endregion Private Fields

        #region Public Constructors

        public RadioButtonGroup()
        {
            buttons = new List<RadioButton>();
        }

        #endregion Public Constructors

        #region Public Methods

        public void Register(RadioButton radioButton)
        {
            buttons.Add(radioButton);
            radioButton.Click += RadioButton_Click;
        }

        public void Unregister(RadioButton radioButton)
        {
            buttons.Remove(radioButton);
            radioButton.Click -= RadioButton_Click;
        }

        #endregion Public Methods

        #region Private Methods

        private void RadioButton_Click(IInteractableUI element)
        {
            foreach (RadioButton button in buttons)
            {
                if (button == element)
                    continue;

                button.CheckState = CheckState.Unchecked;
            }
        }

        #endregion Private Methods
    }
}