using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spears : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damage = gameManager.instance.playerScript.HPOrig / 5;
            gameManager.instance.playerScript.takeDamage((int)damage);
        }
    }
}
