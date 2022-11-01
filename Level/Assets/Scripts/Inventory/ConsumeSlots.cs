using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ConsumeSlots : MonoBehaviour
{
    Inventory inventory;
    ConsumableInventory shopInventory;

    public Image icon;
    public Button buy;
    public Consumable item;
    public TextMeshProUGUI price;

    bool canBuy;

    private void Start()
    {
        inventory = Inventory.instance;
        shopInventory = ConsumableInventory.instance;

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
            if(inventory.items.Contains(item))
            {
                item.numOfItems++;
            }
            else
            {
                inventory.Add(item);
            }

        }
        gameManager.instance.currencyNumber -= item.buyPrice;
        gameManager.instance.playerScript.updatePlayerHUD();
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