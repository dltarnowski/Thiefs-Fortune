using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcAudio : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            source.PlayOneShot(clip);
        }
    }
}
