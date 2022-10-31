using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    public Image icon;
    public Button buy;
    Inventory inventory;
    ShopInventory shopInventory;
    public Item item;
    bool canBuy;
    public TextMeshProUGUI price;

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
            inventory.Add(item);
            shopInventory.Remove(item);
        }

        gameManager.instance.currencyNumber -= item.buyPrice;
        gameManager.instance.playerScript.updatePlayerHUD();
    }

    public void Sell()
    {
        if(item != null)
        {
            gameManager.instance.currencyNumber += item.sellPrice;
            gameManager.instance.playerScript.updatePlayerHUD();

            inventory.Remove(item);
            //Inventory.instance.onItemChangedCallback.Invoke();

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
        if (item != null)
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
}