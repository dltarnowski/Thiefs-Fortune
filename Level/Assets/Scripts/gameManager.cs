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
    public int ammoCount;

    [Header("----- UI -----")]
    public GameObject winMenu;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject menuCurrentlyOpen;
    public GameObject acObject;
    public GameObject playerDamageFlash;
    public GameObject spawnPosition;
    public Image playerHPBar;
    public GameObject Crosshair;
    public TextMeshProUGUI EnemyCountText;
    public TextMeshProUGUI hint;

    public GameObject npcDialogue;
    public GameObject shopInventory;
    public TextMeshProUGUI coinCountText;
    public GameObject shopPanels;

    public bool isPaused;
    public bool crossHairVisible = true;

    public GameObject mainCamera;
    public Recoil recoilScript;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
        mainCamera = GameObject.Find("Main Camera");
        recoilScript = GameObject.Find("Camera Recoil").GetComponent<Recoil>();
        spawnPosition = GameObject.FindGameObjectWithTag("Spawn Position");
        ammoCount = 5;
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

    public void checkEnemyTotal()
    {
        EnemyNumber--;
        EnemyCountText.text = EnemyNumber.ToString("F0");

        if (EnemyNumber <= 0)
        {
            GameObject.Find("Crosshair").SetActive(false);
            winMenu.SetActive(true);
            cursorLockPause();
        }
    } 
}
