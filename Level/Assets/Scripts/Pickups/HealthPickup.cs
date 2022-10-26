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
            gameManager.instance.playerScript.HP += healthIncrease;
            gameManager.instance.playerScript.lerpTime = 0f;
            gameManager.instance.playerScript.updatePlayerHUD();
            if (gameManager.instance.playerScript.HP > gameManager.instance.playerScript.HPOrig)
                gameManager.instance.playerScript.HP = gameManager.instance.playerScript.HPOrig;

                Destroy(gameObject);
            }
        }
    }
}
