using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Rendering
{
    public struct RenderDelegation
    {
        public IRenderDelegatable Delegatable;
        public IDelegateDrawSettings Settings;
        public RenderDelegation(IRenderDelegatable delegatable, IDelegateDrawSettings settings)
        {
            Delegatable = delegatable;
            Settings = settings;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Delegatable.Draw(spriteBatch, Settings);
        }
    }
}
