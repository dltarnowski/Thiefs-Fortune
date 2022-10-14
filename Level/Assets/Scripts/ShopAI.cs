using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopAI : MonoBehaviour
{
    [Header("----- Components -----")]
    //[SerializeField] Animator animator;
    [SerializeField] GameObject shopCam;

    bool playerInRange;
    Vector3 camOrig;
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            if(!gameManager.instance.npcDialogue.activeSelf && !gameManager.instance.shopInventory.activeSelf)
            {
                gameManager.instance.hint.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !gameManager.instance.shopInventory.activeSelf)
            {
                gameManager.instance.hint.SetActive(false);
                gameManager.instance.healthBar.SetActive(false);
                gameManager.instance.Crosshair.SetActive(false);
                gameManager.instance.cursorLockPause();
                gameManager.instance.npcDialogue.SetActive(playerInRange);
            }
        }
        else
        {
            gameManager.instance.hint.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Shop");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
