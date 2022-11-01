using System.Collections.Generic;
using UnityEngine;

public class ConsumeShopUI : MonoBehaviour
{
    public Transform shopItems;

    public GameObject shopUI;

    ConsumableInventory consumeInventory;

    public List<Consumable> allConsumables = new List<Consumable>();
    ConsumeSlots[] shopSlots;

    // Start is called before the first frame update
    void Start()
    {
        consumeInventory = ConsumableInventory.instance;
        consumeInventory.onItemChangedCallback += UpdateUI;

        shopSlots = shopItems.GetComponentsInChildren<ConsumeSlots>();

        consumeInventory.items = allConsumables;

        UpdateUI();

        NPCManager.instance.shopUI = shopUI;
    }

    void UpdateUI()
    {
        if (shopUI.activeSelf)
        {
            for (int i = 0; i < shopSlots.Length; i++)
            {
                if (i < consumeInventory.items.Count)
                {
                    shopSlots[i].AddItem(consumeInventory.items[i]);
                }
                else
                    shopSlots[i].ClearSlot();
            }
        }
    }
}
