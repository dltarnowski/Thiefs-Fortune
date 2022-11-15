using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwtich : MonoBehaviour
{
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;

    bool switchCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            switchCam = !switchCam;
            cam1.SetActive(!switchCam);
            cam2.SetActive(switchCam);
        }
    }
}
