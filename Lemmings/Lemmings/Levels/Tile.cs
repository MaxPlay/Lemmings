using Lemmings.Levels.Collision;
using Microsoft.Xna.Framework;

namespace Lemmings.Levels
{
    public struct Tile
    {
        #region Private Fields

        private TileCollision collision;

        private int texture;

        private Rectangle frame;

        public Rectangle Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        #endregion Private Fields

        #region Public Constructors

        public Tile(Point offset, int tilesize)
        {
            collision = new TileCollision(offset, tilesize);
            texture = -1;
            frame = new Rectangle(0, 0, tilesize, tilesize);
        }

        #endregion Public Constructors

        #region Public Properties

        public TileCollision Collision
        {
            get { return collision; }
            set { collision = value; }
        }

        public int Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public void GenerateCollision()
        {
            collision.Generate(texture, new Point(0, 0), 0.1f);
        }

        #endregion Public Methods
    }
}