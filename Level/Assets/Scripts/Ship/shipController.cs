using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipController : MonoBehaviour
{
    [SerializeField] GameObject shipCam;
    [SerializeField] GameObject[] sails;
    [SerializeField] GameObject playerPos;
    [SerializeField] shipMovement shipMove;

    public bool controllingShip;
    bool onShip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onShip && Input.GetKeyDown(KeyCode.R))
        {
            gameManager.instance.playerScript.enabled = controllingShip;
            gameManager.instance.mainCamera.SetActive(controllingShip);
            for (int i = 0; i < sails.Length; i++)
                sails[i].SetActive(!controllingShip);
            shipMove.enabled = !controllingShip;
            shipCam.SetActive(!controllingShip);
            controllingShip = !controllingShip;
        }
        if (shipCam.activeSelf)
            ChangeParent();
        else
        {
            RevertParent();
        }
    }

    void ChangeParent()
    {
        gameManager.instance.player.transform.position = playerPos.transform.position;
        gameManager.instance.player.transform.rotation = playerPos.transform.rotation;
        gameManager.instance.player.transform.parent = transform;
    }

    void RevertParent()
    {
        gameManager.instance.player.transform.parent = null;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onShip = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onShip = false;
        }
    }
}
