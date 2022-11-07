using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TutorialManager.instance.finalTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        TutorialManager.instance.finalTrigger = false;
    }
}
