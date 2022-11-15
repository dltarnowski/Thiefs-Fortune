using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTutorialNPC : meleeEnemyAI
{
    public void OnDestroy()
    {
        TutorialManager.instance.meleeUI[0].color = Color.green;
        TutorialManager.instance.objectivesComplete++;
        TutorialManager.instance.meleeEnemiesLeft--;
    }
}
