using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicMovement : MonoBehaviour
{
    int inputCheck;

    public Image[] Checks;

    private void Update()
    {
        if (gameManager.instance.TutorialCollide == true && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.instance.basicMoveUI.SetActive(true);
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
            Checks[inputCheck].color = Color.green;
            inputCheck++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && inputCheck == 1)
        {
            Checks[inputCheck].color = Color.green;
            inputCheck++;
        }
        else if (Input.GetKeyDown(KeyCode.S) && inputCheck == 2)
        {
            Checks[inputCheck].color = Color.green;
            inputCheck++;
        }
        else if (Input.GetKeyDown(KeyCode.D) && inputCheck == 3)
        {
            Checks[inputCheck].color = Color.green;
            inputCheck++;
        }
    }
}
