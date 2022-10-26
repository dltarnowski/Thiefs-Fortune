using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] int coinValue;
    [SerializeField] int destroyTime;
    
    private void Start()
    {
        if (destroyTime > 0)
            Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.currencyNumber += coinValue;
            Destroy(gameObject);
        }
    }
}
