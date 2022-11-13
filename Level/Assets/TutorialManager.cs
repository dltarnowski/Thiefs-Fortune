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
    public bool meleeTrigger;
    public bool rangedTrigger;
    public bool finalTrigger;

    [Header("----- Spawns -----")]
    public GameObject basicSpawn;
    public GameObject advanceSpawn;
    public GameObject inventorySpawn;

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

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        nextPoint = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialProgress == 0)
        {
            basicPoint.SetActive(true);
        }
        if (tutorialProgress == 1)
        {
            basicPoint.SetActive(false);
            advancePoint.SetActive(true);
        }
        if(tutorialProgress == 2)
        {
            advancePoint.SetActive(false);
            inventoryPoint.SetActive(true);
        }
        if(tutorialProgress == 3)
        {
            inventoryPoint.SetActive(false);
            combatPoint.SetActive(true);
        }
    }

    public void Begin()
    {
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
        /*else if (meleeTrigger)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameManager.instance.cameraScript.enabled = true;

            meleeSpawnerObj.SetActive(true);

            meleeUIObj.SetActive(true);
            objectiveText.text = "Make sure your melee weapon is equipped by pressing [2]. Kill the enemies by centering your body to the enemy and pressing the [L-MOUSE BUTTON]";
        }*/
        /*else if (rangedTrigger)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameManager.instance.cameraScript.enabled = true;

            rangedSpawnerObj.SetActive(true);
            skull.SetActive(false);

            rangedUIObj.SetActive(true);
            objectiveText.text = "Make sure your ranged weapon is equipped by pressing [1]. Kill the enemies by lining up your reticle and pressing the [L-MOUSE BUTTON]";
        }*/
    }

    public void Continue()
    {
        if (!finalTrigger)
        {
            meleeTrigger = false;
            rangedTrigger = true;

            continueButton.SetActive(false);
            beginButton.SetActive(true);

            meleeUIObj.SetActive(false);
            objectiveName.text = "Ranged Combat";
            objectiveText.text = "Sometimes, a gun is best. Press begin to learn how to use ranged attacks!";
        }
        else
        {
            beginButton.SetActive(false);
            continueButton.SetActive(false);
            objectiveText.text = "Also? I just showed you the basics. You'll wanna take a look at the HELP OPTION in your PAUSE>SETTINGS MENU for all of the controls. I almost forgot! The boat behind me is yours. Safe travels and good luck!";

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gameManager.instance.cameraScript.enabled = true;
            gameManager.instance.CurrentObjectiveMiniMapIcon();
            tutorialProgress = 6;

            StartCoroutine(CleanUp());
        }
    }

    public void Complete()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager.instance.cameraScript.enabled = true;

        if (tutorialProgress == 1)
        {
            completeButton.SetActive(false);
            basicMoveUIObj.SetActive(false);
            basicMoveTrigger = false;
            objectiveText.text = "Looks like your sea legs are land legs! Find me up a ways for your next lesson!";
            nextPoint.transform.position = advanceSpawn.transform.position;
        }
        if(tutorialProgress == 2)
        {
            completeButton.SetActive(false);
            advanceMoveUIObj.SetActive(false);
            ammoBag.SetActive(true);
            advanceMoveTrigger = false;
            objectiveText.text = "Now, we all know that a pirate is only as good as the things he carries. Look for the floating bag of ammo up ahead and walk over it to pick it up. Then come find me!";
            nextPoint.transform.position = inventorySpawn.transform.position;
        }
        if (tutorialProgress == 3)
        {
            completeButton.SetActive(false);
            inventoryUIObj.SetActive(false);
            gameManager.instance.inventoryPanel.SetActive(false);
            inventoryTrigger = false;

            objectiveText.text = "Now that you know how to stock yourself up, let's get prepared for some action!";
            nextPoint.transform.position = combatPoint.transform.position;
        }
        if (tutorialProgress == 5)
        {
            completeButton.SetActive(false);
            rangedUIObj.SetActive(false);
            objectiveText.text = "You're quite the sharpshooter! And with that, we're near the end of our lessons! Now that you've humored me, perhaps I can give you a hand. Come find me and I'll tell you what I know!";
            nextPoint.transform.position = finalPoint.transform.position;
            rangedTrigger = false;
            finalPoint.SetActive(true);
        }

        StartCoroutine(CleanUp());
    }
    public IEnumerator CleanUp()
    {   
        objectivesComplete = 0;
        yield return new WaitForSeconds(1.5f);
        skull.SetActive(false);
        skull.transform.position = new Vector3(nextPoint.transform.position.x, nextPoint.transform.position.y, nextPoint.transform.position.z);

        if (!finalTrigger && tutorialProgress <= 5)
        {
            yield return new WaitForSeconds(1.2f);
            skull.SetActive(true);
            exclamation.SetActive(true);
        }
        yield return new WaitForSeconds(2f);
        dialogueBox.SetActive(false);

        tutorialActive = false;

    }

    public void AnimationStop()
    {
        gameManager.instance.playerScript.anim.SetBool("IsRanged", false);
        gameManager.instance.playerScript.anim.SetBool("IsWalking", false);
        gameManager.instance.playerScript.anim.SetBool("IsInWater", false);
    }
}
