using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableButtons : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject playerInventory;
    public GameObject buyInventory;
    public GameObject sellInventory;
    public Button buyTab;
    public Button sellTab;

    private void Start()
    {
    }
    public void Talk()
    {
        NPCManager.instance.talking = true;
    }

    public void Shop()
    {
        gameManager.instance.hint.SetActive(false);
        gameManager.instance.shopDialogue.SetActive(false);
        shopMenu.SetActive(true);
        playerInventory.SetActive(true);
        BuyTab();
    }

    public void CloseShop()
    {
        gameManager.instance.shopDialogue.SetActive(true);
        shopMenu.SetActive(false);
        playerInventory.SetActive(false);
    }

    public void Bye()
    {
        gameManager.instance.shopDialogue.SetActive(false);
        gameManager.instance.NpcUnpause();
        NPCManager.instance.dialogue.text = "I don't know nothin' about nothin'... What can I do for you today?";
        gameManager.instance.mainCamera.SetActive(true);
        NPCManager.instance.NPCCamera.SetActive(false);
    }

    public void BuyTab()
    {
        buyInventory.SetActive(true);
        sellInventory.SetActive(false);
        buyTab.interactable = false;
        sellTab.interactable = true;
        ShopInventory.instance.onItemChangedCallback.Invoke();
    }

    public void SellTab()
    {
        buyInventory.SetActive(false);
        sellInventory.SetActive(true);
        buyTab.interactable = true;
        sellTab.interactable = false;
        ShopInventory.instance.onItemChangedCallback.Invoke();
    }
}
