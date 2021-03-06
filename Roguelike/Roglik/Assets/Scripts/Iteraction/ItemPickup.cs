﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

	private SpriteRenderer spriteRenderer;
    public Item item;

    public override void Interact()
    {

        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        //Vector3 worldpos = transform.TransformVector(transform.position);
        //Debug.Log("x: "+ worldpos.x + "y: " + worldpos.y);
        //Debug.Log("Picking up " + item.name);
        if (Inventory.instance.Add(item))
        {
        Destroy(gameObject);
        }
    }

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = item.icon;
	}
}
