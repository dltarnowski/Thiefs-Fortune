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
    
    float playerX = 0;
    float playerY = .67f;
    float playerZ = .08f;

    public bool cannonView;

    // Start is called before the first frame update
    void Start()
    {
        cannonView = false;
        detectionLayer = LayerMask.GetMask("Interaction Objects");
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

    //Detect if object collision in the interaction object layer
    bool DetectObject()
    {
        return Physics.CheckSphere(detectionPoint.position, detectionRadius, detectionLayer);
    }

    //Set Game Object you have detected
    GameObject ObjectDetected()
    {
        return Physics.OverlapSphere(detectionPoint.position, detectionRadius, detectionLayer)[0].gameObject;
    }

    //Interact with Objects
    public void InteractWithObject()
    {
        //if the object is a cannon
        if (interactObject.CompareTag("Cannon"))
        {
            //change view
            if (!cannonView)
            {
                gameManager.instance.mainCamera.transform.position = new Vector3(interactObject.transform.position.x + interactObject.GetComponent<Cannons>().cameraXPos,
                                                                     interactObject.transform.position.y + interactObject.GetComponent<Cannons>().cameraYPos,
                                                                     interactObject.transform.position.z + interactObject.GetComponent<Cannons>().cameraZPos);
                gameManager.instance.mainCamera.GetComponent<Camera>().fieldOfView = 45;
            }
            else
            {
                gameManager.instance.mainCamera.transform.position = new Vector3(gameManager.instance.player.transform.position.x + playerX,
                                                                                 gameManager.instance.player.transform.position.y + playerY,
                                                                                 gameManager.instance.player.transform.position.z + playerZ);
                gameManager.instance.mainCamera.GetComponent<Camera>().fieldOfView = 60;
            }

            //change view bool and activate/deactivate gun model
            cannonView = !cannonView;
        }
    }
}
