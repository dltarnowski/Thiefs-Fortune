using System.Collections;
using UnityEngine;

public class cannonEnemyAI : enemyAI
{
    [Header("----- Componenets -----")]
    [SerializeField] CannonController cannonCtrl;
    [SerializeField] SphereCollider cannonCol;
    [SerializeField] GameObject cannon;



    [Header("----- Weapon Stats -----")]
    [SerializeField] internal GameObject attackPos;
    [SerializeField] GameObject bullet;
    [SerializeField] Gun gunStat;


    bool isShooting;


    void Start()
    {
        if (canRoam)
            roam();
        if (cannon != null)
            cannon.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Dead"))
        {
            blackSpotUpdate();
            movementAnimationChange();

            if (agent.enabled)
            {
                if (playerInRange)
                {
                    playerDir = gameManager.instance.player.transform.position - headPos.transform.position;
                    angle = Vector3.Angle(playerDir, transform.forward);
                    canSeePlayer(shoot(), isShooting);
                }
                if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position && canRoam)
                    roam();
                else if (!canRoam)
                    facePlayer();
            }
        }
    }


    public override void takeDamage(float dmg)
    {
        base.takeDamage(dmg);
        if (HP <= 0)
        {
            cannonCtrl.enabled = true;
            cannonCol.enabled = true;
            cannon.transform.parent = null;
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
