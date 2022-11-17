using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameManager.instance.ammo.text = gameManager.instance.playerScript.gunStats.ammoCount.ToString();

        if (gameManager.instance.playerScript.gunStats.ammoCount <= 0)
        {
            gameManager.instance.reloadHint.SetActive(true);
            gameManager.instance.ammoObject.SetActive(false);
        }
        else
        {
            gameManager.instance.reloadHint.SetActive(false);
            gameManager.instance.ammoObject.SetActive(true);
        }
    }
}
