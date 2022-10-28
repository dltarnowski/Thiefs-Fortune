using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoCount : MonoBehaviour
{
    [SerializeField] public Image[] Bullets;
    void Start()
    {
        foreach (var image in Bullets)
        {
            image.enabled = false;
        }
    }
    private void Update()
    {
        if(gameManager.instance.playerScript.gunGrabbed == true)
        {
            for (int i = 0; i <= gameManager.instance.playerScript.ammoCount - 1; i++)
            {
                Bullets[i].enabled = true;
            }
            if (Input.GetButton("Fire1"))
            {
                UpdateAmmo();
            }
        }
    }
    public void UpdateAmmo()
    {
        if(gameManager.instance.playerScript.ammoCount != 0)
            Bullets[gameManager.instance.playerScript.ammoCount - 1].enabled = false;
    }
}
