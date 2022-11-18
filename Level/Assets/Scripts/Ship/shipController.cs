using UnityEngine;

public class shipController : MonoBehaviour
{
    [SerializeField] GameObject shipCam;
    [SerializeField] GameObject[] sails;
    [SerializeField] GameObject sailsUp;
    [SerializeField] GameObject sailsDown;
    [SerializeField] public GameObject playerPos;
    [SerializeField] shipMovement shipMove;
    //[SerializeField] Animator anim;

    public bool controllingShip;
    public bool onShip;
    private void Start()
    {
        gameManager.instance.shipControllerScript = this;
    }
    // Update is called once per frame
    void Update()
    {
        if(onShip && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.instance.CurrentObjectiveMiniMapIcon();
            gameManager.instance.hint.SetActive(false);
            gameManager.instance.playerScript.enabled = controllingShip;
            gameManager.instance.mainCamera.SetActive(controllingShip);
            gameManager.instance.playerScript.anim.enabled = !gameManager.instance.playerScript.anim.enabled;

            if(shipMove.wake.isPlaying)
            {
                shipMove.wake.Stop();
            }

            if (shipMove.aud.isPlaying)
                shipMove.aud.Stop();


            sailsUp.SetActive(controllingShip);
            sailsDown.SetActive(!controllingShip);
            shipMove.enabled = !controllingShip;
            shipCam.SetActive(!controllingShip);
            controllingShip = !controllingShip;

            if (shipMove.enabled == false)
                shipMove.speed = 0;
            //if(anim != null)
            //    anim.SetBool("PlayerControllingShip", controllingShip);
        }
        if (shipCam.activeSelf)
            ChangeParent();
        else
        {
            RevertParent();
        }

        gameManager.instance.playerScript.MapSelect();
    }

    void ChangeParent()
    {
        gameManager.instance.player.transform.position = playerPos.transform.position;
        gameManager.instance.player.transform.rotation = playerPos.transform.rotation;
        gameManager.instance.player.transform.parent = transform;
    }

    void RevertParent()
    {
        if (gameManager.instance.player != null)
            gameManager.instance.player.transform.parent = null;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.instance.hint.SetActive(true);
            onShip = true;
            TutorialManager.instance.tutorialProgress = 6;

            if (TutorialManager.instance.dialogueBox.activeSelf)
                TutorialManager.instance.dialogueBox.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onShip = false;
        }
        
        if(onShip == false)
            gameManager.instance.hint.SetActive(false);
    }
}
