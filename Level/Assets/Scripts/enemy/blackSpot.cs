using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackSpot : MonoBehaviour
{
    [Range(0f , 1f)] public float blackSpotMultiplier;
    [SerializeField] float raidTimer;
    [SerializeField] GameObject spawner;
    float spawnChance;
    int timesSpawned;
    bool isSpawning;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawning && blackSpotMultiplier > 0)
            StartCoroutine(raid());
    }

    IEnumerator raid()
    {
        isSpawning = true;
        spawnChance = Random.Range(0, 1f);
        if(spawnChance < blackSpotMultiplier)
            Instantiate(spawner, gameManager.instance.player.transform.position, gameManager.instance.player.transform.rotation);
        else
            timesSpawned++;
        yield return new WaitForSeconds(raidTimer / blackSpotMultiplier);
        isSpawning = false;
    }
}
