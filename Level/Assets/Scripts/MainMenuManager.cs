using UnityEngine;

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
    public static MainMenuManager instance;

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
}
