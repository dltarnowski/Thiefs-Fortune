using UnityEngine;
using TMPro;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    public TextMeshProUGUI npcName;
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI followUpDialogue;
    public TextMeshProUGUI talkButtonText;
    public TextMeshProUGUI shopButtonText;
    public GameObject NPCCamera;
    public GameObject shopUI;
    public GameObject weaponUI;
    public GameObject consumeUI;

    public bool talking;

    private void Awake()
    {
        instance = this;
    }
}
