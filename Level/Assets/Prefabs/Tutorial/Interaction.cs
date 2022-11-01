using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private Animator anim;
    public Transform target;

    private bool playerInRange;
    void Start()
    {
        anim = GetComponent<Animator>();
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
            if (Input.GetKeyDown(KeyCode.O))
            {
                anim.SetTrigger("Speak");
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                anim.SetTrigger("Idle");
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
