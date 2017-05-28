using Lemmings.Rendering.Delegation;
using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Lemmings.UI
{
    public class UIManager : IUIElement, IRenderDelegator
    {
        #region Private Fields

        private List<IUIElement> children;

        private Dictionary<DelegationType, List<RenderDelegation>> delegates;

        #endregion Private Fields

        #region Public Constructors

        public UIManager()
        {
            children = new List<IUIElement>();
            SetupDelegator();
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

        public void ClearDelegator()
        {
            DelegationType[] t = Enum.GetValues(typeof(DelegationType)) as DelegationType[];
            for (int i = 0; i < t.Length; i++)
            {
                delegates[t[i]].Clear();
            }
        }

        public void Delegate(IRenderDelegatable element, IDelegateDrawSettings settings)
        {
            if (element.DelegationPossible)
                delegates[element.Delegation].Add(new RenderDelegation(element, settings));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (IUIElement child in children)
            {
                child.Draw(spriteBatch);
            }
            spriteBatch.End();
            DrawDelegates(spriteBatch);
        }

        public void DrawDelegates(SpriteBatch spriteBatch)
        {
            if (delegates[DelegationType.SamplerWrap].Count > 0)
            {
                DelegateDraw(spriteBatch, DelegationType.SamplerWrap, BlendState.AlphaBlend, SamplerState.PointWrap, null, null);
            }
            if (delegates[DelegationType.Additive].Count > 0)
            {
                DelegateDraw(spriteBatch, DelegationType.Additive, BlendState.Additive, null, null, null);
            }
            if (delegates[DelegationType.NoRasterizer].Count > 0)
            {
                DelegateDraw(spriteBatch, DelegationType.NoRasterizer, BlendState.AlphaBlend, null, null, RasterizerState.CullNone);
            }
            if (delegates[DelegationType.StencilState].Count > 0)
            {
                DelegateDraw(spriteBatch, DelegationType.StencilState, BlendState.AlphaBlend, null, DepthStencilState.DepthRead, null);
            }

            ClearDelegator();
        }

        public void RemoveChild(IUIElement child)
        {
            if (children.Contains(child))
                return;

            children.Add(child);
        }

        public void SetupDelegator()
        {
            delegates = new Dictionary<DelegationType, List<RenderDelegation>>();
            DelegationType[] t = Enum.GetValues(typeof(DelegationType)) as DelegationType[];
            for (int i = 0; i < t.Length; i++)
            {
                delegates.Add(t[i], new List<RenderDelegation>());
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (IUIElement child in children)
            {
                child.Update(gameTime);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void DelegateDraw(SpriteBatch spriteBatch, DelegationType delegation, BlendState blendState, SamplerState sampler, DepthStencilState depth, RasterizerState rasterizer)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, blendState, sampler, depth, rasterizer);
            for (int i = 0; i < delegates[delegation].Count; i++)
            {
                delegates[delegation][i].Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        #endregion Private Methods
    }
}