using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSpawnPos : MonoBehaviour
{
    bool changed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!changed && other.CompareTag("Player"))
        {
            changed = true;
            gameManager.instance.spawnPosition = this.gameObject;
        }
    }
}
