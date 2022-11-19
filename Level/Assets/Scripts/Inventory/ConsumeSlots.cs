using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsumeSlots : MonoBehaviour
{
    Inventory inventory;
    ConsumableInventory consumeInventory;


    public Image icon;
    public Button buy;
    public Consumable item;
    public TextMeshProUGUI price;

    bool canBuy;
    bool containsItem;

    private void Start()
    {
        inventory = Inventory.instance;
        consumeInventory = ConsumableInventory.instance;
        item = Instantiate(item);
    }
    private void Update()
    {
        BuyCheck();

        if (!canBuy)
        {
            buy.interactable = false;
        }
        else
            buy.interactable = true;
    }
    public void Buy()
    {
        if (canBuy)
        {
            if (item is Consumable)
            {
                for (int i = 0; i < Inventory.instance.items.Count; i++)
                {
                    if (Inventory.instance.items[i].name == item.name)
                    {
                        Inventory.instance.items[i].numOfItems++;
                        containsItem = true;
                    }
                }
                if (EquipmentManager.instance.currentEquipment[2] != null && item.name == "Ammo")
                    EquipmentManager.instance.currentEquipment[2].numOfItems++;
                else if (EquipmentManager.instance.currentEquipment[3] != null && item.name == "HealthPotion")
                    EquipmentManager.instance.currentEquipment[3].numOfItems++;
                else if (!containsItem)
                    Inventory.instance.Add(item);
            }

        }
        gameManager.instance.currencyNumber -= item.buyPrice;
        gameManager.instance.playerScript.updatePlayerHUD();
        Inventory.instance.onItemChangedCallback.Invoke();
    }

    public void AddItem(Consumable newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

        price.text = item.buyPrice.ToString();

    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void BuyCheck()
    {
        if (gameManager.instance.currencyNumber >= item.buyPrice)
        {
            canBuy = true;
        }
        else
            canBuy = false;
    }
}