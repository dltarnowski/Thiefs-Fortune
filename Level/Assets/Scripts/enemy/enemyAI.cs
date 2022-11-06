using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{
    [Header("----- Componenets -----")]
    [SerializeField] internal NavMeshAgent agent;
    [SerializeField] internal Renderer model;
    [SerializeField] internal Collider col;
    [SerializeField] internal Animator anim;
    [SerializeField] internal GameObject[] drops;

    [Header("----- Enemy Stats -----")]
    [SerializeField] internal float HP;
    [SerializeField] internal int speedChase;
    [SerializeField] internal int facePlayerSpeed;
    [SerializeField] internal int animLerpSpeed;
    [SerializeField] internal int sightDist;
    [SerializeField] internal int viewAngle;
    [SerializeField] internal float damagedDuration;
    [SerializeField] internal GameObject headPos;

    [Header("----- Weapon Stats -----")]
    [SerializeField] internal GameObject weapon;

    [Header("----- Roam Settings -----")]
    [SerializeField] internal bool linearRoam;
    [SerializeField] internal bool stationary;
    [SerializeField] internal bool noRotation;
    [SerializeField] internal int roamDist;
    [SerializeField] internal bool canRoam = true;

    [Header("----- Audio -----")]
    [SerializeField] internal AudioSource aud;
    [SerializeField] internal AudioClip[] enemyHurtAud;
    [Range(0, 1)] [SerializeField] internal float enemyHurtAudVol;
    [SerializeField] internal AudioClip[] enemyStepsAud;
    [Range(0, 1)] [SerializeField] internal float enemyStepsAudVol;
    [Range(0, 1)] [SerializeField] internal float enemyWeaponAudVol;

    internal bool playingSteps;
    internal Vector3 randomDirection;

    internal bool playerInRange;
    internal Color modelColor;
    internal Vector3 playerDir;
    internal float stoppingDistanceOrig;
    internal Vector3 startingPos;
    internal float angle;
    internal float speedPatrol;
    internal float currBlackSpot;


    void Start()
    {
        modelColor = model.material.color;
        stoppingDistanceOrig = agent.stoppingDistance;
        startingPos = transform.position;
        speedPatrol = agent.speed;
        currBlackSpot = gameManager.instance.blackspot.blackSpotMultiplier;
        if (!stationary && canRoam)
            roam();
    }
    public void movementAnimationChange()
    {
        if (linearRoam && !playerInRange)
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude * 0.5f, Time.deltaTime * animLerpSpeed));
        else
            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * animLerpSpeed));
    }

    public void blackSpotUpdate()
    {
        if (currBlackSpot != gameManager.instance.blackspot.blackSpotMultiplier)
        {
            currBlackSpot = gameManager.instance.blackspot.blackSpotMultiplier;
            HP = (int)(HP * (1 + gameManager.instance.blackspot.blackSpotMultiplier));
        }
    }
    public IEnumerator PlaySteps()
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

    public void roam()
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

    public void canSeePlayer(IEnumerator attack = null, bool isAttacking = true)
    {
        RaycastHit hit;
        if (Physics.Raycast(headPos.transform.position, playerDir, out hit, sightDist))
        {
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

    public void facePlayer()
    {
        playerDir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * facePlayerSpeed);
    }
    public virtual void takeDamage(float dmg)
    {
        HP -= dmg;
        if(enemyHurtAudVol > 0)
            aud.PlayOneShot(enemyHurtAud[Random.Range(0, enemyHurtAud.Length - 1)], enemyHurtAudVol);

        if(HP <= 0)
        {
            gameManager.instance.checkEnemyTotal();
            anim.SetBool("Dead", true);
            col.enabled = false;
            agent.enabled = false;
            Destroy(gameObject, 5);
            Instantiate(drops[Random.Range(0, drops.Length - 1)], transform.position, transform.rotation);
        }
        else if (HP > 0)
            StartCoroutine(flashDamage());

    }

    public IEnumerator flashDamage()
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
