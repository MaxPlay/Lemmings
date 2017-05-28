using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lemmings.Levels
{
    public class TileMap
    {
        #region Private Fields

        private int height;

        private Tile[] tiles;
        private Point tileSize;
        private int width;

        #endregion Private Fields

        #region Public Properties

        public int Height
        {
            get { return height; }
        }

        public Tile[] Tiles
        {
            get { return tiles; }
        }

        public Point TileSize
        {
            get { return tileSize; }
        }

        public int Width
        {
            get { return width; }
        }

        #endregion Public Properties

        #region Public Indexers

        public Tile this[int x, int y]
        {
            get { return GetTile(x, y); }
        }

        #endregion Public Indexers

        #region Public Methods

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tile t = tiles[y * width + x];
                    spriteBatch.Draw(Assetmanager.GetTexture(t.Texture), new Rectangle(x * tileSize.X + (int)offset.X, y * tileSize.Y + (int)offset.Y, tileSize.X, tileSize.Y), t.Frame, Color.White);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Tile t = tiles[y * width + x];
                    spriteBatch.Draw(Assetmanager.GetTexture(t.Texture), new Rectangle(x * tileSize.X, y * tileSize.Y, tileSize.X, tileSize.Y), t.Frame, Color.White);
                }
            }
        }

        public Tile GetTile(int x, int y)
        {
            return tiles[y * width + x];
        }

        #endregion Public Methods
    }
}