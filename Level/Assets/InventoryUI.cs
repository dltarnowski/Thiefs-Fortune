using Unity.VisualScripting;
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

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && TutorialManager.instance.tutorialProgress >= 2)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);

            if (TutorialManager.instance.basicMoveTrigger || TutorialManager.instance.advanceMoveTrigger || TutorialManager.instance.inventoryTrigger || TutorialManager.instance.meleeTrigger && inventoryUI.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                gameManager.instance.cameraScript.enabled = false;
            }
            else
            {
                if (inventoryUI.activeSelf)
                    gameManager.instance.cursorLockPause();
                else
                    gameManager.instance.cursorUnlockUnpause();
            }
        }
    }

    void UpdateUI()
    {

        for (int i = 0; i < slots.Length; i++)
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

        for(int i = 0; i < activeSlots.Length; i++)
        {
            if(i < EquipmentManager.instance.currentEquipment.Length && EquipmentManager.instance.currentEquipment[i] != null)
            {
                activeSlots[i].AddItem(EquipmentManager.instance.currentEquipment[i]);
            }
            else
                activeSlots[i].ClearSlot();
        }
    }
}
