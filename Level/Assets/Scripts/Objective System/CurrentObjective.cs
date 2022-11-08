using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CurrentObjective : MonoBehaviour
{
    [SerializeField] public string Description;
    public GameObject ObjectiveBox;
    public TextMeshProUGUI objText;

    void Update()
    {
        objText.text = Description.ToString();
        objText.text = gameManager.instance.objText.text;
    }
}
