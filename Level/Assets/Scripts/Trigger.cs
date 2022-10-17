using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trigger : MonoBehaviour, IDamage
{
    void Start()
    {
        
    }

    public void takeDamage(int dmg)
    {
        transform.GetComponent<Fracture>().Trigger();
    }
}