using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePickup : MonoBehaviour
{
    //[SerializeField] MeleeStats meleeStats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameManager.instance.playerScript.MeleePickup(meleeStats);

            Destroy(gameObject);
        }
    }
}
