using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    [SerializeField] private Slider MSSlider;
    public float MSVaule;
    
    [SerializeField] private Slider PlayerVolumeSlider;
    public float playervolumeVaule;

    [SerializeField] private Slider AudioSlider;
    public float audioVaule;
    
    [SerializeField] private Slider gunSlider;
    public float gunVaule;

    [SerializeField] private Slider OverallSlider;
    public float overallVaule;

    private void Awake()
    {
        DefaultSettings();
    }
    void Start()
    {
        LoadMSSettings();
        LoadPVSettings();
        LoadAudioSettings();
        LoadGunSettings();
    }
    private void Update()
    {
        SaveMSSettings();
        SavePlayerVolumeSettings();
        SaveAudioSettings();
        SaveGunSettings();
    }

    public void DefaultSettings()
    {
        MSVaule = 350;
        playervolumeVaule = 0.5f;
        audioVaule = 0.5f;
        gunVaule = 0.5f;
        overallVaule = 0.5f;
    }


    //Manages Mouse senseitivity
    public void SaveMSSettings()
    {
        MSVaule = MSSlider.value;
        PlayerPrefs.SetFloat("msValue", MSVaule);
        LoadMSSettings();
    }
    void LoadMSSettings()
    {
        float MSVaule = PlayerPrefs.GetFloat("msValue");
        MSSlider.value = MSVaule;
    }
    //Manages Player volume
    public void SavePlayerVolumeSettings()
    {
        playervolumeVaule = PlayerVolumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", playervolumeVaule);
        LoadPVSettings();
    }
    void LoadPVSettings()
    {
        float playervolumeValue = PlayerPrefs.GetFloat("VolumeValue");
        PlayerVolumeSlider.value = playervolumeValue;
    }
    //Manages Music volume
    public void SaveAudioSettings()
    {
        audioVaule = AudioSlider.value;
        PlayerPrefs.SetFloat("AudioValue", audioVaule);
        LoadAudioSettings();
    }
    void LoadAudioSettings()
    {
        float audioVaule = PlayerPrefs.GetFloat("AudioValue");
        AudioSlider.value = audioVaule;
    }
    //Manages Gun volume
    public void SaveGunSettings()
    {
        gunVaule = gunSlider.value;
        PlayerPrefs.SetFloat("GunSlider", gunVaule);
        LoadGunSettings();
    }
    void LoadGunSettings()
    {
        float gunVaule = PlayerPrefs.GetFloat("GunSlider");
        gunSlider.value = gunVaule;
    }
    //Manages Overall volume
    public void SaveOverallSettings()
    {
        overallVaule = OverallSlider.value;
        gameManager.instance.gunVaule = overallVaule;
        PlayerPrefs.SetFloat("OverallSlider", overallVaule);
        LoadOverallSettings();
    }
    void LoadOverallSettings()
    {
        float overallVaule = PlayerPrefs.GetFloat("OverallSlider");
        OverallSlider.value = overallVaule;
        AudioListener.volume = overallVaule;
    }
}
