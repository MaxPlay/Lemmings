namespace Lemmings.UI.Internal
{
    public delegate void UIEventHandler(IInteractableUI element);

    public interface IInteractableUI
    {
        #region Public Events

        event UIEventHandler Click;

        event UIEventHandler Focus;

        event UIEventHandler LostFocus;

        #endregion Public Events
    }
}