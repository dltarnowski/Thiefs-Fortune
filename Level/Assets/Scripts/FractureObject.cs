using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureObject : MonoBehaviour, IDamage
{
    public GameObject destroyedVersion;
    public ParticleSystem flame;
    public Light fireLight;

    public void takeDamage(float damage)
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);

        if (flame.isPlaying)
            flame.Stop();

        fireLight.enabled = false;

        Destroy(gameObject);

        gameManager.instance.CheckTowerTotal();
    }
}
