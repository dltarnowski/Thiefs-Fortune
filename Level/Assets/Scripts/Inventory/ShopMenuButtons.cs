using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuButtons : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject playerInventory;
    public GameObject buyInventory;
    public GameObject sellInventory;
    public Button buyTab;
    public Button sellTab;
    public TextMeshProUGUI dialogue;

    private void Start()
    {
    }
    public void Talk()
    {
        dialogue.text = "Last I heard, he was seen heading to Lone Peak Isle. The food vendor on the docks might know more...";
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
        //dialogue.text = "What's that smell... Sniff Sniff... Huh I think that's me... Oh Hi! What can I do for you today?";
        gameManager.instance.mainCamera.SetActive(true);
        gameManager.instance.shopCam.SetActive(false);
    }

    public void BuyAmmo()
    {
        if (gameManager.instance.currencyNumber >= 2 && gameManager.instance.ammoCount != 5)
        {
            int ammoGone = 5 - gameManager.instance.playerScript.gunStats.ammoCount;

            if (ammoGone >= 1)
            {
                gameManager.instance.playerScript.gunStats.ammoCount = 5;
            }
            else
            {
                gameManager.instance.playerScript.gunStats.ammoCount += ammoGone;
            }

            gameManager.instance.currencyNumber -= 2;
            gameManager.instance.IncreaseAmmo();
        }
    }

    public void BuyHealth()
    {
        if (gameManager.instance.currencyNumber >= 2 && gameManager.instance.playerScript.HP < gameManager.instance.playerScript.HPOrig)
        {
            float healthGone = gameManager.instance.playerScript.HPOrig - gameManager.instance.playerScript.HP;

            if (healthGone >= (gameManager.instance.playerScript.HPOrig / 2))
            {
                gameManager.instance.playerScript.HP += (gameManager.instance.playerScript.HPOrig / 2);
            }
            else
            {
                gameManager.instance.playerScript.HP += healthGone;
                gameManager.instance.playerScript.lerpTime = 0f;
            }

            gameManager.instance.currencyNumber -= 2;
        }
    }

    public void NoBuy()
    {
        gameManager.instance.npcDialogue.SetActive(true);
        gameManager.instance.shopInventory.SetActive(false);
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
