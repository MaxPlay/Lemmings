using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Lemmings.Extensions;

namespace Lemmings.Levels.Collision
{
    public class QuadTree
    {
        public enum Directions : int
        {
            NW = 0,
            NE = 1,
            SW = 2,
            SE = 3
        }

        class QuadNode
        {
            int value;
            QuadNode[] children;
            private bool lastNode;

            public bool LastNode
            {
                get
                {
                    return lastNode;
                }
            }

            public QuadNode(bool[,] part)
            {
                children = new QuadNode[4];
                int dimension = part.GetLength(0);

                for (int y = 0; y < dimension; y++)
                {
                    for (int x = 0; x < dimension; x++)
                    {
                        if (part[x, y])
                            value++;
                    }
                }

                if (value == part.Length || dimension == 1)
                {
                    lastNode = true;
                    return;
                }

                int halfDimension = dimension / 2;

                children[(int)Directions.NW] = new QuadNode(
                    part.SubArray(0,
                    halfDimension,
                    0,
                    halfDimension));
                children[(int)Directions.NE] = new QuadNode(
                    part.SubArray(halfDimension,
                    halfDimension,
                    0,
                    halfDimension));
                children[(int)Directions.SW] = new QuadNode(
                    part.SubArray(0,
                    halfDimension,
                    halfDimension,
                    halfDimension));
                children[(int)Directions.SE] = new QuadNode(
                    part.SubArray(halfDimension,
                    halfDimension,
                    halfDimension,
                    halfDimension));
            }

            public List<Rectangle> GenerateRectangles(Point offset, int tilesize, int depth)
            {
                List<Rectangle> rects = new List<Rectangle>();

                if (lastNode)
                {
                    if (value > 0)
                        rects.Add(new Rectangle(offset.X, offset.Y, tilesize, tilesize));
                    return rects;
                }

                int nextTilesize = tilesize / 2;
                
                rects.AddRange(children[(int)Directions.NW].GenerateRectangles(new Point(offset.X, offset.Y), nextTilesize, depth + 1));
                rects.AddRange(children[(int)Directions.NE].GenerateRectangles(new Point(offset.X + nextTilesize, offset.Y), nextTilesize, depth + 1));
                rects.AddRange(children[(int)Directions.SW].GenerateRectangles(new Point(offset.X, offset.Y + nextTilesize), nextTilesize, depth + 1));
                rects.AddRange(children[(int)Directions.SE].GenerateRectangles(new Point(offset.X + nextTilesize, offset.Y + nextTilesize), nextTilesize, depth + 1));
                
                Optimize(rects);

                return rects;
            }
            
            private void Optimize(List<Rectangle> rects)
            {
                List<int> replacables = new List<int>();
                List<Rectangle> replacements = new List<Rectangle>();

                for (int i = 0; i < rects.Count; i++)
                {
                    for (int j = 0; j < rects.Count; j++)
                    {
                        if (i == j || replacables.Contains(i) || replacables.Contains(j))
                            continue;

                        if (rects[i].Y == rects[j].Y && rects[i].Height == rects[j].Height &&
                        rects[i].X + rects[i].Width == rects[j].X)
                        {
                            replacements.Add(Rectangle.Union(rects[j], rects[i]));
                            replacables.Add(i);
                            replacables.Add(j);
                        }
                        else if (rects[i].X == rects[j].X && rects[i].Width == rects[j].Width &&
                            rects[i].Y + rects[i].Height == rects[j].Y)
                        {
                            replacements.Add(Rectangle.Union(rects[j], rects[i]));
                            replacables.Add(i);
                            replacables.Add(j);
                        }
                    }
                }

                for (int i = rects.Count - 1; i >= 0; i--)
                {
                    if (replacables.Contains(i))
                        rects.RemoveAt(i);
                }

                rects.AddRange(replacements);
            }
        }

        private Point offset;
        private int tilesize;
        private QuadNode root;

        public QuadTree(Point offset, int tilesize)
        {
            this.offset = offset;
            this.tilesize = tilesize;
        }

        public Rectangle[] Compute(TileCollision value)
        {
            bool[,] spatialRepresentation = GenerateSpatialRepresentation(value);
            root = new QuadNode(spatialRepresentation);
            return root.GenerateRectangles(offset, tilesize, 0).ToArray();
        }

        private bool[,] GenerateSpatialRepresentation(TileCollision value)
        {
            bool[,] representation = new bool[4, 4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    representation[x, y] = value.GetCollision(x + y * 4);
                }
            }
            return representation;
        }
    }
}