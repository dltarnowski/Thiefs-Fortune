using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] float meleeAttackRange;
    [SerializeField] int meleeDamage;
    [SerializeField] LayerMask whatIsPlayer;
    bool isMelee;
    [SerializeField] enemyAI eAI;
   

    private void Update()
    {
        if(CompareTag("Melee") && !isMelee)
            StartCoroutine(melee());
    }
    IEnumerator melee()
    {
        isMelee = true;
        Collider[] hit = Physics.OverlapSphere(transform.position, meleeAttackRange, whatIsPlayer);
        // Loops through all Game Objects that are withn Range and in the Layer Mask
        for (int i = 0; i < hit.Length; i++)
        {
            // Removes Health from GameObjects that are within Range and in the LayerMask
            hit[i].GetComponent<playerController>().takeDamage(meleeDamage);
        }
        yield return new WaitForSeconds(eAI.attackRate);
        isMelee = false;
    }
}
