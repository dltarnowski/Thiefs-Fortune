using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] int coinValue = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.currencyNumber += coinValue;
            Debug.Log(gameManager.instance.currencyNumber);
            gameManager.instance.playerScript.updatePlayerHUD();
            Destroy(gameObject);
        }
    }
}
