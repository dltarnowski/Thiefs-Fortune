using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pirateLegendMeleeDamage: MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] pirateLegendEnemyAI EAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            gameManager.instance.playerScript.takeDamage(EAI.damage);
    }
}
