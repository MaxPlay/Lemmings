using Lemmings.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lemmings.Levels
{
    public class Layer
    {
        protected TileMap tiles;

        public TileMap Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        protected Rectangle bounds;

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        protected Vector2 parallaxOffset;

        public Layer()
        {
            tiles = new TileMap();
        }

        public void GenerateParallax(GameLayer game)
        {
            parallaxOffset = new Vector2(game.bounds.Width - bounds.Width, game.bounds.Height - bounds.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
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
    }
}
