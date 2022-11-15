using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuButtons : MonoBehaviour
{
    public static ShopMenuButtons instance;

    public static ShopInventory inventory;
    public GameObject playerInventory;
    public GameObject buyInventory;
    public GameObject sellInventory;
    public Button buyTab;
    public Button sellTab;

    bool weaponNPC;
    bool consumeNPC;

    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        inventory = ShopInventory.instance;
    }
    public void Talk()
    {
        NPCManager.instance.dialogue.gameObject.SetActive(false);
        NPCManager.instance.followUpDialogue.gameObject.SetActive(true);

        if (!weaponNPC && gameManager.instance.weaponCollide && winManager.instance.clueCount == 2)
        {
            winManager.instance.clueCount++;
            gameManager.instance.CurrentObjectiveMiniMapIcon();
            weaponNPC = true;
        }
        if (!consumeNPC && gameManager.instance.consumeCollide && winManager.instance.clueCount == 3)
        {
            winManager.instance.clueCount++;
            consumeNPC = true;
        }
    }

    public void Shop()
    {
        gameManager.instance.hint.SetActive(false);
        gameManager.instance.shopDialogue.SetActive(false);
        NPCManager.instance.shopUI.SetActive(true);
        playerInventory.SetActive(true);
        BuyTab();
    }

    public void CloseShop()
    {
        gameManager.instance.shopDialogue.SetActive(true);
        NPCManager.instance.shopUI.SetActive(false);
        playerInventory.SetActive(false);
    }

    public void Bye()
    {
        gameManager.instance.shopDialogue.SetActive(false);
        gameManager.instance.NpcUnpause();
        if(gameManager.instance.consumeCollide == true)
            NPCManager.instance.dialogue.text = "I don't know nothin' about nothin'... What can I do for you today?";
        else if(gameManager.instance.weaponCollide == true)
            NPCManager.instance.dialogue.text = "What's that smell... Sniff Sniff... Huh I think that's me... Oh Hi! What can I do for you today?";

        gameManager.instance.mainCamera.SetActive(true);
        NPCManager.instance.NPCCamera.SetActive(false);
        gameManager.instance.miniMapWindow.SetActive(true);
    }

    public void BuyTab()
    {
        buyInventory.SetActive(true);
        sellInventory.SetActive(false);
        buyTab.interactable = false;
        sellTab.interactable = true;
        Inventory.instance.onItemChangedCallback.Invoke();
    }

    public void SellTab()
    {
        buyInventory.SetActive(false);
        sellInventory.SetActive(true);
        buyTab.interactable = true;
        sellTab.interactable = false;
        Inventory.instance.onItemChangedCallback.Invoke();
    }
}
