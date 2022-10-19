using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicSwap : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip fightingMusic;
    [Range(0, 1)] [SerializeField] float musicAudVol;
    public bool inCombat;
    public bool levelMusicPlaying;
    public bool fightingMusicPlaying;
    // Update is called once per frame
    void Update()
    {
        if (!inCombat && !levelMusicPlaying)
        {
            aud.Stop();
            aud.PlayOneShot(levelMusic, musicAudVol);
            Debug.Log("Level Music");

            levelMusicPlaying = true;
            fightingMusicPlaying = false;
        }
        else if (inCombat && !fightingMusicPlaying)
        {
            aud.Stop();
            aud.PlayOneShot(fightingMusic, musicAudVol);
            Debug.Log("Fighting Music");
            levelMusicPlaying = false;
            fightingMusicPlaying = true;
        }
    }
}
