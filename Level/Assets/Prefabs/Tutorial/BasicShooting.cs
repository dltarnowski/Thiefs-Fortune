using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicShooting : MonoBehaviour
{
    int inputCheck;

    public Image[] Checks;

    private void Update()
    {
        if (gameManager.instance.TutorialCollide == true && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.instance.basicMoveUI.SetActive(true);
        }

        if (gameManager.instance.basicMoveUI.activeSelf)
            ObjectiveCheck();

        if (inputCheck == 4)
        {
            gameManager.instance.basicMoveUI.SetActive(false);
            gameManager.instance.objectiveComplete.SetActive(true);
            gameManager.instance.objectiveComplete.SetActive(false);
        }
    }

    void ObjectiveCheck()
    {
        /*if (Input.GetKeyDown("") && inputCheck == 0)
        {
            Checks[inputCheck].color = Color.green;
            inputCheck++;
        }*/
    }
}
