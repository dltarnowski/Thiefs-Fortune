using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoController : MonoBehaviour
{
    public string url;
    public VideoPlayer vidplayer;
    [SerializeField] GameObject Barrel;
    [SerializeField] GameObject Background;
    private bool playing;

    // Start is called before the first frame update
    void Awake()
    {
        vidplayer = GetComponent<VideoPlayer>();
        vidplayer.url = url;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playing && vidplayer.isPlaying)
        {
            playing = true;
            Play();
            Barrel.SetActive(false);
            Background.SetActive(false);
        }

    }

    void Play()
    {
        vidplayer.Play();
        vidplayer.isLooping = true;
    }
}