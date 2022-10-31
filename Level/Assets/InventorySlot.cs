using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Button equipButton;
    public Button unEquipButton;
    public Inventory inventory;
    Item item;

    private void Start()
    {
        inventory = Inventory.instance;
        /*if (CompareTag("Shop"))
            AddItem(item);*/
    }
    public void Buy()
    {
        if (item != EquipmentManager.instance.currentEquipment[0] && item != EquipmentManager.instance.currentEquipment[1])
        {
            inventory.Add(item);
        }
    }
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        
        if(CompareTag("Inventory Slot"))
        {
            equipButton.interactable = true;
            removeButton.interactable = true;
        }
        if (CompareTag("Equipment Slot"))
            unEquipButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        if (CompareTag("Inventory Slot"))
        {
            equipButton.interactable = false;
            removeButton.interactable = false;
        }
        if (CompareTag("Equipment Slot"))
            unEquipButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Instantiate(item.model, new Vector3(gameManager.instance.playerScript.itemDropPoint.transform.position.x, 
            gameManager.instance.playerScript.itemDropPoint.transform.position.y, 
            gameManager.instance.playerScript.itemDropPoint.transform.position.z), gameManager.instance.playerScript.itemDropPoint.transform.rotation);

        if (item.numOfItems == 0)
            Inventory.instance.Remove(item);
        else
            item.numOfItems--;

    }

    public void EquipItem()
    {
        if(item != null)
        {
            if (CompareTag("Inventory Slot"))
            {
                item.Equip();
                ClearSlot();
            }
            else
            {
                item.unUse();
                ClearSlot();
            }
            Inventory.instance.onItemChangedCallback.Invoke();
        }
    }
}
