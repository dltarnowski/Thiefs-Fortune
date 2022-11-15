using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class blackSpot : MonoBehaviour
{
    [Range(0f , 1f)] public float blackSpotMultiplier;
    [SerializeField] float raidTimer;
    [SerializeField] GameObject spawner;
    [SerializeField] Image blackspotUI;
    float spawnChance;
    float currblackspot;
    bool isSpawning;
    bool firstRaid;
    void Start()
    {
        FillBlackSpot();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawning && blackSpotMultiplier > 0 && !TutorialManager.instance.tutorialActive)
        {
            if (!firstRaid)
                Invoke("firstTimeSpawn", 15f);
            else
                StartCoroutine(raid());
        }

        if(currblackspot != blackSpotMultiplier)
        {
            FillBlackSpot();
        }
    }

    public void firstTimeSpawn()
    {
        firstRaid = true;
        StartCoroutine(raid());
    }

    public void FillBlackSpot()
    {
        currblackspot = blackSpotMultiplier;
        blackspotUI.fillAmount = blackSpotMultiplier;
    }

    IEnumerator raid()
    {
        isSpawning = true;
        Instantiate(spawner, gameManager.instance.player.transform.position, gameManager.instance.player.transform.rotation);
        yield return new WaitForSeconds(raidTimer / blackSpotMultiplier);
        isSpawning = false;
    }
}
