using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] meleeEnemyAI EAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            gameManager.instance.playerScript.takeDamage(EAI.swordStat.strength);
    }
}
