using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonCameraControls : MonoBehaviour
{
    [SerializeField] int sensHort;
    [SerializeField] int sensVert;

    [SerializeField] int lockVertMin;
    [SerializeField] int lockVertMax;    
    [SerializeField] int lockHorzMin;
    [SerializeField] int lockHorzMax;

    [SerializeField] bool invert;

    [SerializeField] GameObject barrel;
    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
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
        barrel.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // rotate the player
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
