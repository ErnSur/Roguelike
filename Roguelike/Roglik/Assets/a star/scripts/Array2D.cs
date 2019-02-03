using System;
using UnityEngine;

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