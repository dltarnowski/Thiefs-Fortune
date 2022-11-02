using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public bool playerInRange;

    private Animator anim;
    public Transform target;
    [SerializeField] GameObject Icon;
    

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
            gameManager.instance.hint.SetActive(true);
        }
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TutorialManager.instance.dialogueBox.SetActive(true);
            TutorialManager.instance.beginButton.SetActive(true);
            gameManager.instance.NpcPause();

            if (anim != null)
            {
                anim.SetTrigger("Speak");
                gameManager.instance.hint.SetActive(false);
                Icon.SetActive(false);
            }

            if (TutorialManager.instance.basicMoveTrigger)
            {
                TutorialManager.instance.objectiveName.text = "Basic Movement";
                TutorialManager.instance.objectiveText.text = "Let's get shake off those sea legs. Press begin to learn basic movement within the world!";
            }
            if (TutorialManager.instance.advanceMoveTrigger)
            {
                TutorialManager.instance.objectiveText.text = "I think you're ready for something harder. Press begin to learn advanced movement within the world!";
            }
            if (TutorialManager.instance.inventoryTrigger)
            {
                TutorialManager.instance.objectiveText.text = "See the ammo pouch floating up ahead? Why don't you walk over it? Press begin to learn the basic of accessing and managing your inventory!";
            }
            if (TutorialManager.instance.meleeTrigger)
            {
                TutorialManager.instance.objectiveText.text = "There are many dangers in the world. Press begin to learn how to use melee attacks!";
            }
            if (TutorialManager.instance.rangedTrigger)
            {
                TutorialManager.instance.objectiveText.text = "Sometimes, a gun is best. Press begin to learn how to use ranged attacks!";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.hint.SetActive(true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        anim.SetTrigger("Idle");
        gameManager.instance.hint.SetActive(false);
        Icon.SetActive(true);
    }
}
