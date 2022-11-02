using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockOut_OpenWorld : MonoBehaviour
{
    public GameObject edgeUI;
    public GameObject deathMenu;

    bool playerOutOfBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (deathMenu.activeSelf)
            edgeUI.SetActive(false);*/

        if(playerOutOfBounds)
        {
            edgeUI.SetActive(true);
            gameManager.instance.playerScript.takeDamage(1);
            gameManager.instance.playerScript.updatePlayerHUD();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOutOfBounds = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        playerOutOfBounds = false;
    }

}
