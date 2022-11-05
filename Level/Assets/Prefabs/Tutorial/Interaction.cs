using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public bool playerInRange;

    public float facePlayerSpeed;
    private Animator anim;
    public Transform target;
    [SerializeField] GameObject Icon;

    Vector3 playerDir;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;

        if (playerInRange)
        {
            facePlayer(playerDir);
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E) && TutorialManager.instance.tutorialProgress < 5 && !TutorialManager.instance.tutorialActive)
        {
            TutorialManager.instance.dialogueBox.SetActive(true);
            TutorialManager.instance.beginButton.SetActive(true);
            gameManager.instance.hint.SetActive(false);

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            gameManager.instance.cameraScript.enabled = false;

            if (anim != null)
            {
                anim.SetTrigger("Speak");
                TutorialManager.instance.exclamation.SetActive(false);
            }

            if (TutorialManager.instance.basicMoveTrigger)
            {
                TutorialManager.instance.objectiveName.text = "Basic Movement";
                TutorialManager.instance.objectiveText.text = "Let's get shake off those sea legs. Press begin to learn basic movement within the world!";
            }
            if (TutorialManager.instance.advanceMoveTrigger)
            {
                TutorialManager.instance.objectiveName.text = "Advanced Movement";
                TutorialManager.instance.objectiveText.text = "I think you're ready for something harder. Press begin to learn advanced movement within the world!";
            }
            if (TutorialManager.instance.inventoryTrigger)
            {
                TutorialManager.instance.objectiveName.text = "Inventory";
                TutorialManager.instance.objectiveText.text = "You have now picked up your first new item! Press begin to learn the basic of accessing and managing your inventory!";
            }
            if (TutorialManager.instance.meleeTrigger)
            {
                TutorialManager.instance.objectiveName.text = "Melee Combat";
                TutorialManager.instance.objectiveText.text = "There are many dangers in the world. Press begin to learn how to use melee attacks!";
            }
            if (TutorialManager.instance.rangedTrigger)
            {
                TutorialManager.instance.objectiveText.text = "Sometimes, a gun is best. Press begin to learn how to use ranged attacks!";
            }
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
        if (other.CompareTag("Player"))
        {
            if (TutorialManager.instance.meleeTrigger && TutorialManager.instance.tutorialActive)
                gameManager.instance.hint.SetActive(false);
            else
                gameManager.instance.hint.SetActive(true);

            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        anim.SetTrigger("Idle");
        gameManager.instance.hint.SetActive(false);
        TutorialManager.instance.exclamation.SetActive(true);
    }
}
