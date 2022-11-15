using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceMovement : MonoBehaviour
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
        if (TutorialManager.instance.advanceMoveUIObj.activeSelf && Input.GetKeyDown(KeyCode.Space) && TutorialManager.instance.advanceMoveUI[0].color != Color.green)
        {
            TutorialManager.instance.advanceMoveUI[0].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if (TutorialManager.instance.advanceMoveUIObj.activeSelf && Input.GetKeyDown(KeyCode.LeftShift) && TutorialManager.instance.advanceMoveUI[1].color != Color.green)
        {
            TutorialManager.instance.advanceMoveUI[1].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if (TutorialManager.instance.advanceMoveUIObj.activeSelf && Input.GetKeyDown(KeyCode.LeftControl) && TutorialManager.instance.advanceMoveUI[2].color != Color.green)
        {
            TutorialManager.instance.advanceMoveUI[2].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }

        if (TutorialManager.instance.objectivesComplete == 3)
        {
            TutorialManager.instance.AnimationStop();
            Cursor.lockState = CursorLockMode.Confined;
            gameManager.instance.cameraScript.enabled = false;
            Cursor.visible = true;

            TutorialManager.instance.beginButton.SetActive(false);
            TutorialManager.instance.completeButton.SetActive(true);
            TutorialManager.instance.advanceMoveUIObj.SetActive(false);
            TutorialManager.instance.advancePoint.SetActive(false);
            TutorialManager.instance.advanceMoveTrigger = false;
            TutorialManager.instance.tutorialProgress = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.instance.advanceMoveTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TutorialManager.instance.advanceMoveTrigger = false;
    }
}
