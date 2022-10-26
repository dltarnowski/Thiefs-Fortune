using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthIncrease;
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
            if (gameManager.instance.playerScript.HP < gameManager.instance.playerScript.HPOrig)
            {
                gameManager.instance.playerScript.HP += healthIncrease;
                if (gameManager.instance.playerScript.HP > gameManager.instance.playerScript.HPOrig)
                    gameManager.instance.playerScript.HP = gameManager.instance.playerScript.HPOrig;

                Destroy(gameObject);
            }
        }
    }
}
