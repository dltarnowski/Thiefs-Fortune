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

    [Header("----- Player Stuff -----")]
    public GameObject player;
    public playerController playerScript;
    public GameObject Ammo;
    public int ammoCount;
    public cameraControls cameraScript;

    [Header("----- Menu UI -----")]
    public GameObject winMenu;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject menuCurrentlyOpen;
    [Header("----- Player UI -----")]
    public GameObject acObject;
    public GameObject playerDamageFlash;
    public GameObject spawnPosition;
    public Image playerHPBar;
    public GameObject Crosshair;
    public TextMeshProUGUI EnemyCountText;
    [Header("----- Objective UI -----")]
    public TextMeshProUGUI objText;
    public GameObject ObjectiveBox;
    [SerializeField] public Animator anim;
    [Header("----- UI -----")]
    public GameObject hint;
    [Header("----- NPC UI -----")]
    public GameObject healthBar;
    public GameObject npcDialogue;
    public GameObject shopInventory;
    public TextMeshProUGUI coinCountText;
    public GameObject shopPanels;
    public Collider collide;
    [Header("----- Gun -----")]
    public GameObject mainCamera;
    public Recoil recoilScript;
    [Header("----- Other -----")]
    public bool isPaused;
    public bool crossHairVisible = true;

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
        ammoCount = 5;
        towersLeft = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !deathMenu.activeSelf && !winMenu.activeSelf && !npcDialogue.activeSelf && !shopInventory.activeSelf)
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
        //CheckAmmoAmount();
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
        healthBar.SetActive(false);
        Crosshair.SetActive(false);
        collide.isTrigger = false;
    }

    public void NpcUnpause()
    {
        playerScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraScript.enabled = true;
        healthBar.SetActive(true);
        Crosshair.SetActive(true);
        collide.isTrigger = true;
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
            GameObject.Find("Crosshair").SetActive(false);
            winMenu.SetActive(true);
            cursorUnlockUnpause();
        }
    }
}