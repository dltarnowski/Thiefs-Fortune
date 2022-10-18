using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trigger : MonoBehaviour, IDamage
{
    [SerializeField] ParticleSystem flame;
    [SerializeField] Light light;

    bool fractured;

    void Start()
    {

    }

    void Update()
    {
        if (fractured)
        {
            if (flame.isPlaying)
                flame.Stop();

            light.enabled = false;
        }
    }

    public void takeDamage(int dmg)
    {
        transform.GetComponent<Fracture>().Trigger();
        fractured = true;
    }
}