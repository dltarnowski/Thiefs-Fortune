using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    bool isEnding = false;
    public float transitionTime = 5f;

    void Update()
    {
        if (isEnding == true && Input.GetKeyDown(KeyCode.E))
        {
            EndingScene();
        }
    }

    public void EndingScene()
    {
        StartCoroutine(SceneTimer(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator SceneTimer(int buildIndex)
    {
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnding = true;
            gameManager.instance.hint.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hint.SetActive(false);
    }
}
