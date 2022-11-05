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
    }

    public void ObjectiveCheck()
    {
        if (TutorialManager.instance.meleeTrigger == true)
        {
            if (TutorialManager.instance.meleeUIObj.activeSelf && TutorialManager.instance.meleeEnemiesLeft == 1 && TutorialManager.instance.meleeUI[0].color != Color.green)
            {
                TutorialManager.instance.meleeUI[0].color = Color.green;
                TutorialManager.instance.objectivesComplete++;
            }
            else if (TutorialManager.instance.meleeUIObj.activeSelf && TutorialManager.instance.meleeEnemiesLeft == 0 && TutorialManager.instance.meleeUI[1].color != Color.green)
            {
                TutorialManager.instance.meleeUI[1].color = Color.green;
                TutorialManager.instance.objectivesComplete++;
            }

            if (TutorialManager.instance.objectivesComplete == 2)
            {
                TutorialManager.instance.beginButton.SetActive(false);
                TutorialManager.instance.continueButton.SetActive(true);
                TutorialManager.instance.tutorialProgress = 4;
            }
        }
        else if(TutorialManager.instance.rangedTrigger == true)
        {
            TutorialManager.instance.objectivesComplete = 0;

            if (TutorialManager.instance.rangedUIObj.activeSelf && Input.GetKeyDown(KeyCode.Space) && TutorialManager.instance.rangedUI[0].color != Color.green)
            {
                TutorialManager.instance.rangedUI[0].color = Color.green;
                TutorialManager.instance.objectivesComplete++;
            }
            else if (TutorialManager.instance.rangedUIObj.activeSelf && Input.GetKeyDown(KeyCode.LeftShift) && TutorialManager.instance.rangedUI[1].color != Color.green)
            {
                TutorialManager.instance.rangedUI[1].color = Color.green;
                TutorialManager.instance.objectivesComplete++;
            }

            if (TutorialManager.instance.objectivesComplete == 2)
            {
                TutorialManager.instance.beginButton.SetActive(false);
                TutorialManager.instance.completeButton.SetActive(true);
                TutorialManager.instance.tutorialProgress = 5;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.instance.meleeTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TutorialManager.instance.meleeTrigger = false;
    }
}
