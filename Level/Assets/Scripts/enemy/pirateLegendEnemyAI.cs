using System.Collections;
using UnityEngine;

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
                if (playerInRange && !gameManager.instance.npcDialogue.activeSelf)
                {
                    playerDir = gameManager.instance.player.transform.position - headPos.transform.position;
                    angle = Vector3.Angle(playerDir, transform.forward);
                    WeaponSwap();
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
            anim.SetBool("rangeIdle", false);
            anim.SetBool("meleeIdle", true);
            weapon.GetComponent<MeshFilter>().sharedMesh = swordStat.model.GetComponent<MeshFilter>().sharedMesh;
            weapon.GetComponent<MeshRenderer>().sharedMaterial = swordStat.model.GetComponent<MeshRenderer>().sharedMaterial;
            canSeePlayer(melee(), isMelee);
        }
        else
        {
            anim.SetBool("rangeIdle", true);
            anim.SetBool("meleeIdle", false);
            weapon.GetComponent<MeshFilter>().sharedMesh = gunStat.model.GetComponent<MeshFilter>().sharedMesh;
            weapon.GetComponent<MeshRenderer>().sharedMaterial = gunStat.model.GetComponent<MeshRenderer>().sharedMaterial;
            canSeePlayer(shoot(), isShooting);
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
        if (weapon.GetComponent<MeshFilter>().sharedMesh == gunStat.model.GetComponent<MeshFilter>().sharedMesh && !isShooting)
        {
            isShooting = true;
            anim.SetTrigger("range");
            aud.PlayOneShot(gunStat.sound, enemyWeaponAudVol);
            bullet.GetComponent<Bullet>().damage = gunStat.strength * (1 + gameManager.instance.blackspot.blackSpotMultiplier);
            Invoke("BulletDelay", anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return new WaitForSeconds(gunStat.speed);
            isShooting = false;
        }
    }

    void BulletDelay()
    {
        Instantiate(bullet, attackPos.transform.position, transform.rotation);
    }
    IEnumerator melee()
    {
        if (weapon.GetComponent<MeshFilter>().sharedMesh == swordStat.model.GetComponent<MeshFilter>().sharedMesh && !isMelee)
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
}
