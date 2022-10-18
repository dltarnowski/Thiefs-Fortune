using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Objective1 : MonoBehaviour
{
    public GameObject theObjective;
    public GameObject Trigger;
    public GameObject Text;
    [SerializeField] public string Description;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(ObjectiveList());
    }
    private IEnumerator ObjectiveList()
    {
        theObjective.SetActive(true);
        Text.GetComponent<Text>().text = Description.ToString();
        theObjective.GetComponent<Animation>().Play("ObjectivesSlidein");
        yield return new WaitForSeconds(7.0f);
        Text.GetComponent<Text>().text = "";
        Trigger.SetActive(false);
        theObjective.SetActive(false);
    }
}
