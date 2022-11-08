using UnityEngine;

public class pirateLegendMeleeDamage: MonoBehaviour
{
    [SerializeField] pirateLegendEnemyAI EAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            gameManager.instance.playerScript.takeDamage(EAI.swordStat.strength);
    }
}
