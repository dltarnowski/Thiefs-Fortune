using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] GameObject ammo;
    [SerializeField] Transform spawnPos;

    private void Start()
    {
        if(TutorialManager.instance.combatTrigger)
            Instantiate(ammo,spawnPos);
    }
    // Update is called once per frame
    void Update()
    {
        if (TutorialManager.instance.pickedUp)
        {
            StartCoroutine(Delay());
            TutorialManager.instance.pickedUp = false;
        }
    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(10f);
        Instantiate(ammo, spawnPos);
    }
}
