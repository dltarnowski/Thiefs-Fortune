using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Analytics;

public class ShopInventoryUI : MonoBehaviour
{
    public Transform shopItems;

    public GameObject shopInventoryUI;

    ShopInventory shopInventory;
    public List<Item> allWeapons = new List<Item>();
    ShopSlot[] shopSlots;

    // Start is called before the first frame update
    void Start()
    {
        shopInventory = ShopInventory.instance;
        shopInventory.onItemChangedCallback += UpdateUI;

        shopSlots = shopItems.GetComponentsInChildren<ShopSlot>();

        shopInventory.items = allWeapons;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateUI()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (i < shopInventory.items.Count)
            {
                shopSlots[i].AddItem(shopInventory.items[i]);
            }
            else
                shopSlots[i].ClearSlot();
        }
    }    
}
