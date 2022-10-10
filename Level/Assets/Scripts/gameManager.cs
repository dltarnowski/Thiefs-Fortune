using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public int ammoCountNum;
    public static gameManager instance;

    [Header("----- Player Stuff -----")]
    public GameObject player;
    public playerController playerScript;

    [Header("----- UI -----")]
    public GameObject winMenu;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject menuCurrentlyOpen;
    public GameObject acObject;
    public TextMeshProUGUI ammoCountText;

    public bool isPaused;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAmmoAmount();
        if (Input.GetButtonDown("Cancel") && !deathMenu.activeSelf && !winMenu.activeSelf)
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);

            if(isPaused)
                cursorLockPause();
            else
                cursorUnlockUnpause();
        }
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

    public void CheckAmmoAmount()
    {
        if (playerScript.isShooting == true)
        {
            playerScript.ammoCount--;
        }
        if (playerScript.gunGrabbed == true)
        {
            ammoCountText.text = playerScript.ammoCount.ToString("F0");
            acObject.SetActive(true);
            if(playerScript.ammoCount <= 1)
            {
                ammoCountText.color = new Color(255, 0, 0, 100);
            }
            else
            {
                ammoCountText.color = new Color(0, 0, 0, 100);
            }
        }
    }
}
