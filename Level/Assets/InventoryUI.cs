using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform activeItems;
    public GameObject inventoryUI;
    Inventory inventory;

    InventorySlot[] slots;
    InventorySlot[] activeSlots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        activeSlots = activeItems.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf)
                gameManager.instance.cursorLockPause();
            else
                gameManager.instance.cursorUnlockUnpause();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                if (inventory.items[i] != null)
                    slots[i].AddItem(inventory.items[i]);
                else
                    slots[i].ClearSlot();
            }
        }

        for(int i = 0; i < activeSlots.Length; i++)
        {
            if(i < EquipmentManager.instance.currentWeapon.Length)
            {
                if (EquipmentManager.instance.currentWeapon[i] != null)
                    activeSlots[i].AddItem(EquipmentManager.instance.currentWeapon[i]);
                else
                    activeSlots[i].ClearSlot();
            }
        }
    }
}
