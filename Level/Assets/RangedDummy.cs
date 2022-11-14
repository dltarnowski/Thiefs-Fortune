using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDummy : MonoBehaviour, IDamage
{
    public Animator anim;

    public void takeDamage(float dmg)
    {
        anim.SetTrigger("Hit");
        TutorialManager.instance.rangedEnemiesLeft--;

        if (TutorialManager.instance.rangedEnemiesLeft == 1)
        {
            TutorialManager.instance.rangedUI[0].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
        else if (TutorialManager.instance.rangedEnemiesLeft == 0)
        {
            TutorialManager.instance.rangedUI[1].color = Color.green;
            TutorialManager.instance.objectivesComplete++;
        }
    }
}
