using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] Step;
    private int Stepindex;
    int inputCheck;
    public Image[] Checks;
    public float wait = 5f;

    public static TutorialManager instance;


    void Update()
    {
        StartTutorial();
    }
    public void StartTutorial()
    {
        for (int i = 0; i < Step.Length; i++)
        {
            if (i == Stepindex)
            {
                Step[Stepindex].SetActive(true);
            }
            else
            {
                Step[Stepindex].SetActive(false);
            }
            if (Stepindex == 0)
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
               
                wait -= Time.deltaTime;
                if(wait <= 0)
                    Stepindex++;
            }
            else if (Stepindex == 1 && wait <= 0)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Checks[inputCheck].color = Color.green;
                    inputCheck++;
                    gameManager.instance.objectiveComplete.SetActive(false);
                }
                Stepindex++;
            }
            Debug.Log(Stepindex);
        }
       
    }
}
