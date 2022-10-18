using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoIncrease;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.ammoCount += ammoIncrease;

            gameManager.instance.ammoCount = 
            gameManager.instance.playerScript.gunStat[gameManager.instance.playerScript.selectGun].ammoCount = 
            gameManager.instance.playerScript.ammoCount;

            Destroy(gameObject);
        }
    }
}
