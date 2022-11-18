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
    public bool menuCurrentlyOpen;

    [Header("----- Player UI -----")]
    public GameObject playerDamageFlash;
    public GameObject playerDamageIndicator;
    public GameObject playerHealthFlash;
    public GameObject playerHealthIndicator;
    public GameObject underwaterIndicator;
    public GameObject map;
    public GameObject spawnPosition;
    public Image playerHPBar;
    public Image playerHPLost;
    public Image staminaBar;
    public GameObject Crosshair;
    public TextMeshProUGUI EnemyCountText;
    public blackSpot blackspot;
    public GameObject reloadHint;
    public TextMeshProUGUI ammo;
    public GameObject ammoObject;

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
    public GameObject shopDialogue;
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
    public List<GameObject> miniMapObjectiveIcons;
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
    [SerializeField] public Animator VictoryAnim;
    public shipController shipControllerScript;

    [Header("----- Audio -----")]
    public musicSwap music;

    int towersLeft;
    /* [Header("----- Settings -----")]
     public float MSVaule;
     public float playervolumeVaule;
     public float audioVaule;
     public float gunVaule;
     public float OverallVaule;*/

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
    }
    // Update is called once per frame
    void Update()
    {
        MenuCurrentlyOpen();

        if (Input.GetButtonDown("Cancel") && !menuCurrentlyOpen)
        {
            crossHairVisible = !crossHairVisible;
            Crosshair.SetActive(crossHairVisible);

            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);

            playerScript.enabled = false;

            if (isPaused)
                cursorLockPause();
            else if (!isPaused && !inventoryPanel.activeSelf)
                cursorUnlockUnpause();
        }
    }

    public IEnumerator playerHeal()
    {
        playerHealthFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerHealthFlash.SetActive(false);
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

        if (shipControllerScript != null && !shipControllerScript.controllingShip)
        {
            playerScript.thirdPersonCam_Obj.SetActive(true);
            playerScript.thirdPersonCam_Obj.tag = "MainCamera";
            playerScript.firstPersonCam_Obj.SetActive(false);
            playerScript.firstPersonCam_Obj.tag = "SecondaryCamera";
        }
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

    public void LockPlayerPosition()
    {
        player.transform.position = shipControllerScript.playerPos.transform.position;
        player.transform.rotation = shipControllerScript.playerPos.transform.rotation;
        player.transform.parent = transform;
    }

    public void CurrentObjectiveMiniMapIcon()
    {
        for (int i = 0; i < miniMapObjectiveIcons.Count; ++i)
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

    public bool MenuCurrentlyOpen()
    {
        menuCurrentlyOpen = false;

        if (deathMenu.activeSelf)
            menuCurrentlyOpen = true;

        if (shopDialogue.activeSelf)
            menuCurrentlyOpen = true;

        if (shopInventory.activeSelf)
            menuCurrentlyOpen = true;

        if (winMenu.activeSelf)
            menuCurrentlyOpen = true;

        if (settingsMenu.activeSelf)
            menuCurrentlyOpen = true;

        if (helpMenu.activeSelf)
            menuCurrentlyOpen = true;

        if (NPCManager.instance.shopUI.activeSelf)
            menuCurrentlyOpen = true;

        if (NPCDialogueManager.instance.anim.GetBool("isOpen") == true)
            menuCurrentlyOpen = true;

        if (TutorialManager.instance.tutorialActive)
            menuCurrentlyOpen = true;


        return menuCurrentlyOpen;
    }

    /*public void DefaultSettings()
    {
        MSVaule = 350;
        playervolumeVaule = 0.5f;
        audioVaule = 0.5f;
        gunVaule = 0.5f;
        OverallVaule = 0.5f;
    }*/
}