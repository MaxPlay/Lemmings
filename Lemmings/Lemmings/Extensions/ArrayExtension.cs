﻿using System;

namespace Lemmings.Extensions
{
    public static class ArrayExtension
    {
        #region Public Methods

        public static float Median(this float[,] data)
        {
            int width = data.GetLength(0);
            int height = data.GetLength(1);
            if (height == 0 || width == 0)
                return 0;

            float value = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    value += data[x, y];
                }
            }

            return value / (height * width);
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static T[,] SubArray<T>(this T[,] data, int index0, int length0, int index1, int length1)
        {
            T[,] result = new T[length0, length1];
            int yEnd = index1 + length1;
            int xEnd = index0 + length0;

            for (int y = index1; y < yEnd; y++)
            {
                for (int x = index0; x < xEnd; x++)
                {
                    result[x - index0, y - index1] = data[x, y];
                }
            }

            return result;
        }

        #endregion Public Methods
    }
}