using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{
    [Header("----- Componenets -----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int facePlayerSpeed;
    [SerializeField] int sightDist;
    [SerializeField] float damagedDuration;

    [Header("----- Weapon Stats -----")]
    [SerializeField] internal float attackRate;
    [SerializeField] internal GameObject attackPos;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject bullet;


    public bool stationary;
    public bool noRotation;
    bool isShooting;
    bool playerInRange;
    Color modelColor;

    void Awake()
    {
        modelColor = model.material.color;
    }

    void Start()
    {
        gameManager.instance.EnemyNumber++;
        gameManager.instance.EnemyCountText.text = gameManager.instance.EnemyNumber.ToString("F0");
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled && playerInRange)
        {
            if(!noRotation)
            {
                if (stationary)
                    FaceTarget();
                else
                    agent.SetDestination(gameManager.instance.player.transform.position);
            }
            if (gameObject.CompareTag("Ranged") && !isShooting)
                StartCoroutine(shoot());
        }
    }

    void FaceTarget()
    {
        Vector3 turnTowardNavSteeringTarget = agent.steeringTarget;

        Vector3 direction = (turnTowardNavSteeringTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    public void takeDamage(int dmg)
    {
        HP -= dmg;
        StartCoroutine(flashDamage());
        if (HP <= 0)
        {
            gameManager.instance.checkEnemyTotal();
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
