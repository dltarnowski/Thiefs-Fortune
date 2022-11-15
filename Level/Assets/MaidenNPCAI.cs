using UnityEngine;

public class MaidenNPCAI : MonoBehaviour
{
    public float facePlayerSpeed;
    public Animator anim;
    public GameObject cam;

    Vector3 playerDir;
    Vector3 originalNPC;

    private void Start()
    {
        originalNPC = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
    private void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;

        if (gameManager.instance.handmaiden)
        {
            facePlayer(playerDir);
            anim.SetBool("inRange", true);
        }
        else
        {
            facePlayer(originalNPC);
            anim.SetBool("inRange", false);
        }
    }

    public void facePlayer(Vector3 dir)
    {
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * facePlayerSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            gameManager.instance.hint.SetActive(true);
            gameManager.instance.npcCam = cam;
            gameManager.instance.handmaiden = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hint.SetActive(false);
        gameManager.instance.handmaiden = false;

    }
}