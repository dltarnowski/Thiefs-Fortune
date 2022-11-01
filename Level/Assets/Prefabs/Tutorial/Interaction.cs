using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private Animator anim;
    public Transform target;
    [SerializeField] GameObject Icon;
    [SerializeField] GameObject Icon2;
    private bool playerInRange;

    void Start()
    {
        anim = GetComponent<Animator>();
        Icon2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && playerInRange == true)
        {
            transform.LookAt(target);
        }
        if (anim != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetTrigger("Speak");
                gameManager.instance.hint.SetActive(false);
                gameManager.instance.playerScript.enabled = false;
                Icon.SetActive(false);
                Icon2.SetActive(true);

            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                anim.SetTrigger("Idle");
                gameManager.instance.hint.SetActive(true);
                gameManager.instance.playerScript.enabled = true;
                Icon.SetActive(true);
                Icon2.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }
}
