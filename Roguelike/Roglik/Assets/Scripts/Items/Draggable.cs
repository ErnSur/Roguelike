using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    private Vector2 originalItemPos;
    private Transform originalItemParent;

    //droping
    Inventory inventory;

    public void OnBeginDrag(PointerEventData eventData)
    {
        item = GetComponent<InventorySlot>().item;
        originalItemPos = this.transform.position;
        originalItemParent = this.transform.parent;

        this.transform.SetParent(this.transform.parent.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dropPos = PFgrid.ScreenToGridCell(eventData.position);
        Vector3 dropDirection = dropPos - PlayerMovement.PlayerPos3;
        float dropDistance = Vector3.Distance(dropPos, PlayerMovement.PlayerPos3);

        //using item
        if (dropPos == PlayerMovement.PlayerPos3)
        {
            Debug.Log("consumed " + item.name);
            item.OnUsePlayer();
            inventory.Remove(item);
        }
        else if(!PlayerMovement.RayWallUpdate(dropDirection, dropDistance)) // raycast to see if it does not collide with wall
        {
            item.OnUseGround(dropPos);
            inventory.Remove(item);
        }

        this.transform.SetParent(originalItemParent);
        this.transform.position = originalItemPos;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        item = null;
    }
    void Start()
    {
        inventory = Inventory.instance;
    }
}
