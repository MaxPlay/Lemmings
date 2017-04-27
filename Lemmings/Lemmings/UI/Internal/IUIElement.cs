using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.UI.Internal
{
    public interface IUIElement
    {
        #region Public Properties

        IUIElement[] Children { get; }
        bool Enabled { get; set; }
        Vector2 LocalPosition { get; set; }
        float LocalRotation { get; set; }
        Vector2 LocalScale { get; set; }
        Vector2 LossyScale { get; }
        IUIElement Parent { get; set; }
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        bool Visible { get; set; }

        #endregion Public Properties

        #region Public Methods

        void AddChild(IUIElement child);

        void Draw(SpriteBatch spriteBatch);

        void RemoveChild(IUIElement child);

        void Update(GameTime gameTime);

        #endregion Public Methods
    }
}