using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static Water instance;
    //[SerializeField] float waterHeight = -3.2f;
    private void Awake()
    {
        instance = this; 
    }
    public void WaterReset()
    {
        gameManager.instance.underwaterIndicator.SetActive(false);
        gameManager.instance.playerScript.jumpHeight = gameManager.instance.playerScript.jumpHeightOrig;
        gameManager.instance.playerScript.gravityValue = gameManager.instance.playerScript.gravityValueOrig;
        gameManager.instance.playerScript.isUnderwater = false;
        gameManager.instance.playerScript.anim.SetBool("IsInWater", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") /*&& gameManager.instance.playerScript.waterDetectionPoint.transform.position.y < waterHeight*/)
        {
            gameManager.instance.underwaterIndicator.SetActive(true);
            gameManager.instance.playerScript.jumpHeight = 3;
            gameManager.instance.playerScript.gravityValue /= 10;
            gameManager.instance.playerScript.isUnderwater = true;
            gameManager.instance.playerScript.anim.SetBool("IsInWater", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") /*&& gameManager.instance.playerScript.waterDetectionPoint.transform.position.y > waterHeight*/)
        {
            gameManager.instance.underwaterIndicator.SetActive(false);
            gameManager.instance.playerScript.jumpHeight = gameManager.instance.playerScript.jumpHeightOrig;
            gameManager.instance.playerScript.gravityValue = gameManager.instance.playerScript.gravityValueOrig;
            gameManager.instance.playerScript.isUnderwater = false;
            gameManager.instance.playerScript.anim.SetBool("IsInWater", false);
        }
    }
}
