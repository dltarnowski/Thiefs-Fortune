using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{

    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;


    [Header("----- Player Stats -----")]
    [Range(1, 5)] [SerializeField] float playerSpeed;
    [Range(2, 5)] [SerializeField] float runSpeed;
    [Range(8, 15)] [SerializeField] float jumpHeight;
    [Range(-5, -35)] [SerializeField] float gravityValue;
    [Range(1, 3)] [SerializeField] int jumpsMax;
    [Range(0.1f, 1.0f)] [SerializeField] float crouchHeight;
    
    public int HP;
    public int HPOrig;

    [Header("----- Gun Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shootDamage;
    public GameObject gunModel;
    [SerializeField] public int ammoCount;
    [SerializeField] List<GunStats> gunStat = new List<GunStats>();

    private Vector3 playerVelocity;
    private int timesJumped;
    public bool isShooting;
    int selectGun;
    public bool gunGrabbed;


    void Start()
    {
        HPOrig = HP;
        respawn();
    }


    void Update()
    {
       movement();
       StartCoroutine(shoot());
       GunSelect();
       updatePlayerHUD();
    }

    void movement()
    {
        //Reset jump
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }

       

        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
            transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x,
                                                                    transform.GetChild(0).localPosition.y - crouchHeight,
                                                                    transform.GetChild(0).localPosition.z);
        if (Input.GetKeyUp(KeyCode.LeftControl))
            transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x,
                                                                    transform.GetChild(0).localPosition.y + crouchHeight,
                                                                    transform.GetChild(0).localPosition.z);

        Vector3 move = transform.right * Input.GetAxis("Horizontal") +
                       transform.forward * Input.GetAxis("Vertical");

        //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * Time.deltaTime * playerSpeed * runSpeed);
        }
        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
        }



        //Jump
        if (Input.GetButtonDown("Jump") && timesJumped < jumpsMax)
        {
            playerVelocity.y = jumpHeight;
            timesJumped++;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator shoot()
    {
        if (gunStat.Count > 0 && Input.GetButton("Fire1") && !isShooting)
        {
            isShooting = true;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
            {
               //  -------      WAITING ON IDAMAGE      -------
               if (hit.collider.GetComponent<IDamage>() != null)
                hit.collider.GetComponent<IDamage>().takeDamage(shootDamage);
            }

            Debug.Log("Shoot!");
            yield return new WaitForSeconds(shootRate);
            isShooting = false;
        }
    }

    public void GunPickup(GunStats stats)
    {
        shootRate = stats.shootSpeed;
        shootDist = stats.shootDist;
        shootDamage = stats.shootDamage;
        ammoCount = stats.ammoCount;
        gunModel.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        gunStat.Add(stats);
        gunGrabbed = true;
    }

    void GunSelect()
    {
        if (gunStat.Count > 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectGun < gunStat.Count - 1)
            {
                selectGun++;
                ChangeGuns();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectGun > 0)
            {
                selectGun--;
                ChangeGuns();
            }
        }
    }

    void ChangeGuns()
    {
        shootRate = gunStat[selectGun].shootSpeed;
        shootDist = gunStat[selectGun].shootDist;
        shootDamage = gunStat[selectGun].shootDamage;
        ammoCount = gunStat[selectGun].ammoCount;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        StartCoroutine(gameManager.instance.playerDamage());;
        if (HP <= 0)
        {
            gameManager.instance.Crosshair.SetActive(false);
            gameManager.instance.playerDamageFlash.SetActive(false);

            gameManager.instance.deathMenu.SetActive(true);
            gameManager.instance.cursorLockPause();
        }

    }

    public void updatePlayerHUD()
    {
        //Health bar updates
        gameManager.instance.playerHPBar.fillAmount = (float)HP / (float)HPOrig;
    }

    public void respawn()
    {
        if (gameManager.instance.pauseMenu)
        {
            gameManager.instance.pauseMenu.SetActive(false);
        }
        gameManager.instance.deathMenu.SetActive(false);
        controller.enabled = false;
        HP = HPOrig;
        updatePlayerHUD();
        gameManager.instance.Crosshair.SetActive(gameManager.instance.crossHairVisible);
        transform.position = gameManager.instance.spawnPosition.transform.position;
        controller.enabled = true;
    }
}
