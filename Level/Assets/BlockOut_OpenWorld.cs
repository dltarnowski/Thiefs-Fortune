using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockOut_OpenWorld : MonoBehaviour
{
    public GameObject edgeUI;
    public GameObject deathMenu;

    bool playerOutOfBounds;
    bool damagingPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deathMenu.activeSelf)
            edgeUI.SetActive(false);

        if(playerOutOfBounds)
        {
            if(!deathMenu.activeSelf)
                edgeUI.SetActive(true);

            if(!damagingPlayer)
                StartCoroutine(DamageTimer());

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
        edgeUI.SetActive(false);
    }

    IEnumerator DamageTimer()
    {
        damagingPlayer = true;
        yield return new WaitForSeconds(2);
        gameManager.instance.playerScript.takeDamage(10);
        damagingPlayer = false;
    }
}
