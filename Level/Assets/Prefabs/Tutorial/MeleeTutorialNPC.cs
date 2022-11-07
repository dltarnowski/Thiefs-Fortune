using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTutorialNPC : meleeEnemyAI
{
    public void OnDestroy()
    {
        TutorialManager.instance.meleeEnemiesLeft--;
    }
}
