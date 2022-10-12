using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Restart()
    {
        gameManager.instance.cursorUnlockUnpause();
    }
    public void Return()
    {
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
   
}
