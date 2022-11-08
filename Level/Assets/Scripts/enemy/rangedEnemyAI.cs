using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class rangedEnemyAI : enemyAI
{

    [Header("----- Weapon Stats -----")]
    [SerializeField] internal GameObject attackPos;
    [SerializeField] GameObject bullet;
    [SerializeField] Gun gunStat;



    bool isShooting;
    bool equipped;


    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Dead"))
        {
            if (!equipped)
            {
                weapon.GetComponent<MeshFilter>().sharedMesh = gunStat.model.GetComponent<MeshFilter>().sharedMesh;
                weapon.GetComponent<MeshRenderer>().sharedMaterial = gunStat.model.GetComponent<MeshRenderer>().sharedMaterial;
                equipped = true;
            }

            blackSpotUpdate();
            movementAnimationChange();

            if (agent.enabled)
            {
                if (playerInRange && !gameManager.instance.npcDialogue.activeSelf)
                {
                    playerDir = gameManager.instance.player.transform.position - headPos.transform.position;
                    angle = Vector3.Angle(playerDir, transform.forward);
                    canSeePlayer(shoot(), isShooting);

                }
                if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position && !stationary && canRoam && !playerInRange)
                    roam();
                else if (!canRoam && stationary)
                    facePlayer();
            }
        }
    }
    IEnumerator shoot()
    {
        isShooting = true;
        anim.SetTrigger("attack");
        aud.PlayOneShot(gunStat.sound, enemyWeaponAudVol);
        bullet.GetComponent<Bullet>().damage = gunStat.strength * (1 + gameManager.instance.blackspot.blackSpotMultiplier);
        Instantiate(bullet, attackPos.transform.position, transform.rotation);
        yield return new WaitForSeconds(gunStat.speed);
        isShooting = false;
    }

}
