using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{
    [Header("----- Componenets -----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] LayerMask whatIsPlayer;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int facePlayerSpeed;
    [SerializeField] int sightDist;
    [SerializeField] float damagedDuration;

    [Header("----- Weapon Stats -----")]
    [SerializeField] float attackRate;
    [SerializeField] GameObject attackPos;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject bullet;
    [SerializeField] float meleeAttackRange;
    [SerializeField] int meleeDamage;
    bool isShooting;
    bool isMelee;
    bool playerInRange;
    Color modelColor;

    void Awake()
    {
        modelColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled && playerInRange)
        {
            agent.SetDestination(gameManager.instance.player.transform.position);

            if (gameObject.CompareTag("Ranged") && !isShooting)
                StartCoroutine(shoot());
            else if (gameObject.CompareTag("Melee") && !isMelee)
                StartCoroutine(melee());
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        StartCoroutine(flashDamage());
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;
        Instantiate(bullet, attackPos.transform.position, transform.rotation);
        yield return new WaitForSeconds(attackRate);
        isShooting = false;
    }

    IEnumerator melee()
    {
        isMelee = true;
        Collider[] hit = Physics.OverlapSphere(attackPos.transform.position, meleeAttackRange, whatIsPlayer);
        // Loops through all Game Objects that are withn Range and in the Layer Mask
        for (int i = 0; i < hit.Length; i++)
        {
            // Removes Health from GameObjects that are within Range and in the LayerMask
            //hit[i].GetComponent<playerController>().takeDamage(meleeDamage);
        }
        yield return new WaitForSeconds(attackRate);
        isMelee = false;
    }

    IEnumerator flashDamage()
    {
        model.material.color = Color.red;
        agent.enabled = false;
        yield return new WaitForSeconds(damagedDuration);
        model.material.color = modelColor;
        agent.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
