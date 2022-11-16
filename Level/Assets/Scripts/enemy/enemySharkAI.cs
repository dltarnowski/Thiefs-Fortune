using UnityEngine;

public class enemySharkAI : enemyAI
{
    [Header("----- Shark Stats -----")]
    [SerializeField] float deadLerpSpeed;
    [SerializeField] public float damage;

    void Update()
    {
        if(!anim.GetBool("Dead"))
        {
            blackSpotUpdate();

            if (agent.enabled)
            {
                if (playerInRange && !gameManager.instance.shopDialogue.activeSelf)
                {
                    playerDir = gameManager.instance.player.transform.position - headPos.transform.position;
                    angle = Vector3.Angle(playerDir, transform.forward);
                    canSeePlayer();
                }
                if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position)
                    roam();
            }
        }
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 180), Time.deltaTime * deadLerpSpeed);
    }
}
