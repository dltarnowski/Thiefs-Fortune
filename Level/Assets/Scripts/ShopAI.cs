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

    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (playerInRange && !gameManager.instance.shopInventory.activeSelf && !gameManager.instance.npcDialogue.activeSelf && !gameManager.instance.deathMenu.activeSelf)
        {
            anim.SetBool("isWaving", true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                shopCam.SetActive(true);
                gameManager.instance.mainCamera.SetActive(false);

                anim.SetBool("isWaving", false);
                anim.SetBool("isTalking", true);
                gameManager.instance.hint.SetActive(false);

                gameManager.instance.NpcPause();
                
                gameManager.instance.npcDialogue.SetActive(playerInRange);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }

        gameManager.instance.hint.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }

        gameManager.instance.hint.SetActive(false);
    }
}