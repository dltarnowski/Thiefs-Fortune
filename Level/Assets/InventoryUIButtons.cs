using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIButtons : MonoBehaviour
{
    public GameObject inventory;
    public GameObject activeInventory;
    public Button inventoryTab;
    public Button activeTab;

    public void Inventory()
    {
        inventory.SetActive(true);
        activeInventory.SetActive(false);
        inventoryTab.interactable = false;
        activeTab.interactable = true;
    }

    public void Active()
    {
        inventory.SetActive(false);
        activeInventory.SetActive(true);
        inventoryTab.interactable = true;
        activeTab.interactable = false;
    }
}
