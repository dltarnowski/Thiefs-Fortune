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
        if (TutorialManager.instance.combatTrigger == true)
        {
            if (TutorialManager.instance.objectivesComplete == 4)
            {
                TutorialManager.instance.AnimationStop();
                gameManager.instance.cameraScript.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;

                TutorialManager.instance.beginButton.SetActive(false);
                TutorialManager.instance.completeButton.SetActive(true);
                TutorialManager.instance.tutorialProgress = 4;
            }
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
