using Lemmings.Rendering.Delegation;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Rendering
{
    public class DelegatableFont : IRenderDelegatable
    {
        #region Private Fields

        private DelegationType delegation;
        private string font;

        #endregion Private Fields

        #region Public Properties

        public DelegationType Delegation
        {
            get
            {
                return delegation;
            }
            set
            {
                delegation = value;
            }
        }

        public bool DelegationPossible
        {
            get
            {
                return true;
            }
        }

        public string FontName
        {
            get { return font; }
            set { font = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch, IDelegateDrawSettings settings)
        {
            if (!(settings is FontSettings))
                return;

            FontSettings f = (FontSettings)settings;

            spriteBatch.DrawString(Assetmanager.GetFont(font), f.Text, f.Position, f.Color);
        }

        #endregion Public Methods
    }
}