using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    public Camera cam;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.instance.hint.SetActive(true);

        if (other.CompareTag("Player"))
            gameManager.instance.npcCollide = true;
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hint.SetActive(false);

        if (other.CompareTag("Player"))
            gameManager.instance.npcCollide = false;
    }
}
