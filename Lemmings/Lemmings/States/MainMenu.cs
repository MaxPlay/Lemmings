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

        private Button btnExit;
        private Slider slider;
        private UIManager ui;

        #endregion Private Fields

        #region Public Constructors

        public MainMenu(string name, StateMachine statemachine) : base(name, statemachine)
        {
            ui = new UIManager();
            btnExit = new Button(ui, ui);
            btnExit.Font = "Fonts/default";
            btnExit.LocalizationKey = "language";
            btnExit.Focus += (IInteractableUI i) => { Localization.Localizer.ChangeCulture("de-DE"); };
            btnExit.Dimension = new Point(100, 200);
            slider = new Slider(ui, ui);
            slider.Position = new Vector2(300, 300);
            slider.Texture = Assetmanager.AquireTexture("Textures/UI/Slider");
            slider.SetHandleTexture(Assetmanager.AquireTexture("Textures/UI/Handle"));
            slider.RecalculateBounds();
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
    }
}