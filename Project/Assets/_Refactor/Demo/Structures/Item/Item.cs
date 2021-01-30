using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDF.Roglik.Items
{
    public class Item : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Image;
    }

    [CreateAssetMenu(menuName = "Items_R/Food")]
    public class Food : Item
    {
        public void Use()
        {

        }
    }
}