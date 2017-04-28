using Lemmings.Localization;
using Lemmings.Statemachine;
using Lemmings.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Private Fields

        private static ContentManager contentManager;
        private static Texture2D pixel;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private StateMachine statemachine;

        public static string ApplicationDirectory { get { return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); } }

        #endregion Private Fields

        #region Public Constructors

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;
        }

        #endregion Public Constructors

        #region Public Properties

        public static ContentManager ContentManager
        {
            get { return contentManager; }
        }
        public static Texture2D Pixel { get { return pixel; } }

        #endregion Public Properties

        #region Protected Methods

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            statemachine.Draw(spriteBatch);
            Debug.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Localizer.LoadLocalization();
            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new Color[] { Color.White });

            statemachine = new StateMachine();
            new SplashScreen("splash", statemachine);
            new MainMenu("mainmenu", statemachine);
            new Gameplay("gameplay", statemachine);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            statemachine.Update(gameTime);

            if (statemachine.Exiting)
                Exit();
            base.Update(gameTime);
        }

        #endregion Protected Methods
    }
}