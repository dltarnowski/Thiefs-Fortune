using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
    public AudioSource destructionSFX;
    // Start is called before the first frame update
    void Start()
    {
        destructionSFX.Play();
        Destroy(gameObject, 10);

    }
}
