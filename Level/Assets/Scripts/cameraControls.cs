using UnityEngine;

public class cameraControls : MonoBehaviour
{
    [SerializeField] public int sensHort;
    [SerializeField] public int sensVert;

    [SerializeField] int lockVertMin;
    [SerializeField] int lockVertMax;

    [SerializeField] bool invert;

    float xRotation;
    int currSliderValue;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ChangeSense();
    }

    void LateUpdate()
    {
        if (currSliderValue != (int)gameManager.instance.MSSlider.value)
            ChangeSense();
         // get input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensHort;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensVert;

        if (invert)
            xRotation += mouseY;
        else
            xRotation -= mouseY;

        // clamp camera rotation
        xRotation = Mathf.Clamp(xRotation, lockVertMin, lockVertMax);

        // rotate the camera on the x-axis
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // rotate the player
        transform.parent.Rotate(Vector3.up * mouseX);

    }

    public void ChangeSense()
    {
        currSliderValue = (int)gameManager.instance.MSSlider.value;
        sensHort = (int)gameManager.instance.MSSlider.value;
        sensVert = (int)gameManager.instance.MSSlider.value;
    }
}
