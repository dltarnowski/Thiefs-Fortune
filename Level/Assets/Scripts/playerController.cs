using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{

    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;
    public Animator anim;


    [Header("----- Player Stats -----")]
    [Range(1, 5)] [SerializeField] float playerSpeed;
    [Range(2, 5)] [SerializeField] float runSpeed;
    [Range(1, 15)] public float jumpHeight;
    public float jumpHeightOrig;
    [Range(-1, -35)] public float gravityValue;
    public float gravityValueOrig;
    [Range(1, 3)] [SerializeField] int jumpsMax;
    [Range(0.1f, 1.0f)] [SerializeField] float crouchHeight;
    //Health
    public float HP;
    public float lerpTime;
    public float HPOrig;
    public float HPLoss = 2f;
    //Stamina
    public float Stam;
    public float maxStamina;
    public float drainValue;

    [Header("----- Gun Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shootDamage;
    [SerializeField] int headShotMultiplier;
    public GameObject gunModel;
    [SerializeField] public int ammoCount;
    public List<GunStats> gunStat = new List<GunStats>();
    [SerializeField] Recoil recoilScript;
    public List<Transform> muzzleLocations = new List<Transform>();
    ParticleSystem gunSmoke;

    [Header("----- Melee Stats -----")]
    [SerializeField] float swingSpeed;
    [SerializeField] int meleeDamage;
    [SerializeField] int hitsUntilBrokenCurrentAmount;
    [SerializeField] float swingDist;
    public GameObject meleeModel;
    public AudioClip meleeSound;
    public GameObject meleeHitEffect;
    public List<MeleeStats> meleeStat = new List<MeleeStats>();
    [SerializeField] AudioClip[] gruntAudio;


    [Header("----- Audio -----")]
    public AudioSource aud;
    [SerializeField] AudioClip[] playerHurtAud;
    [Range(0, 1)] [SerializeField] float playerHurtAudVol;
    [SerializeField] AudioClip[] playerStepsAud;
    [SerializeField] AudioClip[] playerStepsAudSand;
    [Range(0, 1)] [SerializeField] float playerStepsAudVol;

    private Vector3 playerVelocity;
    private int timesJumped;
    [Header("----- Misc. -----")]
    public bool isShooting;
    public int selectGun;
    public int selectMelee;
    public bool gunGrabbed;
    bool playingSteps;
    bool isSprinting;
    bool canSprint = true;
    bool isSwinging;
    [SerializeField] bool isOnSand;
    Vector3 move;

    public int barrel;
    private Color staminColor;
    public bool isUnderwater;
    void Start()
    {
        HPOrig = HP;
        gameManager.instance.playerDamageIndicator.GetComponent<Animator>().SetFloat("HP", HP);
        maxStamina = Stam;
        staminColor = new Color(0f, 250f, 253f, 255f);
        respawn();
        recoilScript = transform.Find("Main Camera/Camera Recoil").GetComponent<Recoil>();
        gunSmoke = GetComponentInChildren<ParticleSystem>();
        jumpHeightOrig = jumpHeight;
        gravityValueOrig = gravityValue;
    }


    void Update()
    {
        movement();
        StartCoroutine(PlaySteps());
        if (gunModel.activeSelf)
            StartCoroutine(shoot());
        else if (meleeModel.activeSelf)
            StartCoroutine(swing());
        SelectMeleeOrGun();
        if (gunModel.activeSelf)
            GunSelect();
        else
            MeleeSelect();
        HP = Mathf.Clamp(HP, 0, HPOrig);
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
        {
            anim.SetBool("IsCrouched", true);
            transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x,
                                                                    transform.GetChild(0).localPosition.y - crouchHeight,
                                                                    transform.GetChild(0).localPosition.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && Cursor.lockState == CursorLockMode.Locked)
        {
            anim.SetBool("IsCrouched", false);
            transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x,
                                                                    transform.GetChild(0).localPosition.y + crouchHeight,
                                                                    transform.GetChild(0).localPosition.z);
        }

        //Move
        if (isUnderwater)
        {
            move = (transform.right * Input.GetAxis("Horizontal")) / 3 +
                       (transform.forward * Input.GetAxis("Vertical")) / 3;
        }
        else
        {
            move = transform.right * Input.GetAxis("Horizontal") +
                           transform.forward * Input.GetAxis("Vertical");
        }

        anim.SetFloat("Speed", move.normalized.magnitude);

        if (anim.GetFloat("Speed") > 0)
            anim.SetBool("IsWalking", true);
        else
            anim.SetBool("IsWalking", false);


        //Run
        if(canSprint == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(move * Time.deltaTime * playerSpeed * runSpeed);
                isSprinting = true;
                if (Stam > 0)
                {
                    gameManager.instance.staminaBar.color = staminColor;
                    canSprint = true;
                    DecreaseStamina();
                }
                if (Stam <= 0)
                {
                    canSprint = false;
                    gameManager.instance.staminaBar.color = Color.red;
                }

            }
            else
            {
                controller.Move(move * Time.deltaTime * playerSpeed);
                isSprinting = false;
                if (Stam < maxStamina)
                    IncreaseStamina();
            }
        }
        else
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
            isSprinting = false;
            if (Stam < maxStamina)
                IncreaseStamina();
            if (Stam >= maxStamina)
            {
                gameManager.instance.staminaBar.color = staminColor;
                canSprint = true;
            }    
        }

        //Jump
        if (Input.GetButtonDown("Jump") && timesJumped < jumpsMax)
        {
            anim.SetTrigger("IsJumping");
            playerVelocity.y = jumpHeight;
            timesJumped++;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator PlaySteps()
    {
        if (move.magnitude > 0.3f && !playingSteps && controller.isGrounded && !isOnSand)
        {
            playingSteps = true;

            aud.PlayOneShot(playerStepsAud[Random.Range(0, playerStepsAud.Length - 1)], playerStepsAudVol);

            if (isSprinting)
                yield return new WaitForSeconds(0.3f);
            else
                yield return new WaitForSeconds(0.4f);

            playingSteps = false;
        }
        else if (move.magnitude > 0.3f && !playingSteps && controller.isGrounded && isOnSand)
        {
            playingSteps = true;

            aud.PlayOneShot(playerStepsAudSand[Random.Range(0, playerStepsAudSand.Length - 1)], playerStepsAudVol);

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
                gameManager.instance.ReduceAmmo();
                gameManager.instance.ammoCount = gunStat[selectGun].ammoCount = ammoCount;

                RaycastHit hit;
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDist, Color.red, 20);
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
                {
                    //  -------      WAITING ON IDAMAGE      -------
                    if (hit.collider.GetComponent<IDamage>() != null)
                    {
                        if(hit.GetType() == typeof(SphereCollider) && !hit.collider.isTrigger)
                            hit.collider.GetComponent<IDamage>().takeDamage(shootDamage * headShotMultiplier);
                        else
                            hit.collider.GetComponent<IDamage>().takeDamage(shootDamage);
                        Debug.Log("Bodyshot");
                        Instantiate(gunStat[selectGun].hitEffect, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                    }
                }

                aud.PlayOneShot(gunStat[selectGun].gunSound);
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
    IEnumerator swing()
    {
        if (!gameManager.instance.npcDialogue.activeSelf && !gameManager.instance.shopInventory.activeSelf && !gameManager.instance.pauseMenu.activeSelf && !gameManager.instance.deathMenu.activeSelf)
        {
            if (meleeStat.Count > 0 && Input.GetButton("Fire1") && !isSwinging)
            {
                isSwinging = true;

                anim.SetTrigger("Attacking");
                aud.PlayOneShot(gruntAudio[Random.Range(0, gruntAudio.Length)]);

                RaycastHit hit;
                if (Physics.BoxCast(Camera.main.transform.position, transform.lossyScale, Camera.main.transform.forward, out hit, Camera.main.transform.rotation, swingDist))
                {
                    if (hit.collider.GetComponent<IDamage>() != null)
                    {
                        hitsUntilBrokenCurrentAmount--;
                        hit.collider.GetComponent<IDamage>().takeDamage(meleeDamage);
                        Instantiate(meleeStat[selectMelee].meleeHitEffect, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                    }
                }

                recoilScript.MeleeSwing();

                if (hitsUntilBrokenCurrentAmount <= 0)
                {
                    aud.PlayOneShot(meleeStat[selectMelee].meleeSound);
                    Destroy(meleeStat[selectMelee]);
                }

                yield return new WaitForSeconds(swingSpeed);

                isSwinging = false;
            }
        }
    }

    public void GunPickup(GunStats stats)
    {
        gunModel.SetActive(true);
        meleeModel.SetActive(false);

        shootRate = stats.shootSpeed;
        shootDist = stats.shootDist;
        shootDamage = stats.shootDamage;

        ammoCount = stats.ammoCount = stats.ammoStartCount;
        gameManager.instance.IncreaseAmmo();

        gunModel.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        //muzzleLocations[barrel] = stats.muzzleLocations[barrel];
        gameManager.instance.recoilScript.SetGunStatScript(stats);
        CopyMuzzleLocations(stats.muzzleLocations);

        gunStat.Add(stats);
        gunGrabbed = true;

        //For toggling animations
        anim.SetBool("IsMelee", false);
        anim.SetBool("IsRanged", true);



        if (gunStat.Count == 1)
            selectGun = 0;
        else
            selectGun++;

        barrel = 0;
    }

    public void MeleePickup(MeleeStats stats)
    {
        meleeModel.SetActive(true);
        gunModel.SetActive(false);

        swingSpeed = stats.swingSpeed;
        meleeDamage = stats.meleeDamage;
        hitsUntilBrokenCurrentAmount = stats.hitsUntilBrokenCurrentAmount = stats.hitsUntilBrokenStartAmmount;

        meleeModel.GetComponent<MeshFilter>().sharedMesh = stats.meleeModel.GetComponent<MeshFilter>().sharedMesh;
        meleeModel.GetComponent<MeshRenderer>().sharedMaterial = stats.meleeModel.GetComponent<MeshRenderer>().sharedMaterial;

        gameManager.instance.recoilScript.SetMeleeStatScript(stats);

        meleeStat.Add(stats);

        //For toggling animations
        anim.SetBool("IsMelee", true);
        anim.SetBool("IsRanged", false);

        if (meleeStat.Count == 1)
            selectMelee = 0;
        else
            selectMelee++;
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

    void MeleeSelect()
    {
        if (meleeStat.Count > 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectMelee < meleeStat.Count - 1)
            {
                selectMelee++;
                ChangeMelee();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectMelee > 0)
            {
                selectMelee--;
                ChangeMelee();
            }
        }
    }

    void ChangeGuns()
    {
        shootRate = gunStat[selectGun].shootSpeed;
        shootDist = gunStat[selectGun].shootDist;
        shootDamage = gunStat[selectGun].shootDamage;
        ammoCount = gunStat[selectGun].ammoCount;
        gameManager.instance.IncreaseAmmo();

        gameManager.instance.recoilScript.SetGunStatScript(gunStat[selectGun]);
        CopyMuzzleLocations(gunStat[selectGun].muzzleLocations);
        //muzzleLocations[barrel] = gunStat[selectGun].muzzleLocations[barrel];

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        barrel = 0;
    }

    void ChangeMelee()
    {
        swingSpeed = meleeStat[selectMelee].swingSpeed;
        meleeDamage = meleeStat[selectMelee].meleeDamage;
        hitsUntilBrokenCurrentAmount = meleeStat[selectMelee].hitsUntilBrokenCurrentAmount;

        gameManager.instance.recoilScript.SetMeleeStatScript(meleeStat[selectMelee]);

        meleeModel.GetComponent<MeshFilter>().sharedMesh = meleeStat[selectMelee].meleeModel.GetComponent<MeshFilter>().sharedMesh;
        meleeModel.GetComponent<MeshRenderer>().sharedMaterial = meleeStat[selectMelee].meleeModel.GetComponent<MeshRenderer>().sharedMaterial;

    }

    void SelectMeleeOrGun()
    {
        //if (gunStat.Count > 0 && meleeStat.Count <= 0)
        //{
        //    gunModel.SetActive(true);
        //    meleeModel.SetActive(false);
        //}
        //else if (gunStat.Count <= 0 && meleeStat.Count > 0)
        //{
        //    gunModel.SetActive(false);
        //    meleeModel.SetActive(true);
        //}

        if (Input.GetKeyDown(KeyCode.Mouse2) && gunStat.Count > 0 && meleeStat.Count > 0)
        {
            gunModel.SetActive(!gunModel.activeSelf);
            meleeModel.SetActive(!meleeModel.activeSelf);

            //For toggling animations
            if (gunModel.activeSelf)
            {
                anim.SetBool("IsMelee", false);
                anim.SetBool("IsRanged", true);
            }
            else if (meleeModel.activeSelf)
            {
                anim.SetBool("IsMelee", true);
                anim.SetBool("IsRanged", false);
            }
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        lerpTime = 0f;
        aud.PlayOneShot(playerHurtAud[Random.Range(0, playerHurtAud.Length - 1)], playerHurtAudVol);

        gameManager.instance.playerDamageIndicator.GetComponent<Animator>().SetFloat("HP", HP);

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
        float fillA = gameManager.instance.playerHPBar.fillAmount;
        float fillB = gameManager.instance.playerHPLost.fillAmount;
        float healthDiff = HP / HPOrig;
        if(fillB > healthDiff)
        {
            gameManager.instance.playerHPBar.fillAmount = healthDiff;
            gameManager.instance.playerHPLost.color = Color.red;
            lerpTime += Time.deltaTime;
            float percentComplete = lerpTime / HPLoss;
            percentComplete = percentComplete * percentComplete;
            gameManager.instance.playerHPLost.fillAmount = Mathf.Lerp(fillB, healthDiff, percentComplete);
        }
        if (fillA < healthDiff)
        {
            gameManager.instance.playerHPLost.color = Color.blue;
            gameManager.instance.playerHPLost.fillAmount = healthDiff;
            lerpTime += Time.deltaTime;
            float percentComplete = lerpTime / HPLoss;
            percentComplete = percentComplete * percentComplete;
            gameManager.instance.playerHPBar.fillAmount = Mathf.Lerp(fillA, gameManager.instance.playerHPLost.fillAmount, percentComplete);
        }
        //Coin Bag updates
        gameManager.instance.coinCountText.text = gameManager.instance.currencyNumber.ToString("F0");
        //Stamina bar updates
        gameManager.instance.staminaBar.fillAmount = (float)Stam / (float)maxStamina;
    }
    private void DecreaseStamina()
    {
        Stam -= drainValue * Time.deltaTime;
    }
    private void IncreaseStamina()
    {
        Stam += drainValue * Time.deltaTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sand"))
        {
            isOnSand = true;
            Debug.Log("Sand");
        }
        else if (!other.CompareTag("Ship") && !other.CompareTag("Sand"))
        {
            isOnSand = false;
            Debug.Log("Not Sand");
        }
    }
}
