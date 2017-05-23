using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lemmings.Rendering
{
    interface IRenderDelegator
    {
        void Delegate(IRenderDelegatable element, IDelegateDrawSettings settings);

        void DrawDelegates(SpriteBatch spriteBatch);

        void SetupDelegator();

        void ClearDelegator();
    }
}
