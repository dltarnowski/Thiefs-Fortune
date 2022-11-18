using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class winManager : MonoBehaviour
{
    public static winManager instance;
    [SerializeField] GameObject pirateLegend;
    [SerializeField] Transform pirateLegendSpawnPos;
    [SerializeField] GameObject winUI;
    [SerializeField] public int clueCount;
    [SerializeField] public GameObject currLegend;
    bool legendSpawned;
    bool win;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (clueCount == 4 && !legendSpawned)
        {
            legendSpawned = true;
            Instantiate(pirateLegend, pirateLegendSpawnPos.position, pirateLegendSpawnPos.rotation);
            currLegend = GameObject.FindGameObjectWithTag("Legend");
            //gameManager.instance.miniMapObjectiveIcons.Add(currLegend.GetComponent<MiniMapIcons>().gameObject);
            gameManager.instance.CurrentObjectiveMiniMapIcon();
        }
        if (legendSpawned && currLegend == null && !win)
        {
            win = true;
            gameManager.instance.miniMapObjectiveIcons[4].SetActive(false);
            gameManager.instance.miniMapPointer.gameObject.SetActive(false);
            gameManager.instance.winMenu.SetActive(true);
            StartCoroutine(CleanUp());
        }
    }

    public IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(1f);
        gameManager.instance.cursorLockPause();
    }
}
