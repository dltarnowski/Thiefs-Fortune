using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTutorialNPC : rangedEnemyAI
{
    public void OnDestroy()
    {
        TutorialManager.instance.rangedUI[0].color = Color.green;
        TutorialManager.instance.objectivesComplete++;
        TutorialManager.instance.rangedEnemiesLeft--;
    }
}
