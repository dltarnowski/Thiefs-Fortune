using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTutorial : MonoBehaviour
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
        if (TutorialManager.instance.inventoryUIObj.activeSelf && Input.GetKeyDown(KeyCode.I) && TutorialManager.instance.inventoryUI[0].color != Color.green)
        {
            TutorialManager.instance.inventoryUI[0].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if (TutorialManager.instance.inventoryUIObj.activeSelf && TutorialManager.instance.equipButton && TutorialManager.instance.inventoryUI[1].color != Color.green)
        {
            TutorialManager.instance.inventoryUI[1].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if (TutorialManager.instance.inventoryUIObj.activeSelf && TutorialManager.instance.unequipButton && TutorialManager.instance.inventoryUI[2].color != Color.green)
        {
            TutorialManager.instance.inventoryUI[2].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }

        if (TutorialManager.instance.objectivesComplete == 3)
        {
            TutorialManager.instance.beginButton.SetActive(false);
            TutorialManager.instance.completeButton.SetActive(true);
            TutorialManager.instance.tutorialProgress = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.instance.inventoryTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TutorialManager.instance.inventoryTrigger = false;
    }
}
