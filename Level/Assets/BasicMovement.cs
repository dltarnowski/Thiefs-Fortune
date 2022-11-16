using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
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
        if(TutorialManager.instance.basicMoveUIObj.activeSelf && Input.GetKeyDown(KeyCode.W) && TutorialManager.instance.basicMoveUI[0].color != Color.green && TutorialManager.instance.tutorialActive)
        {
            TutorialManager.instance.basicMoveUI[0].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if (TutorialManager.instance.basicMoveUIObj.activeSelf && Input.GetKeyDown(KeyCode.A) && TutorialManager.instance.basicMoveUI[1].color != Color.green && TutorialManager.instance.tutorialActive)
        {
            TutorialManager.instance.basicMoveUI[1].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if (TutorialManager.instance.basicMoveUIObj.activeSelf && Input.GetKeyDown(KeyCode.S) && TutorialManager.instance.basicMoveUI[2].color != Color.green && TutorialManager.instance.tutorialActive)
        {
            TutorialManager.instance.basicMoveUI[2].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if (TutorialManager.instance.basicMoveUIObj.activeSelf && Input.GetKeyDown(KeyCode.D) && TutorialManager.instance.basicMoveUI[3].color != Color.green && TutorialManager.instance.tutorialActive)
        {
            TutorialManager.instance.basicMoveUI[3].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }

        if (TutorialManager.instance.objectivesComplete == 4)
        {
            TutorialManager.instance.AnimationStop();
            gameManager.instance.cameraScript.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            TutorialManager.instance.beginButton.SetActive(false);
            TutorialManager.instance.completeButton.SetActive(true);
            TutorialManager.instance.basicMoveUIObj.SetActive(false);
            TutorialManager.instance.basicPoint.SetActive(false);
            TutorialManager.instance.basicMoveTrigger = false;
            TutorialManager.instance.tutorialProgress = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TutorialManager.instance.basicMoveTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TutorialManager.instance.basicMoveTrigger = false;
    }
}
