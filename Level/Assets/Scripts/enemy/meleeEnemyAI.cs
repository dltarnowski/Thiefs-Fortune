using System.Collections;
using UnityEngine;

public class meleeEnemyAI : enemyAI
{
    [Header("----- Melee Weapon Stats -----")]
    [SerializeField] internal Sword swordStat;
    internal bool isMelee;

    internal bool equipped;

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Dead"))
        {
            if(!equipped)
            {
                weapon.GetComponent<MeshFilter>().sharedMesh = swordStat.model.GetComponent<MeshFilter>().sharedMesh;
                weapon.GetComponent<MeshRenderer>().sharedMaterial = swordStat.model.GetComponent<MeshRenderer>().sharedMaterial;
                equipped = true;
            }

            blackSpotUpdate();
            movementAnimationChange();

            if (agent.enabled)
            {
                if (playerInRange && !gameManager.instance.shopDialogue.activeSelf)
                {
                    playerDir = gameManager.instance.player.transform.position - headPos.transform.position;
                    angle = Vector3.Angle(playerDir, transform.forward);
                    canSeePlayer(melee(), isMelee);
                }
                if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position && !stationary && canRoam)
                    roam();
                else if (!canRoam && stationary)
                    facePlayer();
            }
        }
    }

    public IEnumerator melee()
    {
        if(!isMelee)
        {
            isMelee = true;
            if (gameManager.instance.player.transform.position.normalized.magnitude - transform.position.normalized.magnitude <= swordStat.distance)
            {
                aud.PlayOneShot(swordStat.sound, enemyWeaponAudVol);
                anim.SetTrigger("attack");
            }
            yield return new WaitForSeconds(swordStat.speed);
            isMelee = false;
        }
    }
}
