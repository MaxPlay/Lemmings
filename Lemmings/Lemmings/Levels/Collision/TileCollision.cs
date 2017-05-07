using Lemmings.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lemmings.Levels.Collision
{
    public struct TileCollision : IEquatable<uint>, IComparable<uint>
    {
        #region Public Fields

        public const int FIRST_COLUMN = 34952;
        public const int FIRST_ROW = 61440;
        public const int FOURTH_COLUMN = 4369;
        public const int FOURTH_ROW = 15;
        public const int SECOND_COLUMN = 17476;
        public const int SECOND_ROW = 3840;
        public const int THIRD_COLUMN = 8738;
        public const int THIRD_ROW = 240;
        public static readonly int[] Parts = new int[] { 32768, 16384, 8192, 4096, 2048, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1 };

        #endregion Public Fields

        #region Private Fields

        private static int quarterTileSize;

        private static int tilesize;

        private Rectangle[] collisionRepresentation;

        private Point offset;

        private int value;

        #endregion Private Fields

        #region Public Constructors

        public TileCollision(Point position, int tilesize)
        {
            value = 0;
            collisionRepresentation = new Rectangle[0];
            offset = new Point(position.X * tilesize, position.Y * tilesize);

            if (TileCollision.tilesize == tilesize)
                return;

            TileCollision.tilesize = tilesize;
            quarterTileSize = tilesize / 4;
        }

        #endregion Public Constructors

        #region Public Properties

        public Rectangle[] Bounds { get { return collisionRepresentation; } }

        #endregion Public Properties

        #region Public Methods

        public int CompareTo(uint other)
        {
            return value.CompareTo(other);
        }

        public void Destroy(int id)
        {
            if ((value & Parts[id]) == Parts[id])
            {
                value = Parts[id] | value;
                Recalculate();
            }
        }

        public void DestroyColumn(int id)
        {
            switch (id)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    value = Parts[id] | value;
                    value = Parts[4 + id] | value;
                    value = Parts[8 + id] | value;
                    value = Parts[12 + id] | value;
                    Recalculate();
                    break;
            }
        }

        public void DestroyRow(int id)
        {
            switch (id)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    int v = 4 * id;
                    value = Parts[v] | value;
                    value = Parts[v + 1] | value;
                    value = Parts[v + 2] | value;
                    value = Parts[v + 3] | value;
                    Recalculate();
                    break;
            }
        }

        public bool Equals(uint other)
        {
            return value == other;
        }

        public void Generate(int textureID, Point frame, float threshold)
        {
            threshold = MathHelper.Clamp(threshold, 0, 1);

            Texture2D texture = Assetmanager.GetTexture(textureID);
            Color[] data = new Color[tilesize * tilesize];
            texture.GetData(0, new Rectangle(frame.X, frame.Y, tilesize, tilesize), data, 0, data.Length);
            float[,] translation = new float[tilesize, tilesize];
            for (int y = 0; y < tilesize; y++)
            {
                for (int x = 0; x < tilesize; x++)
                {
                    translation[x, y] = data[x + y * tilesize].A / 255f;
                }
            }

            bool[] collides = new bool[16];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    collides[x + y * 4] = translation.SubArray(x * quarterTileSize, quarterTileSize, y * quarterTileSize, quarterTileSize).Median() > threshold;
                }
            }

            for (int i = 0; i < collides.Length; i++)
            {
                if (collides[i])
                    value = value | Parts[i];
            }
            Recalculate();
        }

        public bool GetCollision(int id)
        {
            return (value & Parts[id]) == Parts[id];
        }

        public override string ToString()
        {
            return string.Format("[{0}{1}{2}{3}][{4}{5}{6}{7}][{8}{9}{10}{11}][{12}{13}{14}{15}]",
                GetCollision(0),
                GetCollision(1),
                GetCollision(2),
                GetCollision(3),
                GetCollision(4),
                GetCollision(5),
                GetCollision(6),
                GetCollision(7),
                GetCollision(8),
                GetCollision(9),
                GetCollision(10),
                GetCollision(11),
                GetCollision(12),
                GetCollision(13),
                GetCollision(14),
                GetCollision(15));
        }
        public string ToString2D()
        {
            return string.Format("[{0}{1}{2}{3}]\n[{4}{5}{6}{7}]\n[{8}{9}{10}{11}]\n[{12}{13}{14}{15}]",
                GetCollision(0),
                GetCollision(1),
                GetCollision(2),
                GetCollision(3),
                GetCollision(4),
                GetCollision(5),
                GetCollision(6),
                GetCollision(7),
                GetCollision(8),
                GetCollision(9),
                GetCollision(10),
                GetCollision(11),
                GetCollision(12),
                GetCollision(13),
                GetCollision(14),
                GetCollision(15));
        }

        #endregion Public Methods

        #region Private Methods

        private void Recalculate()
        {
            if (value == 0)
            {
                collisionRepresentation = new Rectangle[0];
                return;
            }

            QuadTree quadTree = new QuadTree(offset, tilesize);
            collisionRepresentation = quadTree.Compute(this);
        }

        #endregion Private Methods
    }
}