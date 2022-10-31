using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuButtons : MonoBehaviour
{
    ShopInventory inventory;
    public GameObject shopMenu;
    public GameObject playerInventory;
    public GameObject buyInventory;
    public GameObject sellInventory;
    public Button buyTab;
    public Button sellTab;

    private void Start()
    {
        inventory = ShopInventory.instance;
    }
    public void Talk()
    {
        NPCManager.instance.dialogue.enabled = false;
        NPCManager.instance.followUpDialogue.enabled = true;
    }

    public void Shop()
    {
        gameManager.instance.hint.SetActive(false);
        gameManager.instance.npcDialogue.SetActive(false);
        shopMenu.SetActive(true);
        playerInventory.SetActive(true);
        BuyTab();
    }

    public void CloseShop()
    {
        gameManager.instance.npcDialogue.SetActive(true);
        shopMenu.SetActive(false);
        playerInventory.SetActive(false);
    }

    public void Bye()
    {
        gameManager.instance.npcDialogue.SetActive(false);
        gameManager.instance.NpcUnpause();
        NPCManager.instance.dialogue.text = "What's that smell... Sniff Sniff... Huh I think that's me... Oh Hi! What can I do for you today?";
            //dialogue.text = "I don't know nothin' about nothin'... What can I do for you today?";
        gameManager.instance.mainCamera.SetActive(true);
        NPCManager.instance.NPCCamera.SetActive(false);
    }

    public void BuyTab()
    {
        buyInventory.SetActive(true);
        sellInventory.SetActive(false);
        buyTab.interactable = false;
        sellTab.interactable = true;
        inventory.onItemChangedCallback.Invoke();
    }

    public void SellTab()
    {
        buyInventory.SetActive(false);
        sellInventory.SetActive(true);
        buyTab.interactable = true;
        sellTab.interactable = false;
        inventory.onItemChangedCallback.Invoke();
    }
}
