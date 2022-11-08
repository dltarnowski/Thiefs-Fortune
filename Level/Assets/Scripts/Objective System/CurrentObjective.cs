using UnityEngine;
using TMPro;


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
