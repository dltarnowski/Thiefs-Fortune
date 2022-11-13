using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsReturn : MonoBehaviour
{
    public bool Skip = false;
    public float transitionTime;
    public int SceneSelect;
    void Update()
    {
        if (Skip == false && Input.anyKey)
        {
            SkipScene();
        }
        if (transitionTime > 0)
        {
            transitionTime -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneSelect);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void SkipScene()
    {
        Skip = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - SceneSelect);
    }
}
