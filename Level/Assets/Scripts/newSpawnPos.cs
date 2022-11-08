using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSpawnPos : MonoBehaviour
{
    bool changed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialManager.instance.tutorialProgress == 5 && !changed)
        {
            changed = true;
            gameManager.instance.spawnPosition = this.gameObject;
        }
    }
}
