using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winManager : MonoBehaviour
{
    winManager instance;
    [SerializeField] GameObject pirateLegend;
    [SerializeField] Transform pirateLegendSpawnPos;
    [SerializeField] GameObject winUI;
    [SerializeField] public int clueCount;
    [SerializeField] GameObject currLegend;
    bool legendSpawned;
    bool win;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(clueCount == 4 && !legendSpawned)
        {
            legendSpawned = true;
            Instantiate(pirateLegend, pirateLegendSpawnPos.position, pirateLegendSpawnPos.rotation);
            currLegend = GameObject.FindGameObjectWithTag("Legend");
        }
        if(legendSpawned && currLegend == null && !win)
        {
            win = true;
            winUI.SetActive(true);
        }
    }
}
