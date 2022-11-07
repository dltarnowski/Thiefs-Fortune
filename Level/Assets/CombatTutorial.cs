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

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                gameManager.instance.cameraScript.enabled = false;
                TutorialManager.instance.objectiveText.text = "That was a swing and a hit, but perhaps a little close for comfort. Let's move onto some ranged attacks.";

                TutorialManager.instance.objectivesComplete = 0;
            }
        }
        else if(TutorialManager.instance.rangedTrigger == true)
        {
            if (TutorialManager.instance.rangedUIObj.activeSelf && TutorialManager.instance.rangedEnemiesLeft <= 1 && TutorialManager.instance.rangedUI[0].color != Color.green)
            {
                TutorialManager.instance.rangedUI[0].color = Color.green;
                TutorialManager.instance.objectivesComplete++;
            }
            else if (TutorialManager.instance.rangedUIObj.activeSelf && TutorialManager.instance.rangedEnemiesLeft <= 0 && TutorialManager.instance.rangedUI[1].color != Color.green)
            {
                TutorialManager.instance.rangedUI[1].color = Color.green;
                TutorialManager.instance.objectivesComplete++;
            }

            if (TutorialManager.instance.objectivesComplete == 2)
            {
                TutorialManager.instance.beginButton.SetActive(false);
                TutorialManager.instance.completeButton.SetActive(true);
                TutorialManager.instance.tutorialProgress = 5;

                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                gameManager.instance.cameraScript.enabled = false;
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
