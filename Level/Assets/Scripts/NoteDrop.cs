using System.Collections;
using UnityEngine;

public class NoteDrop : MonoBehaviour
{
    [SerializeField] Animator anim;
    bool canOpen;
    bool opened;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E) && !opened)
        {
            winManager.instance.clueCount++;
            gameManager.instance.CurrentObjectiveMiniMapIcon();
            gameManager.instance.hint.SetActive(false);
            anim.SetTrigger("open");
            gameManager.instance.NotePickup.SetActive(true);
            opened = true;

            StartCoroutine(CleanUp());
        }

    }


    public IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(7f);
        TutorialManager.instance.dialogueBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && opened == false)
        {
            gameManager.instance.hint.SetActive(true);
            canOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.hint.SetActive(false);
            canOpen = false;
        }
    }
}
