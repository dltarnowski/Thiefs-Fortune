using UnityEngine;

public class meleeDamage : MonoBehaviour
{
    [SerializeField] meleeEnemyAI EAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject != null)
            gameManager.instance.playerScript.takeDamage(EAI.swordStat.strength);
    }
}
