using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableShopAI : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] Animator anim;
    [SerializeField] Collider collide;
    public GameObject shopCam;

    // Update is called once per frame
    void Update()
    {

        if (gameManager.instance.consumeCollide && !gameManager.instance.shopInventory.activeSelf && !gameManager.instance.shopDialogue.activeSelf && !gameManager.instance.deathMenu.activeSelf)
        {
            anim.SetBool("isWaving", true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                NPCManager.instance.dialogue.gameObject.SetActive(true);
                NPCManager.instance.followUpDialogue.gameObject.SetActive(false);

                NPCManager.instance.NPCCamera = shopCam;
                NPCManager.instance.npcName.text = "Franky " + '"' + "PHAT" + '"' + " Findley";
                NPCManager.instance.talkButtonText.text = "Willy said you might know where to find Captain Noble?";
                NPCManager.instance.shopButtonText.text = "You sell ammo and health elixirs. Right?";
                NPCManager.instance.followUpDialogue.text = "Willy sent you? Yah, I heard he's camped out at Serpent Cove. I'd avoid him though if I were you...";
                NPCManager.instance.dialogue.text = "I don't know nothin' about nothin'... What can I do for you today?";


                NPCManager.instance.NPCCamera.SetActive(true);
                gameManager.instance.mainCamera.SetActive(false);

                anim.SetBool("isWaving", false);
                anim.SetBool("isTalking", true);
                gameManager.instance.hint.SetActive(false);

                gameManager.instance.NpcPause();

                gameManager.instance.shopDialogue.SetActive(gameManager.instance.consumeCollide);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.consumeCollide = true;
            NPCManager.instance.shopUI = NPCManager.instance.consumeUI;
            gameManager.instance.hint.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.consumeCollide = false;
        gameManager.instance.hint.SetActive(false);
    }
}