using Lemmings.Levels;
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

        private Button btnExit;
        private Slider slider;
        private SlicedSprite sprite;
        private Tile t;
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

            int texture = Assetmanager.AquireTexture("test");

            t = new Tile(new Point(0, 0), 32);
            t.Texture = texture;

            t.GenerateCollision();
            Debug.Log(t.Collision.ToString2D());

             sprite = new SlicedSprite("slice-test", "slice-test");
            sprite.Top = 20;
            sprite.Left = 20;
            sprite.Right = 80;
            sprite.Bottom = 80;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            //  ui.Draw(spriteBatch);
            spriteBatch.Begin();
            sprite.Draw(spriteBatch, new Rectangle(100,100,300,200));
            spriteBatch.End();
            for (int i = 0; i < t.Collision.Bounds.Length; i++)
            {
                Debug.DrawRectangle(t.Collision.Bounds[i], new Color(i / (float)t.Collision.Bounds.Length, 0.5f, 1-i / (float)t.Collision.Bounds.Length));
            }
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