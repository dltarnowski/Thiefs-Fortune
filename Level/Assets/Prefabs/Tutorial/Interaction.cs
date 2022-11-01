using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public bool PlayerInRange;

    private Animator anim;
    public Transform target;
    [SerializeField] GameObject Icon;
    [SerializeField] GameObject Icon2;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        Icon2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && PlayerInRange == true)
        {
            transform.LookAt(target);
            gameManager.instance.hint.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.instance.basicMoveUI.SetActive(true);
            if (anim != null)
            {
                anim.SetTrigger("Speak");
                gameManager.instance.hint.SetActive(false);
                Icon.SetActive(false);
                Icon2.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerInRange = false;
        anim.SetTrigger("Idle");
        gameManager.instance.hint.SetActive(true);
        Icon.SetActive(true);
        Icon2.SetActive(false);
    }
}
