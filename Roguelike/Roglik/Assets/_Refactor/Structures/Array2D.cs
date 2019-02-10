using System;
using UnityEngine;

namespace LDF.Structures
{
    [Serializable]
    public abstract class Array2D<T>
    {
        [SerializeField]
        private T[] array;
        
        [field: SerializeField]
        public int Width { get; private set; }
        [field: SerializeField]
        public int Height { get; private set; }
        public int Length => array.Length;
    
        protected Array2D(int width, int height)
        {
            Width = width;
            Height = height;
            array = new T[width * height];
        }
    
        public T this[int x, int y]
        {
            get => array[x + y * Width];
            set => array[x + y * Width] = value;
        }
    }

    public static class Array2DExtensions
    {
        public static T Get2D<T>(this T[] array, int x, int y, int arrayWidth) => array[x + y * arrayWidth];
        public static void Set2D<T>(this T[] array, int x, int y, int arrayWidth, T value) => array[x + y * arrayWidth] = value;
    }

}
