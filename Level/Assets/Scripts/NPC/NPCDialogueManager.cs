using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogue;
    public Animator anim;

    int random;
    int otherRandom;

    List<string> dialogueList = new List<string>();

    private void Start()
    {
        dialogueList.Add("Heard you're looking for Captain Noble. Have you talked to Willy the Weaponsmith?");
        dialogueList.Add("Rumor is you're looking for Captain Noble. Willy might be able to help. He's the town Weaponsmith.");
        dialogueList.Add("If you're looking for someone, Willy the Weaponsmith is your guy.");
        dialogueList.Add("Captain Noble is ruthless. What do you want with him? Nevermind... Not my business. Go find Willy the Weaponsmith.");
        dialogueList.Add("What're you looking at?");
        dialogueList.Add("Take a picture, it'll last longer...");
        dialogueList.Add("If an apple a day keeps the doctor away, what about a bushel?");
        dialogueList.Add("Do you think sand is called sand because it's between the sea and land?");
        dialogueList.Add("If you were born deaf, what language would you think in?");
        dialogueList.Add("If I hit myself and it hurts, am I weak or am I strong?");
        dialogueList.Add("If you're waiting for the waiter, aren't YOU the waiter?");
    }
    private void Update()
    {
        random = Random.Range(0, 3);
        otherRandom = Random.Range(4, dialogueList.Count);

        if (!gameManager.instance.handmaiden)
        {
            if (gameManager.instance.npcCollide && Input.GetKeyDown(KeyCode.E) && anim.GetBool("isOpen") == false ||
                gameManager.instance.handmaiden && Input.GetKeyDown(KeyCode.E) && anim.GetBool("isOpen") == false)
            {
                gameManager.instance.NpcPause();
                gameManager.instance.hint.SetActive(false);
                gameManager.instance.mainCamera.SetActive(false);
                gameManager.instance.npcCam.SetActive(true);
                anim.SetBool("isOpen", true);

                dialogue.text = dialogueList[otherRandom];
            }
        }
        else
        {
            gameManager.instance.NpcPause();
            gameManager.instance.hint.SetActive(false);
            gameManager.instance.mainCamera.SetActive(false);
            gameManager.instance.npcCam.SetActive(true);
            anim.SetBool("isOpen", true);

            dialogue.text = dialogueList[random];
            winManager.instance.clueCount++;
            gameManager.instance.CurrentObjectiveMiniMapIcon();
        }

    }

    public void Continue()
    {
        gameManager.instance.NpcUnpause();
        gameManager.instance.hint.SetActive(true);
        anim.SetBool("isOpen", false);
        gameManager.instance.mainCamera.SetActive(true);
        gameManager.instance.npcCam.SetActive(false);
    }
}
