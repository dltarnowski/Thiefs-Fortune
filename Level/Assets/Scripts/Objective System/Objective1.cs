using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Objective1 : MonoBehaviour
{
    [SerializeField] public string Description;
    public GameObject ObjectiveTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.objText.text = Description.ToString();
            gameManager.instance.ObjectiveBox.SetActive(true);
            StartCoroutine(Timer());
        }
    }
    private IEnumerator Timer()
    {
        gameManager.instance.anim.SetBool("isActive", true);
        yield return new WaitForSeconds(4f);
        gameManager.instance.anim.SetBool("isActive", false);
        ObjectiveTrigger.SetActive(false);
    }
}
