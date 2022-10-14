using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAI : MonoBehaviour
{
    [Header("----- Components -----")]
    //[SerializeField] Animator animator;

    //[Header("----- NPC Stats -----")]
    //[SerializeField] int sightDist;
    //[SerializeField] GameObject headPos;

    bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            gameManager.instance.hint.enabled = true;
            if(Input.GetKeyDown(KeyCode.E))
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
            Debug.Log("Collision");
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
