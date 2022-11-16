using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSettings : MonoBehaviour
{
    [SerializeField] private Slider MSSlider;
    public float MSVaule;
    
    [SerializeField] private Slider volumeSlider;
    public float volumeVaule;

    [SerializeField] private Slider AudioSlider;
    public float audioVaule;
    
    [SerializeField] private Slider gunSlider;
    public float gunVaule;
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
        volumeVaule = 0.5f;
        audioVaule = 0.5f;
        gunVaule = 0.5f;
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
        volumeVaule = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeVaule);
        LoadPVSettings();
    }
    void LoadPVSettings()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
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
}
