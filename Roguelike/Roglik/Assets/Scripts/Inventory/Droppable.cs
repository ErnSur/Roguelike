using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Droppable : MonoBehaviour, IDropHandler {

    public bool consumeItem;
    public List<Item> accetableItems;

    public Item item;
    Inventory inventory;
    Prisoner001 me;

    void Start()
    {
        inventory = Inventory.instance;
        me = gameObject.GetComponent<Prisoner001>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was Dropped on " + gameObject.name);

        if(consumeItem && accetableItems.Contains(eventData.pointerDrag.GetComponent<InventorySlot>().item) ){

            item = eventData.pointerDrag.GetComponent<InventorySlot>().item;
            me.holdItem = item;
            me.UpdateState();
            inventory.Remove(item);
        }
    }
}