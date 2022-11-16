using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    //int currentPosition;
    //int start = 0;

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
    public void ContinueExploring()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(HideMenu());
    }
    public IEnumerator HideMenu()
    {
        gameManager.instance.VictoryAnim.SetBool("Up", true);
        yield return new WaitForSeconds(1.2f);
        gameManager.instance.VictoryAnim.SetBool("Up", false);
        gameManager.instance.winMenu.SetActive(false);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
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

    public void Settings()
    {
        gameManager.instance.cursorLockPause();
        gameManager.instance.pauseMenu.SetActive(false);
        gameManager.instance.isPaused = false;
        if (gameManager.instance.crossHairVisible)
        {
            gameManager.instance.crossHairVisible = !gameManager.instance.crossHairVisible;
            gameManager.instance.Crosshair.SetActive(gameManager.instance.crossHairVisible);
        }
        gameManager.instance.settingsMenu.SetActive(true);
    }
    
    public void Back()
    {
        if (gameManager.instance.crossHairVisible)
        {
            gameManager.instance.crossHairVisible = !gameManager.instance.crossHairVisible;
            gameManager.instance.Crosshair.SetActive(gameManager.instance.crossHairVisible);
        }
        gameManager.instance.settingsMenu.SetActive(false);
        gameManager.instance.pauseMenu.SetActive(true);
    }
    public void Help()
    {
        gameManager.instance.settingsMenu.SetActive(false);
        gameManager.instance.helpMenu.SetActive(true);
    }
    public void Back2()
    {
        gameManager.instance.helpMenu.SetActive(false);
        gameManager.instance.settingsMenu.SetActive(true);
    }
    //used in Main Menu Scene
    public void cameraChange()
    {
        MainMenuManager.instance.MainButtons.SetActive(false);
        MainMenuManager.instance.MMMcamera = true;
        MainMenuManager.instance.SettingScreen.SetActive(true);
    }
    public void cameraChange2()
    {
        MainMenuManager.instance.MMMcamera = false;
        MainMenuManager.instance.SettingScreen.SetActive(false);
        MainMenuManager.instance.MainButtons.SetActive(true);
    }
    public void Help2()
    {
        MainMenuManager.instance.settingsMenu.SetActive(false);
        MainMenuManager.instance.helpMenu.SetActive(true);
    }
    public void Back3()
    {
        MainMenuManager.instance.helpMenu.SetActive(false);
        MainMenuManager.instance.settingsMenu.SetActive(true);
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
}
