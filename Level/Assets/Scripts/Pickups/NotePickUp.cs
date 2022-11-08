using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickUp : MonoBehaviour
{
    public bool NoteGrabbed;
    void Update()
    {
        if (NoteGrabbed == true)
        {
            StartCoroutine(CleanUp());
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.importantNote.SetActive(true);
            NoteGrabbed = true;
        }
    }

    public IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(5.0f);
        gameManager.instance.importantNote.SetActive(false);
        gameManager.instance.NotePickup.SetActive(false);
    }
}
