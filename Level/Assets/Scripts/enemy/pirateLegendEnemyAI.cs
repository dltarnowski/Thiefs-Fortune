using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pirateLegendEnemyAI : enemyAI
{
    [Header("----- Legend Weapon Stats -----")]
    [SerializeField] Gun gunStat;
    [SerializeField] internal Sword swordStat;
    [SerializeField] internal GameObject attackPos;
    [SerializeField] internal GameObject bullet;
    [SerializeField] float attackSwitchRange;

    bool isMelee;
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
                WeaponSwap();
                if (playerInRange && !gameManager.instance.npcDialogue.activeSelf)
                {
                    playerDir = gameManager.instance.player.transform.position - headPos.transform.position;
                    angle = Vector3.Angle(playerDir, transform.forward);
                    if (weapon.GetComponent<MeshFilter>().sharedMesh == gunStat.model.GetComponent<MeshFilter>().sharedMesh)
                        canSeePlayer(shoot(), isShooting);
                    else if (weapon.GetComponent<MeshFilter>().sharedMesh == swordStat.model.GetComponent<MeshFilter>().sharedMesh)
                        canSeePlayer(melee(), isMelee);
                }
                if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position && !stationary && canRoam)
                    roam();
                else if (!canRoam && stationary)
                    facePlayer();
            }
        }
    }

    void WeaponSwap()
    {
        if (agent.remainingDistance < attackSwitchRange)
        {
            anim.SetBool("meleeIdle", true);
            anim.SetBool("rangeIdle", false);
            weapon.GetComponent<MeshFilter>().sharedMesh = swordStat.model.GetComponent<MeshFilter>().sharedMesh;
            weapon.GetComponent<MeshRenderer>().sharedMaterial = swordStat.model.GetComponent<MeshRenderer>().sharedMaterial;
        }
        else
        {
            anim.SetBool("meleeIdle", false);
            anim.SetBool("rangeIdle", true);
            weapon.GetComponent<MeshFilter>().sharedMesh = gunStat.model.GetComponent<MeshFilter>().sharedMesh;
            weapon.GetComponent<MeshRenderer>().sharedMaterial = gunStat.model.GetComponent<MeshRenderer>().sharedMaterial;
        }
    }


    public override void takeDamage(float dmg)
    {
        base.takeDamage(dmg);
        if (HP <= 0)
            gameManager.instance.blackspot.blackSpotMultiplier = 0;
    }

    IEnumerator shoot()
    {
        isShooting = true;
        anim.SetTrigger("range");
        aud.PlayOneShot(gunStat.sound, enemyWeaponAudVol);
        bullet.GetComponent<Bullet>().damage = gunStat.strength * (1 + gameManager.instance.blackspot.blackSpotMultiplier);
        Instantiate(bullet, attackPos.transform.position, transform.rotation);
        yield return new WaitForSeconds(gunStat.speed);
        isShooting = false;
    }


    IEnumerator melee()
    {
        isMelee = true;
        if (gameManager.instance.player.transform.position.normalized.magnitude - transform.position.normalized.magnitude <= swordStat.distance)
        {
            aud.PlayOneShot(swordStat.sound, enemyWeaponAudVol);
            anim.SetTrigger("melee");
        }
        yield return new WaitForSeconds(swordStat.speed);
        isMelee = false;
    }
}
