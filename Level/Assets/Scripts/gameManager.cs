using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public int EnemyNumber;
    public int currencyNumber;
    public Inventory inventory;

    [Header("----- Player Stuff -----")]
    public GameObject player;
    public playerController playerScript;
    public GameObject Ammo;
    public int ammoCount;
    public cameraControls cameraScript;
    public GameObject origSpawn;

    [Header("----- Menu UI -----")]
    public GameObject winMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject helpMenu;
    public GameObject deathMenu;
    public GameObject menuCurrentlyOpen;

    [Header("----- Player UI -----")]
    public GameObject acObject;
    public GameObject playerDamageFlash;
    public GameObject playerDamageIndicator;
    public GameObject underwaterIndicator;
    public GameObject map;
    public GameObject spawnPosition;
    public Image playerHPBar;
    public Image playerHPLost;
    public Image staminaBar;
    public GameObject Crosshair;
    public TextMeshProUGUI EnemyCountText;
    public blackSpot blackspot;

    [Header("----- Objective UI -----")]
    public TextMeshProUGUI objText;
    public GameObject ObjectiveBox;
    [SerializeField] public Animator anim;

    [Header("----- UI -----")]
    public GameObject hint;
    public Image[] ammoArray;
    public List<Image> objectives;
    public GameObject basicMoveUI;
    public GameObject objectiveComplete;

    public GameObject inventoryTab;
    public GameObject activeTab;
    public GameObject activePanel;
    public GameObject inventoryPanel;

    [Header("----- NPC UI -----")]
    public GameObject healthBar;
    public GameObject npcDialogue;
    public GameObject shopInventory;
    public TextMeshProUGUI coinCountText;
    public bool weaponCollide;
    public bool consumeCollide;
    public bool TutorialCollide;
    public bool npcCollide;
    public GameObject npcCam;

    [Header("----- Gun -----")]
    public GameObject mainCamera;
    public Recoil recoilScript;

    [Header("----- Mini Map -----")]
    public GameObject[] miniMapObjectiveIcons;
    public Pointer miniMapPointer;
    public Camera miniMapCamera;
    public GameObject miniMapWindow;

    [Header("----- Other -----")]
    public bool isPaused;
    public GameObject importantNote;
    public GameObject NotePickup;
    public bool crossHairVisible = true;
    public Slider MSSlider;
    public Slider MusicSlider;
    public Slider PlayerAudioSlider;
    public Slider GunVolumeSlider;
    public bool handmaiden;
    public GameObject[] islandObjects;

    [Header("----- Audio -----")]
    public musicSwap music;

    int towersLeft;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        Ammo = GameObject.Find("Ammo");
        playerScript = player.GetComponent<playerController>();
        mainCamera = GameObject.Find("Main Camera");
        cameraScript = mainCamera.GetComponent<cameraControls>();
        recoilScript = GameObject.Find("Camera Recoil").GetComponent<Recoil>();
        spawnPosition = GameObject.FindGameObjectWithTag("Spawn Position");
        music = GameObject.FindGameObjectWithTag("LevelMusic").GetComponent<musicSwap>();
        towersLeft = 2;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !deathMenu.activeSelf && !npcDialogue.activeSelf && !shopInventory.activeSelf && !settingsMenu.activeSelf && !helpMenu.activeSelf)
        {
            crossHairVisible = !crossHairVisible;
            Crosshair.SetActive(crossHairVisible);

            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);


            if (isPaused)
                cursorLockPause();
            else
                cursorUnlockUnpause();
        }
    }


    public IEnumerator playerDamage()
    {
        playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerDamageFlash.SetActive(false);
    }

    public void cursorLockPause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void cursorUnlockUnpause()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void NpcPause()
    {
        playerScript.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        cameraScript.enabled = false;
        Crosshair.SetActive(false);
    }

    public void NpcUnpause()
    {
        playerScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraScript.enabled = true;
        Crosshair.SetActive(true);
    }

    public void checkEnemyTotal()
    {
        EnemyNumber--;
        EnemyCountText.text = EnemyNumber.ToString("F0");
    }

    public void CheckTowerTotal()
    {
        towersLeft--;
        if (towersLeft <= 0)
        {
            winMenu.SetActive(true);
            cursorUnlockUnpause();
        }
    }

    public void CurrentObjectiveMiniMapIcon()
    {
        if (winManager.instance.plMiniMapObjective != null)
            gameManager.instance.miniMapObjectiveIcons[winManager.instance.clueCount] = winManager.instance.plMiniMapObjective;

        for (int i = 0; i < miniMapObjectiveIcons.Length; ++i)
        {
            if (i == winManager.instance.clueCount)
                miniMapObjectiveIcons[i].SetActive(true);
            else
                miniMapObjectiveIcons[i].SetActive(false);

            //if (i == 1)
            //{
            //    miniMapObjectiveIcons[1].SetActive(true);
            //    miniMapObjectiveIcons[5].SetActive(true);
            //    miniMapObjectiveIcons[6].SetActive(true);
            //    miniMapObjectiveIcons[7].SetActive(true);
            //    miniMapObjectiveIcons[8].SetActive(true);
            //}
            //else
            //{
            //    miniMapObjectiveIcons[1].SetActive(false);
            //    miniMapObjectiveIcons[5].SetActive(false);
            //    miniMapObjectiveIcons[6].SetActive(false);
            //    miniMapObjectiveIcons[7].SetActive(false);
            //    miniMapObjectiveIcons[8].SetActive(false);
            //}
        }

        miniMapPointer.SetTarget();
    }

    public void ReduceAmmo()
    {
        if (ammoArray.Length > 0)
            ammoArray[ammoCount - 1].enabled = false;
    }

    public void IncreaseAmmo()
    {
        for (int i = 0; i < ammoArray.Length; i++)
        {
            ammoArray[i].enabled = true;
        }
    }
}