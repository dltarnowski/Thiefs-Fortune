using UnityEngine;

public class meleeSharkDamage : MonoBehaviour
{
    [SerializeField] enemySharkAI EAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            gameManager.instance.playerScript.takeDamage(EAI.damage);
    }
}
