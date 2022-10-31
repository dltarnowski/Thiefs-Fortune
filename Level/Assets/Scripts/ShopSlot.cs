using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image icon;
    public Button buy;
    Inventory inventory;
    ShopInventory shopInventory;
    public Item item;
    bool canBuy;

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