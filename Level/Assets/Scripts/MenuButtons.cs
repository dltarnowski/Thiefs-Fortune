using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Globalization;

public class MenuButtons : MonoBehaviour
{

    public TextMeshProUGUI dialogue;
    Vector3 position;
    int start;
    int end;

    private void Start()
    {
        start = (int)gameManager.instance.shopscreen.transform.position.x;
        end = -30;
    }

    public void Resume()
    {
        gameManager.instance.cursorUnlockUnpause();
        gameManager.instance.pauseMenu.SetActive(false);
        gameManager.instance.isPaused = false;
        if (!gameManager.instance.crossHairVisible)
        {
            gameManager.instance.crossHairVisible = !gameManager.instance.crossHairVisible;
            gameManager.instance.Crosshair.SetActive(gameManager.instance.crossHairVisible);
        }
    }
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Restart()
    {
        gameManager.instance.cursorUnlockUnpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Return()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void respawn()
    {
        gameManager.instance.playerScript.respawn();
        gameManager.instance.cursorUnlockUnpause();
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Game has ended");
    }

    public void WhereAmI()
    {
        dialogue.text = "Look around you! Paradise!";
    }

    public void Shop()
    {
        gameManager.instance.npcDialogue.SetActive(false);
        gameManager.instance.shopscreen.SetActive(true);
    }

    public void Bye()
    {
        gameManager.instance.npcDialogue.SetActive(false);
    }

    public void Left()
    {
        if (gameManager.instance.shopscreen.transform.position.x < start)
        {
            gameManager.instance.shopscreen.transform.Translate(15, 0, 0);
        }
    }

    public void Right()
    {
        if (gameManager.instance.shopscreen.transform.position.x > end)
        {
            gameManager.instance.shopscreen.transform.Translate(-15, 0, 0);
        }
    }
   
}
