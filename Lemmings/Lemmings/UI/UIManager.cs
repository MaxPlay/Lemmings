using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Lemmings.UI
{
    public class UIManager : IUIElement
    {
        #region Private Fields

        private List<IUIElement> children;

        #endregion Private Fields

        #region Public Constructors

        public UIManager()
        {
            children = new List<IUIElement>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IUIElement[] Children
        {
            get
            {
                return children.ToArray();
            }
        }

        public bool Enabled
        {
            get
            {
                return true;
            }

            set
            {
            }
        }

        public Vector2 LocalPosition
        {
            get
            {
                return Vector2.Zero;
            }

            set
            {
            }
        }

        public float LocalRotation
        {
            get
            {
                return 0f;
            }

            set
            {
            }
        }

        public Vector2 LocalScale
        {
            get
            {
                return Vector2.One;
            }

            set
            {
            }
        }

        public Vector2 LossyScale
        {
            get
            {
                return Vector2.One;
            }
        }

        public IUIElement Parent
        {
            get
            {
                return null;
            }

            set
            {
            }
        }

        public Vector2 Position
        {
            get
            {
                return Vector2.Zero;
            }

            set
            {
            }
        }

        public float Rotation
        {
            get
            {
                return 0f;
            }

            set
            {
            }
        }

        public bool Visible
        {
            get
            {
                return true;
            }

            set
            {
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void AddChild(IUIElement child)
        {
            if (children.Contains(child))
                return;

            children.Add(child);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (IUIElement child in children)
            {
                child.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void RemoveChild(IUIElement child)
        {
            if (children.Contains(child))
                return;

            children.Add(child);
        }

        public void Update(GameTime gameTime)
        {
            foreach (IUIElement child in children)
            {
                child.Update(gameTime);
            }
        }

        #endregion Public Methods
    }
}