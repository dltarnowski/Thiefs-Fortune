using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTrigger : MonoBehaviour
{
    public float transitionTime = 33;

    void Update()
    {
        if (transitionTime > 0)
        {
            transitionTime -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        }
    }
}
