using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTransition : MonoBehaviour
{
    public bool Skip = false;
    public static CutsceneTransition instance;
    public float transitionTime = 10f;

    void Awake()
    {
        instance = this;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        NextScene();
    }
    void Update()
    {
        if(Skip == false && Input.anyKey)
        {
            SkipScene();
        }
    }

    public void NextScene()
    {
        StartCoroutine(SceneTimer(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private void SkipScene()
    {
        Skip = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator SceneTimer(int buildIndex)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(buildIndex);
    }
}
