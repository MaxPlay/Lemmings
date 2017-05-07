using Lemmings.Levels.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lemmings.Levels
{
    public struct Tile
    {
        private TileCollision collision;

        public TileCollision Collision
        {
            get { return collision; }
            set { collision = value; }
        }

        private int texture;

        public int Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public void GenerateCollision()
        {
            collision.Generate(texture, new Point(0,0), 0.1f);
        }
        
        public Tile(Point offset, int tilesize)
        {
            collision = new TileCollision(offset, tilesize);
            texture = -1;
        }
    }
}
