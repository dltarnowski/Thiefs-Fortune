using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void respawn()
    {
        gameManager.instance.playerScript.respawn();
        gameManager.instance.cursorUnlockUnpause();
    }
    public void quit()
    {
        Application.Quit();
    }
   
}
