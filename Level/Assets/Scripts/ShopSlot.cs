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
        inventory = Inventory.instance;
        shopInventory = ShopInventory.instance;

    }
    private void Update()
    {
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
        if (item != EquipmentManager.instance.currentEquipment[0] && item != EquipmentManager.instance.currentEquipment[1]
           && gameManager.instance.currencyNumber >= item.buyPrice && !inventory.items.Contains(item))
        {
            canBuy = true;
        }
        else
            canBuy = false;
    }
}