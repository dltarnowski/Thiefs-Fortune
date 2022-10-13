using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthIncrease;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.HP += healthIncrease;
            if (gameManager.instance.playerScript.HP > gameManager.instance.playerScript.HPOrig)
                gameManager.instance.playerScript.HP = gameManager.instance.playerScript.HPOrig;

            Destroy(gameObject);
        }
    }
}
