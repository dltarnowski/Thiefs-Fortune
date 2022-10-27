using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoIncrease;
    [SerializeField] int destroyTime;

    private void Start()
    {
        if(destroyTime > 0)
            Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.instance.playerScript.ammoCount < 5)
            {
                gameManager.instance.playerScript.ammoCount += ammoIncrease;

                if (gameManager.instance.playerScript.ammoCount > 5)
                    gameManager.instance.playerScript.ammoCount = 5;

                gameManager.instance.ammoCount =
                gameManager.instance.playerScript.gunStat[gameManager.instance.playerScript.selectGun].ammoCount =
                gameManager.instance.playerScript.ammoCount;

                gameManager.instance.IncreaseAmmo();

                Destroy(gameObject);
            }
        }
    }
}
