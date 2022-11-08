using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    public float facePlayerSpeed;
    public Animator anim;
    public GameObject cam;

    Vector3 playerDir;
    Vector3 originalNPC;

    private void Start()
    {
        originalNPC = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
    private void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;

        if(gameManager.instance.npcCollide)
        {
            facePlayer(playerDir);
            anim.SetBool("inRange", true);
        }
        else
        {
            facePlayer(originalNPC);
            anim.SetBool("inRange", false);
        }
    }

    public void facePlayer(Vector3 dir)
    {
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * facePlayerSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.instance.hint.SetActive(true);

        if (other.CompareTag("Player"))
        {
            gameManager.instance.npcCollide = true;
            gameManager.instance.npcCam = cam;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hint.SetActive(false);

        if (other.CompareTag("Player"))
        {
            gameManager.instance.npcCollide = false;
        }
    }
}