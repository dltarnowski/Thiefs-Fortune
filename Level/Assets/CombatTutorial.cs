using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TutorialManager.instance.objectivesComplete = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ObjectiveCheck();
        Debug.Log(TutorialManager.instance.objectivesComplete);
    }

    public void ObjectiveCheck()
    {
        if (TutorialManager.instance.objectivesComplete == 4)
        {
            TutorialManager.instance.AnimationStop();
            gameManager.instance.cameraScript.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            //TutorialManager.instance.objectiveText.text = "Great job! That's all I can teach you. Now to help you: The captain you're looking for was last seen on Chicken Head Enclave. You can open your map by pressing [M]. Grab that boat and be on your way!";
            TutorialManager.instance.beginButton.SetActive(false);
            TutorialManager.instance.completeButton.SetActive(true);
            TutorialManager.instance.rangedUIObj.SetActive(false);
            TutorialManager.instance.meleeUIObj.SetActive(false);
            TutorialManager.instance.combatPoint.SetActive(false);
            TutorialManager.instance.combatTrigger = false;
            TutorialManager.instance.tutorialProgress = 4;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.instance.combatTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TutorialManager.instance.combatTrigger = false;
    }
}
