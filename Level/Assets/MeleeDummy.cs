using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDummy : MonoBehaviour, IDamage
{
    public Animator anim;

    public void takeDamage(float dmg)
    {
        anim.SetTrigger("Hit");
        TutorialManager.instance.meleeEnemiesLeft--;

        if (TutorialManager.instance.meleeEnemiesLeft == 1)
        {
            TutorialManager.instance.meleeUI[0].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if(TutorialManager.instance.meleeEnemiesLeft == 0)
        {
            TutorialManager.instance.meleeUI[1].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
    }
}
