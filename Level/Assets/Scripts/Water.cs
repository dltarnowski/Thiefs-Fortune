using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.underwaterIndicator.SetActive(true);
            gameManager.instance.playerScript.jumpHeight = 3;
            gameManager.instance.playerScript.gravityValue /= 10;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.underwaterIndicator.SetActive(false);
            gameManager.instance.playerScript.jumpHeight = gameManager.instance.playerScript.jumpHeightOrig;
            gameManager.instance.playerScript.gravityValue = gameManager.instance.playerScript.gravityValueOrig;
        }
    }
}
