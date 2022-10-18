using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] GameObject[] objective;
    public bool colliderCheck;
    int currentObjective;

    void Update()
    {
        if(currentObjective < objective.Length - 1)
        {
            if (objective[currentObjective].GetComponent<Objective1>().finished)
            {
                currentObjective++;
                objective[currentObjective].GetComponent<BoxCollider>().isTrigger = true;
            } 
        }
    }
}
