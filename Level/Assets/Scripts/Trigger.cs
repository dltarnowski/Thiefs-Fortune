using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trigger : MonoBehaviour, IDamage
{
    [SerializeField] ParticleSystem flame;

    bool fractured;

    void Start()
    {
        
    }

    void Update()
    {
        if(fractured)
        {
            if(flame.isPlaying)
                flame.Stop();
        }
    }

    public void takeDamage(int dmg)
    {
        transform.GetComponent<Fracture>().Trigger();
        fractured = true;
    }
}