using UnityEngine;

public class musicSwap : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioClip fightingMusic;
    public bool inCombat;
    public bool levelMusicPlaying;
    public bool fightingMusicPlaying;
    float currVolume;

    void Start()
    {
        ChangeMusicVolume();
    }
    // Update is called once per frame
    void Update()
    {
        if(currVolume != gameManager.instance.MusicSlider.value)
        {
            ChangeMusicVolume();
        }
        if (!inCombat && !levelMusicPlaying)
        {
            aud.Stop();
            aud.PlayOneShot(levelMusic, gameManager.instance.MusicSlider.value);
            levelMusicPlaying = true;
            fightingMusicPlaying = false;
        }
        else if (inCombat && !fightingMusicPlaying)
        {
            aud.Stop();
            aud.PlayOneShot(fightingMusic, gameManager.instance.MusicSlider.value);
            levelMusicPlaying = false;
            fightingMusicPlaying = true;
        }
    }

    public void ChangeMusicVolume()
    {
        aud.volume = gameManager.instance.MusicSlider.value;
        currVolume = aud.volume;
    }
}
