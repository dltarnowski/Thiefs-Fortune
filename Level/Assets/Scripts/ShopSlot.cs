using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    Inventory inventory;
    ShopInventory shopInventory;

    public Image icon;
    public Button buy;
    public Item item;
    public TextMeshProUGUI price;
    public GameObject countUI;

    bool canBuy;

    private void Start()
    {
        item = Instantiate(item);
        inventory = Inventory.instance;
        shopInventory = ShopInventory.instance;

    }
    private void Update()
    {
        if(ShopMenuButtons.instance.buyInventory.activeSelf)
            BuyCheck();

        if(!canBuy && CompareTag("ShopBuy"))
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
            gameManager.instance.currencyNumber -= item.buyPrice;
            //shopInventory.Remove(item);
            inventory.Add(item);
        }
        gameManager.instance.playerScript.updatePlayerHUD();
    }

    public void Sell()
    {
        if(item != null)
        {
            gameManager.instance.currencyNumber += item.sellPrice;
            gameManager.instance.playerScript.updatePlayerHUD();

            inventory.Remove(item);

            Inventory.instance.onItemChangedCallback.Invoke();
        }

    }
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

        if (CompareTag("ShopBuy"))
        {
            price.text = item.buyPrice.ToString();
        }
        else if (CompareTag("ShopSell"))
        {
            price.text = item.sellPrice.ToString();
        }
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        if (CompareTag("ShopSell"))
        {
            price.text = " ";
        }
    }

    public void BuyCheck()
    {
        bool containsItem = false;
        for (int i = 0; i < inventory.items.Count - 1; i++)
        {
            if (inventory.items[i].name == item.name)
                containsItem = true;
        }

        if (item.name != EquipmentManager.instance.currentEquipment[0].name && item.name != EquipmentManager.instance.currentEquipment[1].name
           && gameManager.instance.currencyNumber >= item.buyPrice && !containsItem)
        {
            canBuy = true;
        }
        else
            canBuy = false;

    }
}