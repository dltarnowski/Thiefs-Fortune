using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    public TextMeshProUGUI name;
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI followUpDialogue;
    public TextMeshProUGUI talkButtonText;
    public TextMeshProUGUI shopButtonText;
    public GameObject NPCCamera;

    public bool talking;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        /*if(talking)
        {
            outputDialogue.text = followUpDialogue.text;
        }
        else if(!talking)
        {
            outputDialogue.text = dialogue.text;
        }*/
    }
}
