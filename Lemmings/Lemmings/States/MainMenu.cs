using Lemmings.Localization;
using Lemmings.Rendering;
using Lemmings.Statemachine;
using Lemmings.UI;
using Lemmings.UI.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.States
{
    public class MainMenu : State
    {
        #region Private Fields

        private Button btnQuit;
        private SlicedSprite buttonSprite;
        private UIManager ui;

        #endregion Private Fields

        #region Public Constructors

        public MainMenu(string name, StateMachine statemachine) : base(name, statemachine)
        {
            ui = new UIManager();
            Localizer.CultureChanged += Localizer_CultureChanged;
            buttonSprite = new SlicedSprite("button_texture", "Textures/UI/Button");
            buttonSprite.Top = 18;
            buttonSprite.Left = 18;
            buttonSprite.Right = 46;
            buttonSprite.Bottom = 46;

            Assetmanager.AquireFont("Fonts/default");
            btnQuit = new Button(ui, ui);
            btnQuit.Sprite = buttonSprite;
            btnQuit.Font = "Fonts/default";
            btnQuit.Position = new Vector2(Game1.Graphics.PreferredBackBufferWidth / 2 - 100, 400);
            btnQuit.Dimension = new Point(200, 100);
            btnQuit.Click += BtnQuit_Click;

            RefreshLocalization();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            ui.Draw(spriteBatch);
        }

        public override void Initialize()
        {
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            ui.Update(gameTime);
        }

        #endregion Public Methods

        #region Private Methods

        private void BtnQuit_Click(IInteractableUI element)
        {
            statemachine.ExitGame();
        }

        private void Localizer_CultureChanged(string culture)
        {
            RefreshLocalization();
        }

        private void RefreshLocalization()
        {
            btnQuit.Text = Localizer.GetString("quit");
        }

        #endregion Private Methods
    }
}