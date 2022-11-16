using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public bool MMMcamera;
    [Header("----- Cameras -----")]
    public GameObject CameraPosition;
    public GameObject CameraPosition2;
    [Header("----- Screens -----")]
    public GameObject MainButtons;
    public GameObject SettingScreen;
    [Header("----- UI -----")]
    public GameObject settingsMenu;
    public GameObject helpMenu;
    [Header("----- Settings -----")]
    public float MSVaule;
    public float playervolumeVaule;
    public float audioVaule;
    public float gunVaule;
    public float OverallVaule;

    public static MainMenuManager instance;

    void Awake()
    {
        DefaultSettings();
    }
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(MMMcamera == true)
        {
            CameraPosition2.SetActive(true);
        }
        else
        {
            CameraPosition2.SetActive(false);
            CameraPosition.SetActive(true);
        }
    }

    public void DefaultSettings()
    {
        MSVaule = 350;
        playervolumeVaule = 0.5f;
        audioVaule = 0.5f;
        gunVaule = 0.5f;
        OverallVaule = 0.5f;
    }
}
