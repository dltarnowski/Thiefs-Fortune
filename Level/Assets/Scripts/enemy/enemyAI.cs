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
    [SerializeField] SphereCollider cannonCol;
    [SerializeField] GameObject cannon;
    [SerializeField] Collider col;
    [SerializeField] Animator anim;
    [SerializeField] GameObject[] drops;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] int reward;
    [SerializeField] int speedChase;
    [SerializeField] int facePlayerSpeed;
    [SerializeField] int animLerpSpeed;
    [SerializeField] int sightDist;
    [SerializeField] int viewAngle;
    [SerializeField] float damagedDuration;
    [SerializeField] GameObject headPos;

    [Header("----- Weapon Stats -----")]
    [SerializeField] internal float attackRate;
    [SerializeField] internal GameObject attackPos;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject bullet;
    [SerializeField] float meleeAttackRange;
    [SerializeField] public int meleeDamage;

    [Header("----- Roam Settings -----")]
    [SerializeField] bool linearRoam;
    [SerializeField] bool stationary;
    [SerializeField] bool noRotation;
    [SerializeField] int roamDist;
    [SerializeField] bool canRoam = true;

    [Header("----- Audio -----")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] enemyHurtAud;
    [Range(0, 1)] [SerializeField] float enemyHurtAudVol;
    [SerializeField] AudioClip[] enemyStepsAud;
    [Range(0, 1)] [SerializeField] float enemyStepsAudVol;
    [SerializeField] AudioClip enemyWeaponAud;
    [Range(0, 1)] [SerializeField] float enemyWeaponAudVol;

    bool playingSteps;
    Vector3 randomDirection;
    bool isMelee;
    bool isShooting;
    bool playerInRange;
    Color modelColor;
    Vector3 playerDir;
    float stoppingDistanceOrig;
    Vector3 startingPos;
    float angle;
    float speedPatrol;
    Transform tempTrans;


    void Start()
    {
        modelColor = model.material.color;
        stoppingDistanceOrig = agent.stoppingDistance;
        startingPos = transform.position;
        speedPatrol = agent.speed;
        tempTrans = cannon.transform.parent;
        if (!stationary && canRoam)
            roam();
        if (cannon != null)
            cannon.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.GetBool("Dead"))
        {
            if(linearRoam && !playerInRange)
                anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude * 0.5f, Time.deltaTime * animLerpSpeed));
            else
                anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * animLerpSpeed));

            if (agent.enabled)
            {
                if (playerInRange)
                {
                    playerDir = gameManager.instance.player.transform.position - headPos.transform.position;
                    angle = Vector3.Angle(playerDir, transform.forward);
                    if(CompareTag("Ranged"))
                        canSeePlayer(shoot(), isShooting);
                    else if(CompareTag("Melee"))
                        canSeePlayer(melee(), isMelee);

                }
                if (agent.remainingDistance < 0.1f && agent.destination != gameManager.instance.player.transform.position && !stationary && canRoam)
                    roam();
                else if (!canRoam && stationary)
                    facePlayer();
            }
        }
    }

    IEnumerator PlaySteps()
    {
        if (agent.velocity.normalized.magnitude > 0.3f && !playingSteps)
        {
            playingSteps = true;

            aud.PlayOneShot(enemyStepsAud[Random.Range(0, enemyStepsAud.Length - 1)], enemyStepsAudVol);

            if (agent.speed == speedChase)
                yield return new WaitForSeconds(0.3f);
            else
                yield return new WaitForSeconds(0.4f);

            playingSteps = false;
        }
    }

    void roam()
    {
        agent.stoppingDistance = 0;
        agent.speed = speedPatrol;
        if(linearRoam)
            randomDirection = new Vector3(0, 0, 1) * Random.Range(-roamDist, roamDist);
        else
            randomDirection = Random.insideUnitSphere * roamDist;
        randomDirection += startingPos;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomDirection, out hit, 1, 1);

        if (!hit.hit)
            return;

        NavMeshPath path = new NavMeshPath();

        agent.CalculatePath(hit.position, path);
        agent.SetPath(path);
    }

    void canSeePlayer(IEnumerator attack, bool isAttacking)
    {
        RaycastHit hit;
        if (Physics.Raycast(headPos.transform.position, playerDir, out hit, sightDist))
        {
            Debug.DrawRay(headPos.transform.position, playerDir);
            if (hit.collider.CompareTag("Player") && angle <= viewAngle)
            {
                if(!stationary)
                {
                    agent.speed = speedChase;
                    agent.stoppingDistance = stoppingDistanceOrig;
                    agent.SetDestination(gameManager.instance.player.transform.position);
                }
                if (agent.remainingDistance < agent.stoppingDistance)
                    facePlayer();

                if (!isAttacking)
                    StartCoroutine(attack);
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
        aud.PlayOneShot(enemyHurtAud[Random.Range(0, enemyHurtAud.Length - 1)], enemyHurtAudVol);

        if (HP <= 0)
        {
            gameManager.instance.checkEnemyTotal();
            anim.SetBool("Dead", true);
            if (cannonCtrl != null)
            {
                cannonCtrl.enabled = true;
                cannonCol.enabled = true;
                cannon.transform.parent = tempTrans;
            }
            col.enabled = false;
            agent.enabled = false;
            Destroy(gameObject, 5);
            Instantiate(drops[Random.Range(0, drops.Length - 1)], transform.position, transform.rotation);
        }
        else if (HP > 0)
            StartCoroutine(flashDamage());

    }

    IEnumerator shoot()
    {
        isShooting = true;
        anim.SetTrigger("attack");
        aud.PlayOneShot(enemyWeaponAud, enemyWeaponAudVol);
        Instantiate(bullet, attackPos.transform.position, transform.rotation);
        yield return new WaitForSeconds(attackRate);
        isShooting = false;
    }


    IEnumerator melee()
    {
        isMelee = true;
        if(gameManager.instance.player.transform.position.normalized.magnitude - transform.position.normalized.magnitude <= meleeAttackRange)
        {
            aud.PlayOneShot(enemyWeaponAud, enemyWeaponAudVol);
            anim.SetTrigger("attack");
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
        agent.SetDestination(gameManager.instance.player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!gameManager.instance.music.inCombat)
                gameManager.instance.music.inCombat = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            agent.stoppingDistance = 0;
            if (gameManager.instance.music.inCombat)
                gameManager.instance.music.inCombat = false;
        }

    }
}
