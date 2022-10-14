using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingTransition : MonoBehaviour
{
    public bool isEnding = false;
    [SerializeField] KeyCode interactKey;
    public float transitionTime = 5f;

    void Update()
    {
        if (isEnding == true && Input.GetKeyDown(interactKey))
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
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnding = true;
        }
    }
}
