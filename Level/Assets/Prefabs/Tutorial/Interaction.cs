using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public static Interaction instance;
    public bool playerInRange;

    public float facePlayerSpeed;
    private Animator anim;
    public Transform target;
    [SerializeField] GameObject Icon;

    Vector3 playerDir;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialManager.instance.finalTrigger)
        {
            TutorialManager.instance.tutorialActive = false;
        }

        playerDir = gameManager.instance.player.transform.position - transform.position;

        if (playerInRange)
        {
            facePlayer(playerDir);
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E) && TutorialManager.instance.tutorialProgress <= 5 && !TutorialManager.instance.tutorialActive)
        {
            TutorialManager.instance.AnimationStop();
            TutorialManager.instance.tutorialActive = true;

            gameManager.instance.cameraScript.enabled = false;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            if (anim != null)
            {
                anim.SetTrigger("Speak");
                TutorialManager.instance.exclamation.SetActive(false);
            }

            if (TutorialManager.instance.basicMoveTrigger)
            {
                InteractionBegin();

                TutorialManager.instance.objectiveName.text = "Basic Movement";
                TutorialManager.instance.objectiveText.text = "Let's get shake off those sea legs. Press begin to learn basic movement within the world!";
            }
            if (TutorialManager.instance.advanceMoveTrigger && TutorialManager.instance.tutorialProgress < 2)
            {
                InteractionBegin();

                TutorialManager.instance.objectiveName.text = "Advanced Movement";
                TutorialManager.instance.objectiveText.text = "I think you're ready for something harder. Press begin to learn advanced movement within the world!";
            }
            if (TutorialManager.instance.inventoryTrigger && TutorialManager.instance.tutorialProgress < 3)
            {
                InteractionBegin();

                TutorialManager.instance.objectiveName.text = "Inventory";
                TutorialManager.instance.objectiveText.text = "You have now picked up your first new item! Press begin to learn the basic of accessing and managing your inventory!";
            }
            if (TutorialManager.instance.combatTrigger && TutorialManager.instance.tutorialProgress < 4)
            {
                InteractionBegin();

                TutorialManager.instance.objectiveName.text = "Combat";
                TutorialManager.instance.objectiveText.text = "There are many dangers in the world. Press begin to learn how to use melee and ranged attacks!";
            }
            if (TutorialManager.instance.finalTrigger && TutorialManager.instance.tutorialProgress <= 5)
            {
                InteractionBegin();

                TutorialManager.instance.objectiveName.text = "Final Thoughts";
                TutorialManager.instance.objectiveText.text = "The man you're looking for? Captain Noble? Heard he was camped out on Chicken Head Enclave. (Press [M] to open your map and check). Now, that was six months ago. But it might be a good place to start.";
                TutorialManager.instance.continueButton.SetActive(true);
                TutorialManager.instance.tutorialProgress = 5;
            }
        }
        else if(!playerInRange && !TutorialManager.instance.tutorialActive)
        {
            TutorialManager.instance.beginButton.SetActive(false);
            TutorialManager.instance.continueButton.SetActive(false);
            TutorialManager.instance.completeButton.SetActive(false);
        }
    }

    public void facePlayer(Vector3 dir)
    {
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * facePlayerSpeed);
    }

    public void InteractionBegin()
    {
        TutorialManager.instance.dialogueBox.SetActive(true);
        TutorialManager.instance.beginButton.SetActive(true);
        gameManager.instance.hint.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if(TutorialManager.instance.finalPoint.activeSelf)
            {
                TutorialManager.instance.finalTrigger = true;
            }
            if (!TutorialManager.instance.tutorialActive)
            {
                if (TutorialManager.instance.dialogueBox.activeSelf)
                {
                    TutorialManager.instance.dialogueBox.SetActive(false);
                }
                gameManager.instance.hint.SetActive(true);
            }

            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        TutorialManager.instance.finalTrigger = false;
        anim.SetTrigger("Idle");
        gameManager.instance.hint.SetActive(false);

        if(!TutorialManager.instance.tutorialActive)
        {
            TutorialManager.instance.exclamation.SetActive(true);
            playerInRange = false;
        }
    }
}
