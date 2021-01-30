using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    #region Singleton
    public static Inventory instance;
    void Awake()
    {
        if(instance != null)
        {
            //Debug.Log("More than one instance of inventory found!");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public List<Item> items = new List<Item>();


    public bool Add (Item item)
    {
        if(items.Count >= space)
        {
            //Debug.Log("Inventory is Full!");
            return false;
        }

        items.Add(item); //Add is a List method

        if(onItemChangedCallback != null) { onItemChangedCallback.Invoke(); }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null) { onItemChangedCallback.Invoke(); }
    }
}
