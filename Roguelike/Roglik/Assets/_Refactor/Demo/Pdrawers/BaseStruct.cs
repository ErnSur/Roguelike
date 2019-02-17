using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseStruct
{
    public float field;
}


[Serializable]
public class UIElementsDrawerType
{
    public enum IngredientUnit { Spoon, Cup, Bowl, Piece }
    public string name;
    public int amount = 1;
    public IngredientUnit unit;
}