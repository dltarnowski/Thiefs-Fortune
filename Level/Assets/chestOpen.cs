using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestOpen : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject[] drops;
    [SerializeField] Transform itemSpawnPos;
    bool canOpen;
    bool opened;
    float blackSpotDropChance;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canOpen && Input.GetKeyDown(KeyCode.E) && !opened)
        {
            winManager.instance.clueCount++;
            gameManager.instance.hint.SetActive(false);
            anim.SetTrigger("open");
            blackSpotDropChance = Random.Range(0f, 1f);
            if (blackSpotDropChance <= 0.1)
                gameManager.instance.blackspot.blackSpotMultiplier *= 1.2f;
            else
                Instantiate(drops[Random.Range(0, drops.Length - 1)], itemSpawnPos.position, itemSpawnPos.rotation);
            opened = true;

            TutorialManager.instance.objectiveText.text = "My love and I are in exile and even here, we aren't safe. We must head to Port Placeholder for sanctuary! There, I have some friends who I can let know my whereabouts.";
            TutorialManager.instance.beginButton.SetActive(false);
            TutorialManager.instance.completeButton.SetActive(false);
            TutorialManager.instance.dialogueBox.SetActive(true);
            StartCoroutine(CleanUp());
        }
    }
    public IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(7f);
        TutorialManager.instance.dialogueBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.hint.SetActive(true);
            canOpen = true;
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.hint.SetActive(false);
            canOpen = false;
        }        
    }
}
