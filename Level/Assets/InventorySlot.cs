using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Button equipButton;
    public Button unEquipButton;
    public Inventory inventory;
    public TextMeshProUGUI itemCount;
    public GameObject countUI;
    Item item;

    int ammoAmount;
    int hpAmount;

    private void Start()
    {
        inventory = Inventory.instance;
        /*if (CompareTag("Shop"))
            AddItem(item);*/
    }

    // NEEDS TO BE REPLACED FOR FINAL!!! THIS IS JUST FOR TESTING PURPOSES
    private void Update()
    {
        Inventory.instance.onItemChangedCallback.Invoke();
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

        if(item is Consumable)
        {
            countUI.SetActive(true);
            itemCount.text = item.numOfItems.ToString();
        }

    }

    public void ClearSlot()
    {
        if (item is Consumable)
        {
            countUI.SetActive(false);
            itemCount.text = " ";
        }
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
