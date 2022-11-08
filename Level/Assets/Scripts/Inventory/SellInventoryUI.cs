using UnityEngine;

public class SellInventoryUI : MonoBehaviour
{
    public Transform sellItems;

    public GameObject sellInventoryUI;

    ShopInventory shopInventory;
    Inventory inventory;

    ShopSlot[] sellSlots;

    // Start is called before the first frame update
    void Start()
    {
        shopInventory = ShopInventory.instance;
        shopInventory.onItemChangedCallback += UpdateUI;

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        sellSlots = sellItems.GetComponentsInChildren<ShopSlot>();

        UpdateUI();
    }

    void UpdateUI()
    {
        if (sellInventoryUI.activeSelf)
        {
            for (int i = 0; i < sellSlots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    sellSlots[i].AddItem(inventory.items[i]);
                }
                else
                    sellSlots[i].ClearSlot();
            }
        }
    }
}
