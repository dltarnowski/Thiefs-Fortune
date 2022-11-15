using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGrass : MonoBehaviour
{
    [SerializeField] AudioClip aud;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.instance.playerScript.aud.PlayOneShot(aud);
        }
    }
}
