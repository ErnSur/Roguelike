using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Elixir")]
public class Elixir : Item {

    public void OnDrink()
    {
        Debug.Log("drinked");
    }

    public void OnDrop(Vector3 position)
    {
        Debug.Log("dropped");
    }

    public override void OnUseGround(Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
