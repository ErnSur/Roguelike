using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    private Vector2 originalItemPos;
    private Transform originalItemParent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
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
        //Debug.Log("end");
        this.transform.SetParent(originalItemParent);
        this.transform.position = originalItemPos;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
