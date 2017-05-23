using Lemmings.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Levels
{
    public class Layer
    {
        #region Protected Fields

        protected Rectangle bounds;
        protected Vector2 parallaxOffset;
        protected TileMap tiles;

        #endregion Protected Fields

        #region Public Constructors

        public Layer()
        {
            tiles = new TileMap();
        }

        #endregion Public Constructors

        #region Public Properties

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public TileMap Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Add difference to tiles in the draw statement
            Vector2 difference = new Vector2(Camera.Main.Position.X / (bounds.Width + parallaxOffset.X), Camera.Main.Position.Y / (bounds.Height + parallaxOffset.Y)) * parallaxOffset;
            for (int y = 0; y < tiles.Height; y++)
            {
                for (int x = 0; x < tiles.Width; x++)
                {
                    
                }
            }
        }

        public void GenerateParallax(GameLayer game)
        {
            parallaxOffset = new Vector2(game.bounds.Width - bounds.Width, game.bounds.Height - bounds.Height);
        }

        #endregion Public Methods
    }
}