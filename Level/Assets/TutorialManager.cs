using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [Header("----- Objectives -----")]
    public GameObject basicPoint;
    public GameObject advancePoint;
    public GameObject inventoryPoint;
    public GameObject meleePoint;
    public GameObject rangedPoint;
    public GameObject nextPoint;

    [Header("----- Other -----")]
    public bool playerInRange;
    public GameObject skull;
    public int objectivesComplete;
    public int tutorialProgress;

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
    }

    public void Begin()
    {
        beginButton.SetActive(false);

        if (basicMoveTrigger)
        {
            basicMoveUIObj.SetActive(true);
            objectiveText.text = "Let's start with some basic movement! You can look around with your mouse and can move through the world using [W], [A], [S], [D]. Let's try it now!";
        }
        else if(advanceMoveTrigger)
        {
            basicMoveUIObj.SetActive(true);
            objectiveText.text = "Now let's take a look at some more " + '"' + "advanced" + '"' + " techniques. Use [SPACE] to Jump, Hold [SHIFT] while moving to sprint, and use [L-CTRL] to crouch";
        }
        else if (inventoryTrigger)
        {
            inventoryUIObj.SetActive(true);
            objectiveText.text = "Now go ahead and open your inventory by pressing [I]. Click the (+) in the corner of your ammo to equip it. It should now be in your active inventory. You can unequip from here with (-)";
        }
        else if (meleeTrigger)
        {
            meleeUIObj.SetActive(true);
            objectiveText.text = "Make sure your melee weapon is equipped by pressing [1]. Kill the enemies by lining up your reticle and pressing the [L-MOUSE BUTTON]";
        }
        else if (rangedTrigger)
        {
            rangedUIObj.SetActive(true);
            objectiveText.text = "Make sure your ranged weapon is equipped by pressing [2]. Kill the enemies by lining up your reticle and pressing the [L-MOUSE BUTTON]";
        }
    }

    public void Complete()
    {
        if (tutorialProgress == 1)
        {
            basicMoveUIObj.SetActive(false);
            completeButton.SetActive(false);
            objectiveText.text = "Looks like your sea legs are land legs! Find me up a ways for your next lesson!";
            gameManager.instance.NpcUnpause();
            nextPoint.transform.position = advancePoint.transform.position;
            StartCoroutine(CleanUp());
        }
        else if(tutorialProgress == 2)
        {
            advanceMoveUIObj.SetActive(false);
            objectiveText.text = "Now that you know how to move, let's move onto the next lesson!";
        }
        else if (tutorialProgress == 3)
        {
            inventoryUIObj.SetActive(false);
            objectiveText.text = "Now that you know how to stock yourself up, let's get prepared for some action!";
        }
        else if(tutorialProgress == 4)
        {
            meleeUIObj.SetActive(false);
            objectiveText.text = "That was a swing and a hit, but perhaps a little close for comfort. Let's move onto some ranged attacks.";
        }
        else if(tutorialProgress == 5)
        {
            objectiveText.text = "You're quite the sharpshooter! And with that, we're near the end of our lessons! Now that you've humored me, perhaps I can give you a hand. Come find me and I'll tell you what I know!";
        }
    }
    public IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(3f);
        dialogueBox.SetActive(false);
        skull.SetActive(false);
        yield return new WaitForSeconds(1f);
        Instantiate(skull, nextPoint.transform.position, nextPoint.transform.rotation);
    }
}
