using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDummy : MonoBehaviour, IDamage
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float dmg)
    {
        anim.SetTrigger("Hit");
        TutorialManager.instance.meleeEnemiesLeft--;
    }
}
