using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject playerCam;
    [SerializeField] GameObject cannonCamera;
    [SerializeField] GameObject playerPos;
    bool active;
    public bool cannonNear;
    bool isShooting;
    Transform tempTrans;
    [SerializeField] GameObject cannonBall;
    [SerializeField] Transform cannonBallPos;
    [SerializeField] float shootRate;
    [SerializeField] GunStats cannonStats;

    ParticleSystem cannonSmoke;

    private void Start()
    {
        active = false;
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        cannonSmoke = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cannonNear)
        {
            playerCam.SetActive(active);
            gameManager.instance.player.GetComponent<CharacterController>().enabled = active;
            gameManager.instance.playerScript.enabled = active;
            cannonCamera.SetActive(!active);
            active = !active;
        }
        if (cannonCamera.activeSelf)
            ChangeParent();
        else
            RevertParent();

        StartCoroutine(shoot());
    }

    void ChangeParent()
    {
        tempTrans = gameManager.instance.player.transform.parent;
        gameManager.instance.player.transform.parent = transform;
        gameManager.instance.player.transform.position = playerPos.transform.position;
        gameManager.instance.player.transform.rotation = playerPos.transform.rotation;
    }

    //Revert the parent of object 2.
    void RevertParent()
    {
        gameManager.instance.player.transform.parent = tempTrans;

    }
    IEnumerator shoot()
    {
        if (Input.GetButton("Fire1") && !isShooting && cannonCamera.activeSelf)
        {
            isShooting = true;
            Instantiate(cannonBall, cannonBallPos.transform.position, transform.rotation);

            cannonSmoke.transform.localPosition = cannonStats.muzzleLocations[0].localPosition;
            cannonSmoke.Play();

            Debug.Log("Shoot!");
            yield return new WaitForSeconds(shootRate);
            isShooting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            cannonNear = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            cannonNear = false;
        }
    }

    
}
