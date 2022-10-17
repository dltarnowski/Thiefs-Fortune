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
    public List<GunStats> gunStat = new List<GunStats>();
    [SerializeField] Recoil recoilScript;

    [Header("----- Audio -----")]
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] playerHurtAud;
    [Range(0, 1)] [SerializeField] float playerHurtAudVol;
    [SerializeField] AudioClip[] playerStepsAud;
    [Range(0, 1)] [SerializeField] float playerStepsAudVol;

    private Vector3 playerVelocity;
    private int timesJumped;
    [Header("----- Misc. -----")]
    public bool isShooting;
    public int selectGun;
    public bool gunGrabbed;
    bool playingSteps;
    bool isSprinting;
    Vector3 move;

    public List<Transform> muzzleLocations = new List<Transform>();
    ParticleSystem gunSmoke;
    public int barrel;

    void Start()
    {
        HPOrig = HP;
        respawn();
        recoilScript = transform.Find("Main Camera/Camera Recoil").GetComponent<Recoil>();
        gunSmoke = GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        movement();
        StartCoroutine(PlaySteps());
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
        if (Input.GetKeyDown(KeyCode.LeftControl) && Cursor.lockState == CursorLockMode.Locked)
            transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x,
                                                                    transform.GetChild(0).localPosition.y - crouchHeight,
                                                                    transform.GetChild(0).localPosition.z);
        if (Input.GetKeyUp(KeyCode.LeftControl) && Cursor.lockState == CursorLockMode.Locked)
            transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x,
                                                                    transform.GetChild(0).localPosition.y + crouchHeight,
                                                                    transform.GetChild(0).localPosition.z);
        //Move
        move = transform.right * Input.GetAxis("Horizontal") +
                       transform.forward * Input.GetAxis("Vertical");

        //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * Time.deltaTime * playerSpeed * runSpeed);
            isSprinting = true;
        }
        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
            isSprinting = false;
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

    IEnumerator PlaySteps()
    {
        if (move.magnitude > 0.3f && !playingSteps && controller.isGrounded)
        {
            playingSteps = true;
            aud.PlayOneShot(playerStepsAud[Random.Range(0, playerStepsAud.Length - 1)], playerStepsAudVol);

            if (isSprinting)
                yield return new WaitForSeconds(0.3f);
            else
                yield return new WaitForSeconds(0.4f);

            playingSteps = false;
        }
    }

    IEnumerator shoot()
    {
        if (!gameManager.instance.npcDialogue.activeSelf && !gameManager.instance.shopInventory.activeSelf && !gameManager.instance.pauseMenu.activeSelf && !gameManager.instance.deathMenu.activeSelf)
        {
            if (gunStat.Count > 0 && Input.GetButton("Fire1") && !isShooting && ammoCount > 0)
            {
                isShooting = true;
                ammoCount--;
                gameManager.instance.ammoCount = ammoCount;

                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
                {
                    //  -------      WAITING ON IDAMAGE      -------
                    if (hit.collider.GetComponent<IDamage>() != null)
                        hit.collider.GetComponent<IDamage>().takeDamage(shootDamage);
                }

                recoilScript.RecoilFire();
                gunSmoke.transform.localPosition = gunStat[selectGun].muzzleLocations[barrel].position;
                gunSmoke.Play();


                if (barrel >= muzzleLocations.Count - 1)
                    barrel = 0;
                else
                    barrel++;

                Debug.Log("Shoot!");
                yield return new WaitForSeconds(shootRate);
                isShooting = false;
            }
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

        //muzzleLocations[barrel] = stats.muzzleLocations[barrel];
        gameManager.instance.recoilScript.SetGunStatScript(stats);
        CopyMuzzleLocations(stats.muzzleLocations);

        gunStat.Add(stats);
        gunGrabbed = true;

        if (gunStat.Count == 1)
            selectGun = 0;
        else
            selectGun++;
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

        gameManager.instance.recoilScript.SetGunStatScript(gunStat[selectGun]);
        CopyMuzzleLocations(gunStat[selectGun].muzzleLocations);
        //muzzleLocations[barrel] = gunStat[selectGun].muzzleLocations[barrel];

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        aud.PlayOneShot(playerHurtAud[Random.Range(0, playerHurtAud.Length - 1)], playerHurtAudVol);

        StartCoroutine(gameManager.instance.playerDamage());
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
        //Coin Bag updates
        gameManager.instance.coinCountText.text = gameManager.instance.currencyNumber.ToString("F0");
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
        gameManager.instance.Crosshair.SetActive(gameManager.instance.crossHairVisible);
        transform.position = gameManager.instance.spawnPosition.transform.position;
        controller.enabled = true;
    }

    void CopyMuzzleLocations(List<Transform> list)
    {
        muzzleLocations.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            muzzleLocations.Add(list[i]);
        }
    }
}
