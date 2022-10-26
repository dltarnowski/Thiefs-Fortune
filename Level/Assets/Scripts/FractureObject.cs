using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureObject : MonoBehaviour, IDamage
{
    public GameObject destroyedVersion;
    public ParticleSystem flame;
    public Light light;

    public void takeDamage(int damage)
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);

        if (flame.isPlaying)
            flame.Stop();

        light.enabled = false;

        Destroy(gameObject);

        gameManager.instance.CheckTowerTotal();
    }
}
