using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Lemmings.UI
{
    public abstract class UIElement : IUIElement
    {
        #region Protected Fields

        protected List<IUIElement> children;
        protected bool enabled;
        protected IUIElement parent;
        protected Vector2 position;
        protected float rotation;
        protected Vector2 scale;
        protected bool visible;

        #endregion Protected Fields

        #region Private Fields

        private IUIElement root;

        #endregion Private Fields

        #region Public Constructors

        public UIElement(UIManager manager)
        {
            children = new List<IUIElement>();
            root = manager;
        }
        public UIElement(UIManager manager, IUIElement parent)
        {
            children = new List<IUIElement>();
            root = parent ?? manager;
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
                return enabled;
            }

            set
            {
                enabled = value;
            }
        }

        public Vector2 LocalPosition
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public float LocalRotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }

        public Vector2 LocalScale
        {
            get
            {
                return scale;
            }

            set
            {
                scale = value;
            }
        }

        public Vector2 LossyScale
        {
            get
            {
                if (parent != null)
                    return scale * parent.LossyScale;
                return scale;
            }
        }

        public IUIElement Parent
        {
            get
            {
                return parent;
            }

            set
            {
                if (value == parent)
                    return;

                parent.RemoveChild(this);
                parent = value;
                parent.AddChild(this);
            }
        }

        public Vector2 Position
        {
            get
            {
                if (parent != null)
                    return position + parent.Position;
                return position;
            }

            set
            {
                if (parent != null)
                    position = value - parent.Position;
                position = value;
            }
        }

        public float Rotation
        {
            get
            {
                if (parent != null)
                    return rotation + parent.Rotation;
                return rotation;
            }

            set
            {
                if (parent != null)
                    rotation = value - parent.Rotation;
                rotation = value;
            }
        }

        public bool Visible
        {
            get
            {
                return visible && (parent?.Visible == true);
            }

            set
            {
                visible = value;
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (IUIElement child in children)
            {
                child.Draw(spriteBatch);
            }
        }

        public void RemoveChild(IUIElement child)
        {
            if (children.Contains(child))
                return;

            children.Add(child);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IUIElement child in children)
            {
                child.Update(gameTime);
            }
        }

        #endregion Public Methods
    }
}