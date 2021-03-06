﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour {

    public GameObject inventoryPanel;
    public GameObject inspectorBox; //to remove later
    public Transform itemsParent;
	public Image weaponImage;
    Inventory inventory;
    InventorySlot[] slots;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory"))
        {
			ToggleInventory();
        }
		if (Input.GetButtonDown("Cancel"))
		{
			inventoryPanel.SetActive(false);
        	inspectorBox.SetActive(false);
		}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("0Labs");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("1Labs");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Game");
        }

		if ( PlayerStats.instance.weapon != null)
		{
			weaponImage.sprite = PlayerStats.instance.weapon.icon;
		}
	}

	public void ToggleInventory()
	{
		inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        inspectorBox.SetActive(false);
	}

    void UpdateUI()
    {
        //Debug.Log("updateUI");
		if ( PlayerStats.instance.weapon != null)
		{
			weaponImage.sprite = PlayerStats.instance.weapon.icon;
		}

        for (int i =0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
