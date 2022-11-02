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
            TutorialManager.instance.objectiveName.text = "YOU WIN!!!";
            TutorialManager.instance.objectiveText.text = "You have defeated Captain Pirate and your black spot has been removed! Sail the seas and become a pirate legend!";
            TutorialManager.instance.beginButton.SetActive(false);
            TutorialManager.instance.completeButton.SetActive(false);
            TutorialManager.instance.dialogueBox.SetActive(true);
            StartCoroutine(CleanUp());
        }
    }

    public IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(5f);
        TutorialManager.instance.dialogueBox.SetActive(false);
    }
}
