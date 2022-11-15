using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTrigger : MonoBehaviour
{
    public float transitionTime;
    public int SceneSelect;

    void Update()
    {
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
}
