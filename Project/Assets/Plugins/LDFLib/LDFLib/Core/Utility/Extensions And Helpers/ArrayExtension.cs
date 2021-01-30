using System;

namespace LDF.Utility
{
    public static class ArrayExtensions
    {
        public static T[] FlippedVertically<T>(this T[] flipTarget, int width, int height)
        {
            T[] flippedArray = new T[flipTarget.Length];

            for (int i = 0; i < height; ++i)
            {
                int sourceIndex = (height - i - 1) * width;
                int destinationIndex = i * height;

                Array.Copy(flipTarget, sourceIndex, flippedArray, destinationIndex, width);
            }

            return flippedArray;
        }
    }
}
