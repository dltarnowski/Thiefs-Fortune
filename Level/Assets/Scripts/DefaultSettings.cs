using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //Manages Mouse senseitivity
    public void SaveMSSettings()
    {
        MSVaule = MSSlider.value;
        gameManager.instance.MSVaule = MSVaule;
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
        gameManager.instance.playervolumeVaule = playervolumeVaule;
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
        gameManager.instance.audioVaule = audioVaule;
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
        gameManager.instance.gunVaule = gunVaule;
        PlayerPrefs.SetFloat("GunSlider", gunVaule);
        LoadGunSettings();
    }
    void LoadGunSettings()
    {
        float gunVaule = PlayerPrefs.GetFloat("GunSlider");
        gunSlider.value = gunVaule;
    }
}
