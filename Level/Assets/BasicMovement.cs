using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    int inputCheck;
    bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.instance.basicMoveUI.SetActive(true);
            gameManager.instance.hint.SetActive(false);
        }

        if(gameManager.instance.basicMoveUI.activeSelf)
            ObjectiveCheck();

        if(inputCheck == 4)
        {
            gameManager.instance.basicMoveUI.SetActive(false);
            gameManager.instance.objectiveComplete.SetActive(true);
        }
    }

    void ObjectiveCheck()
    {
        if (Input.GetKeyDown(KeyCode.W) && inputCheck == 0)
        {
            gameManager.instance.objectives[inputCheck].color = Color.green;
            inputCheck++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && inputCheck == 1)
        {
            gameManager.instance.objectives[inputCheck].color = Color.green;
            inputCheck++;
        }
        else if (Input.GetKeyDown(KeyCode.S) && inputCheck == 2)
        {
            gameManager.instance.objectives[inputCheck].color = Color.green;
            inputCheck++;
        }
        else if (Input.GetKeyDown(KeyCode.D) && inputCheck == 3)
        {
            gameManager.instance.objectives[inputCheck].color = Color.green;
            inputCheck++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.instance.hint.SetActive(true);
            playerInRange = true;
        }
    }
}
