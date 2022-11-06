using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMeleeNPC : meleeEnemyAI
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Dead"))
        {
            if (!equipped)
            {
                weapon.GetComponent<MeshFilter>().sharedMesh = swordStat.model.GetComponent<MeshFilter>().sharedMesh;
                weapon.GetComponent<MeshRenderer>().sharedMaterial = swordStat.model.GetComponent<MeshRenderer>().sharedMaterial;
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
                    canSeePlayer(melee(), isMelee);
                }
                if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position && !stationary && canRoam)
                    roam();
                else if (!canRoam && stationary)
                    facePlayer();
            }
        }
    }
    public override void takeDamage(float dmg)
    {
        base.takeDamage(dmg);
    }

    public void OnDestroy()
    {
        TutorialManager.instance.meleeEnemiesLeft--;
    }
}
