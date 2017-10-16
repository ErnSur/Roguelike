using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour {

    public GameObject inventoryPanel;
    public GameObject inspectorBox; //to remove later
    public Transform itemsParent;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
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
