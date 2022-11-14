using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.UI;

public class ShopAI : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] Animator anim;
    [SerializeField] Collider collide;
    public GameObject shopCam;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.weaponCollide && !gameManager.instance.shopInventory.activeSelf && !gameManager.instance.shopDialogue.activeSelf && !gameManager.instance.deathMenu.activeSelf)
        {
            anim.SetBool("isWaving", true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                NPCManager.instance.dialogue.gameObject.SetActive(true);
                NPCManager.instance.followUpDialogue.gameObject.SetActive(false);
                gameManager.instance.miniMapWindow.SetActive(false);

                NPCManager.instance.NPCCamera = shopCam;
                NPCManager.instance.dialogue.text = "What's that smell... Sniff Sniff... Huh I think that's me... Oh Hi! What can I do for you today?";
                NPCManager.instance.npcName.text = "Willy " + '"' + "Full Sails" + '"' + " Wilkerson";
                NPCManager.instance.talkButtonText.text = "I'm looking for Captain Noble. Can you help?";
                NPCManager.instance.shopButtonText.text = "I hear you sell weapons. What do you have?";
                NPCManager.instance.followUpDialogue.text = "Last I heard, he was seen heading to Lone Peak Isle. The food vendor on the docks might know more...";

                NPCManager.instance.NPCCamera.SetActive(true);
                gameManager.instance.mainCamera.SetActive(false);

                anim.SetBool("isWaving", false);
                anim.SetBool("isTalking", true);
                gameManager.instance.hint.SetActive(false);

                gameManager.instance.NpcPause();
                
                gameManager.instance.shopDialogue.SetActive(gameManager.instance.weaponCollide);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.weaponCollide = true;
            NPCManager.instance.shopUI = NPCManager.instance.weaponUI;
        }

        gameManager.instance.hint.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.weaponCollide = false;
        }

        gameManager.instance.hint.SetActive(false);
    }
}