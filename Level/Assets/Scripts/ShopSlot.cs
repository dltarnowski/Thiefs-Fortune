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
        if(!canBuy)
        {
            buy.interactable = false;
        }
    }
    public void Buy()
    {
        if (canBuy)
            inventory.Add(item);

        shopInventory.onItemChangedCallback.Invoke();

        gameManager.instance.currencyNumber -= item.buyPrice;
        Debug.Log(item.buyPrice);
        Debug.Log(gameManager.instance.currencyNumber);
        gameManager.instance.playerScript.updatePlayerHUD();
    }
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

        if (CompareTag("ShopBuy"))
        {
            price.text = item.buyPrice.ToString();
            Debug.Log(price.text);
        }
        else if (CompareTag("ShopSell"))
        {
            price.text = item.sellPrice.ToString();
            Debug.Log(price.text);
        }
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
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