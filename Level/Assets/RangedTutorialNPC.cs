using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTutorialNPC : rangedEnemyAI
{
    public void OnDestroy()
    {
        TutorialManager.instance.rangedEnemiesLeft--;
    }
}
