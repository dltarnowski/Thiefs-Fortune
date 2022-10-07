using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] Transform detectionPoint;
    float detectionRadius = 1;
    [SerializeField] LayerMask detectionLayer;

    [SerializeField] KeyCode interactKey;
    [SerializeField] UnityEvent interactAction;

    [SerializeField] GameObject interactObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectObject())
        {
            interactObject = ObjectDetected();

            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

    bool DetectObject()
    {
        return Physics.CheckSphere(detectionPoint.position, detectionRadius, detectionLayer);
    }

    GameObject ObjectDetected()
    {
        return Physics.OverlapSphere(detectionPoint.position, detectionRadius, detectionLayer)[0].gameObject;
    }

    public void InteractWithObject()
    {
        if (interactObject.CompareTag("Cannon 1"))
        {
            GameObject.Find("Main Camera").SetActive(false);
            GameObject.Find("Cannon Camera 1").SetActive(true);
        }
        else if (interactObject.CompareTag("Cannon 2"))
        {
            GameObject.Find("Main Camera").SetActive(false);
            GameObject.Find("Cannon Camera 2").SetActive(true);
        }
    }
}
