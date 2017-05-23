using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Rendering
{
    public interface IRenderDelegatable
    {
        bool DelegationPossible { get; }

        DelegationType Delegation { get; }

        void Draw(SpriteBatch spriteBatch, IDelegateDrawSettings settings);
    }
}
