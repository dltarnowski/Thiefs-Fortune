using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{
    [Header("----- Componenets -----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer model;
    [SerializeField] CannonController cannonCtrl;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int speedChase;
    [SerializeField] int facePlayerSpeed;
    [SerializeField] int sightDist;
    [SerializeField] int viewAngle;
    [SerializeField] float damagedDuration;
    [SerializeField] GameObject headPos;
    [SerializeField] int roamDist;

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
    Vector3 playerDir;
    float stoppingDistanceOrig;
    Vector3 startingPos;
    float angle;
    float speedPatrol;


    void Start()
    {
        gameManager.instance.EnemyNumber++;
        gameManager.instance.EnemyCountText.text = gameManager.instance.EnemyNumber.ToString("F0");
        modelColor = model.material.color;
        stoppingDistanceOrig = agent.stoppingDistance;
        startingPos = transform.position;
        speedPatrol = agent.speed;
        roam();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (playerInRange)
            {
                playerDir = gameManager.instance.player.transform.position - headPos.transform.position;
                angle = Vector3.Angle(playerDir, transform.forward);
                canSeePlayer();
            }
            if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position)
                roam();
        }
    }

    void roam()
    {
        agent.stoppingDistance = 0;
        agent.speed = speedPatrol;
        Vector3 randomDirection = Random.insideUnitSphere * roamDist;
        randomDirection += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 1, 1);
        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);
        agent.SetPath(path);
    }

    void canSeePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(headPos.transform.position, playerDir, out hit, sightDist))
        {
            Debug.DrawRay(headPos.transform.position, playerDir);
            if (hit.collider.CompareTag("Player") && angle <= viewAngle)
            {
                agent.speed = speedChase;
                agent.stoppingDistance = stoppingDistanceOrig;
                agent.SetDestination(gameManager.instance.player.transform.position);
                if (agent.remainingDistance < agent.stoppingDistance)
                    facePlayer();

                if (!isShooting)
                    StartCoroutine(shoot());
            }
        }
    }

    void facePlayer()
    {
        playerDir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * facePlayerSpeed);
    }
    public void takeDamage(int dmg)
    {
        HP -= dmg;
        StartCoroutine(flashDamage());
        if (HP <= 0)
        {
            gameManager.instance.checkEnemyTotal();
            if(cannonCtrl != null)
            {
                cannonCtrl.enabled = true;
            }
            gameObject.transform.DetachChildren();
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
        agent.SetDestination(gameManager.instance.player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            agent.stoppingDistance = 0;
        }

    }
}
