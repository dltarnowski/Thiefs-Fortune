using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTrigger : MonoBehaviour
{
    public float transitionTime = 33;
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
