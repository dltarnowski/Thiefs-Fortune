using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject playerCam;
    [SerializeField] GameObject cannonCamera;
    [SerializeField] GameObject playerPos;
    bool active;
    bool cannonNear;
    bool isShooting;
    bool followCannon;
    Transform tempTrans;
    [SerializeField] GameObject cannonBall;
    [SerializeField] Transform cannonBallPos;
    [SerializeField] float shootRate;

    private void Start()
    {
        active = false;
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
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
