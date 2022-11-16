using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [Header("----- Dialogue Box UI -----")]
    public GameObject dialogueBox;
    public TextMeshProUGUI objectiveName;
    public TextMeshProUGUI objectiveText;
    public GameObject beginButton;
    public GameObject completeButton;
    public GameObject continueButton;

    [Header("----- Objective Check UI -----")]
    public GameObject basicMoveUIObj;
    public GameObject advanceMoveUIObj;
    public GameObject inventoryUIObj;
    public GameObject meleeUIObj;
    public GameObject rangedUIObj;
    public Image[] basicMoveUI;
    public Image[] advanceMoveUI;
    public Image[] inventoryUI;
    public Image[] meleeUI;
    public Image[] rangedUI;

    [Header("----- Triggers -----")]
    public bool basicMoveTrigger;
    public bool advanceMoveTrigger;
    public bool inventoryTrigger;
    public bool combatTrigger;
    public bool rangedTrigger;
    public bool finalTrigger;

    [Header("----- Spawns -----")]
    public GameObject basicSpawn;
    public GameObject advanceSpawn;
    public GameObject inventorySpawn;
    public GameObject combatSpawn;
    public GameObject finalSpawn;

    [Header("----- Objectives -----")]
    public GameObject basicPoint;
    public GameObject advancePoint;
    public GameObject inventoryPoint;
    public GameObject combatPoint;
    public GameObject nextPoint;
    public GameObject finalPoint;

    [Header("----- Buttons -----")]
    public bool equipButton;
    public bool unequipButton;
    public bool activeTab;
    public bool currentTab;

    [Header("----- Other -----")]
    public GameObject ammoSpawner;
    public bool playerInRange;
    public GameObject skull;
    public GameObject exclamation;
    public GameObject ammoBag;
    public GameObject meleeSpawnerObj;
    public GameObject rangedSpawnerObj;
    public int objectivesComplete;
    public int tutorialProgress;
    public int meleeEnemiesLeft;
    public int rangedEnemiesLeft;
    public bool tutorialActive;
    public bool pickedUp;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        nextPoint = basicPoint;
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Interaction.instance.playerInRange;

        if (tutorialProgress == 0)
        {
            basicPoint.SetActive(true);
        }
        if (tutorialProgress == 1)
        {
            advancePoint.SetActive(true);
        }
        if(tutorialProgress == 2)
        {
            inventoryPoint.SetActive(true);
        }
        if(tutorialProgress == 3)
        {
            combatPoint.SetActive(true);
        }
        if(tutorialProgress == 4)
        {
            finalPoint.SetActive(true);
        }
    }

    public void Begin()
    {
        gameManager.instance.playerScript.anim.SetBool("Idle", false);
        gameManager.instance.cameraScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        beginButton.SetActive(false);

        if (basicMoveTrigger)
        {
            basicMoveUIObj.SetActive(true);
            objectiveText.text = "Let's start with some basic movement! You can look around with your mouse and can move through the world using [W], [A], [S], [D]. Let's try it now!";
        }
        else if (advanceMoveTrigger)
        {
            advanceMoveUIObj.SetActive(true);
            objectiveText.text = "Now let's take a look at some more " + '"' + "advanced" + '"' + " techniques. Use [SPACE] to Jump, Hold [SHIFT] while moving to sprint, and use [L-CTRL] to crouch";
        }
        else if (inventoryTrigger)
        {
            inventoryUIObj.SetActive(true);
            objectiveText.text = "Open your inventory by pressing [I]. Click (+) in the corner of your ammo to equip it. It'll now be in your active inventory. You can unequip with (-). Health pickups work the same." +
                "You can use AMMO by pressing [3] and HEALTH by pressing [4].";
        }
        else if (combatTrigger)
        {
            ammoSpawner.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameManager.instance.cameraScript.enabled = true;

            meleeUIObj.SetActive(true);
            rangedUIObj.SetActive(true);
            objectiveText.text = "Welcome to your very own Combat Range! On the left is your melee combat zone and to the right is ranged. Switch between weapons in your active inventory using [1] and [2]. Try to land two of each attack!";
        }
    }

    public void Continue()
    {
        gameManager.instance.playerScript.anim.SetBool("Idle", false);

        beginButton.SetActive(false);
        continueButton.SetActive(false);
        objectiveText.text = "Also? I just showed you the basics. You'll wanna take a look at the HELP OPTION in your PAUSE>SETTINGS MENU for all of the controls. I almost forgot! The boat behind me is yours. Safe travels and good luck!";

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager.instance.cameraScript.enabled = true;
        gameManager.instance.CurrentObjectiveMiniMapIcon();

        StartCoroutine(CleanUp());

    }

    public void Complete()
    {
        gameManager.instance.playerScript.anim.SetBool("Idle", false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager.instance.cameraScript.enabled = true;

        tutorialActive = false;

        gameManager.instance.playerScript.thirdPersonCam_Obj.SetActive(true);
        gameManager.instance.playerScript.thirdPersonCam_Obj.tag = "MainCamera";
        gameManager.instance.playerScript.firstPersonCam_Obj.SetActive(false);
        gameManager.instance.playerScript.firstPersonCam_Obj.tag = "SecondaryCamera";

        if (tutorialProgress == 1)
        {
            completeButton.SetActive(false);
            objectiveText.text = "Looks like your sea legs are land legs! Find me up a ways for your next lesson!";
            nextPoint.transform.position = advanceSpawn.transform.position;
        }
        if(tutorialProgress == 2)
        {
            completeButton.SetActive(false);
            ammoBag.SetActive(true);
            objectiveText.text = "Now, we all know that a pirate is only as good as the things he carries. Look for the floating bag of ammo up ahead and walk over it to pick it up. Then come find me!";
            nextPoint.transform.position = inventorySpawn.transform.position;
        }
        if (tutorialProgress == 3)
        {
            completeButton.SetActive(false);
            gameManager.instance.inventoryPanel.SetActive(false);
            gameManager.instance.cursorUnlockUnpause();
            objectiveText.text = "Now that you know how to stock yourself up, let's get prepared for some action!";
            nextPoint.transform.position = combatSpawn.transform.position;
        }
        if (tutorialProgress == 4)
        {
            ammoSpawner.SetActive(false);
            completeButton.SetActive(false);
            rangedUIObj.SetActive(false);
            meleeUIObj.SetActive(false);
            objectiveText.text = "Looks like you can handle yourself just fine! And with that, we're near the end of our lessons! Now that you've humored me, perhaps I can give you a hand. Come find me and I'll tell you what I know! Also? Come back anytime and practice!";
            nextPoint.transform.position = finalSpawn.transform.position;
        }

        StartCoroutine(CleanUp());
    }
    public IEnumerator CleanUp()
    {   
        objectivesComplete = 0;
        yield return new WaitForSeconds(1.3f);
        skull.SetActive(false);
        Interaction.instance.playerInRange = false;
        skull.transform.position = new Vector3(nextPoint.transform.position.x, nextPoint.transform.position.y, nextPoint.transform.position.z);

        if (tutorialProgress <= 4)
        {
            yield return new WaitForSeconds(.6f);
            skull.SetActive(true);
            exclamation.SetActive(true);
        }

        if (tutorialProgress == 5)
        {
            yield return new WaitForSeconds(5f);
            dialogueBox.SetActive(false);
        }
    }

    public void AnimationStop()
    {
        gameManager.instance.playerScript.anim.SetBool("IsRanged", false);
        gameManager.instance.playerScript.anim.SetBool("IsWalking", false);
        gameManager.instance.playerScript.anim.SetBool("IsInWater", false);
        gameManager.instance.player.GetComponent<CharacterController>().height = 2;
        gameManager.instance.playerScript.anim.SetBool("IsCrouched", false);
        gameManager.instance.playerScript.anim.ResetTrigger("IsJumping");
        gameManager.instance.playerScript.anim.SetFloat("Speed", 0);
        //gameManager.instance.playerScript.move.x = 0;
        gameManager.instance.playerScript.anim.SetBool("Idle", true);
    }
}
