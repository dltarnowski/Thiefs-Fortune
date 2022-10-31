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
            anim.SetTrigger("open");
            blackSpotDropChance = Random.Range(0f, 1f);
            if (blackSpotDropChance <= 0.1)
                gameManager.instance.blackspot.blackSpotMultiplier *= 1.2f;
            else
                Instantiate(drops[Random.Range(0, drops.Length - 1)], itemSpawnPos.position, itemSpawnPos.rotation);
            opened = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }        
    }
}
