using Lemmings.UI.Internal;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.UI
{
    public class Checkbox : ButtonBase
    {
        public Checkbox(UIManager manager, IUIElement parent) : base(manager, parent)
        {
        }

        protected override void DrawButton(SpriteBatch spriteBatch)
        {

        }

        protected override void DrawText(SpriteBatch spriteBatch)
        {
            base.DrawText(spriteBatch);
        }
    }
}