using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneTransition : MonoBehaviour
{
    public static CutsceneTransition instance;
    public float transitionTime = 10f;
    void Start()
    {
        NextScene();
    }

    public void NextScene()
    {
        StartCoroutine(SceneTimer(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator SceneTimer(int buildIndex)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(buildIndex);
    }
}
