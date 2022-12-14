using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] int timer;
    [SerializeField] int maxEnemies;
    [SerializeField] GameObject[] enemy;
    [SerializeField] Transform[] spawnPos;
    int enemiesSpawned;
    bool isSpawning;
    bool startSpawning;

    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.EnemyNumber += maxEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpawning && !isSpawning && enemiesSpawned < maxEnemies)
            StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        isSpawning = true;
        if(enemiesSpawned < enemy.Length)
            Instantiate(enemy[enemiesSpawned], spawnPos[enemiesSpawned].position, spawnPos[enemiesSpawned].rotation);
        enemiesSpawned++;

        yield return new WaitForSeconds(timer);
        isSpawning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            startSpawning = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            startSpawning = false;
    }
}
