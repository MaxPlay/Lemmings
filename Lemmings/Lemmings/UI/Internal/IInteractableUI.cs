using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lemmings.UI.Internal
{
    public delegate void UIEventHandler(IInteractableUI element);

    public interface IInteractableUI
    {
        event UIEventHandler Click;

        event UIEventHandler Focus;

        event UIEventHandler LostFocus;
    }
}
