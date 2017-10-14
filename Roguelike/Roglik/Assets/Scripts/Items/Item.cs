using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Name";
    public Sprite icon = null;
    public string description = null;
    public Transform prefab; // prefab to use when item drops

    public virtual void OnUsePlayer()
    {
        //Debug.Log(name + " used on player");
    }

    public virtual void OnUseGround(Vector3 groundPos)
    {
        //Debug.Log(name + " used on ground");
        Instantiate(prefab, groundPos, Quaternion.identity);
    }
}
