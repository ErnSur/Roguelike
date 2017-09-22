using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    //public Button removeButton;
    public Image icon;

    //InspectorBox inspectorBox;

    public GameObject inspectorBoxGO;

    public Item item;

    public void AddItem(Item newItem)
    {
        icon.enabled = true;
        item = newItem;
        icon.sprite = item.icon;
        icon.name = item.name;
    }

    public void ClearSlot()
    {
        icon.enabled = false;
        item = null;
        icon.sprite = null;
        icon.name = "Nothing";
    }

    public void InspectItem()
    {
        if (item != null)
        {
            inspectorBoxGO.SetActive(true);
            inspectorBoxGO.GetComponent<InspectorBox>().itemImage.sprite = item.icon;
            inspectorBoxGO.GetComponent<InspectorBox>().itemName.text = item.name;
            inspectorBoxGO.GetComponent<InspectorBox>().itemDescription.text = item.description;
            print("inspect");
            //print()
        }
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }
}
