using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DefaultSettings : MonoBehaviour
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

    [SerializeField] AudioMixer MasterMixer;
    public float volume;

    void OnEnable()
    {
        LoadMSSettings();
        LoadPVSettings();
        LoadAudioSettings();
        LoadGunSettings();
        LoadOverallSettings();
    }
    public void SaveAllSettings()
    {
        SaveMSSettings();
        SavePlayerVolumeSettings();
        SaveAudioSettings();
        SaveGunSettings();
        SaveOverallSettings();
    }
    //Manages Mouse senseitivity
    public void SaveMSSettings()
    {
        MSVaule = MSSlider.value;
        MainMenuManager.instance.MSVaule = MSVaule;
        PlayerPrefs.SetFloat("msValue", MSVaule);
        LoadMSSettings();
    }
    void LoadMSSettings()
    {
        float MSVaule = PlayerPrefs.GetFloat("msValue");
        MSSlider.value = MSVaule;
    }
    //Manages Player Volume
    public void SavePlayerVolumeSettings()
    {
        playervolumeVaule = PlayerVolumeSlider.value;
        MainMenuManager.instance.playervolumeVaule = playervolumeVaule;
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
        MainMenuManager.instance.audioVaule = audioVaule;
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
        MainMenuManager.instance.gunVaule = gunVaule;
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
        MainMenuManager.instance.OverallVaule = overallVaule;
        PlayerPrefs.SetFloat("OverallSlider", overallVaule);
        LoadOverallSettings();
    }
    void LoadOverallSettings()
    {
        float overallVaule = PlayerPrefs.GetFloat("OverallSlider");
        OverallSlider.value = overallVaule;
    }
}
