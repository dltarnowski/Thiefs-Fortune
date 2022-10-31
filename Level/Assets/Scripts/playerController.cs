using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class playerController : MonoBehaviour
{

    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject thirdPersonCam_Obj;
    [SerializeField] GameObject firstPersonCam_Obj;
    [SerializeField] Camera thirdPersonCam_Cam;
    [SerializeField] Camera firstPersonCam_Cam;
    public Animator anim;
    public GameObject itemDropPoint;


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

    [Header("----- Weapon Stats -----")]
    [SerializeField] float headShotMultiplier;
    [SerializeField] Recoil recoilScript;
    public ParticleSystem gunSmoke;
    public GameObject weaponModel;
    public Gun gunStats;
    public Sword swordStat;
    [SerializeField] AudioClip[] gruntAudio;


    [Header("----- Audio -----")]
    [SerializeField] public AudioSource aud;
    [SerializeField] AudioClip[] playerHurtAud;
    [Range(0, 1)] [SerializeField] float playerHurtAudVol;
    [SerializeField] AudioClip[] playerStepsAud;
    [SerializeField] AudioClip[] playerStepsAudSand;
    [Range(0, 1)] [SerializeField] float playerStepsAudVol;
    float currVolume;
    float currGunVolume;

    private Vector3 playerVelocity;
    private int timesJumped;
    [Header("----- Misc. -----")]
    public bool isShooting;
    public int selectItem;
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
        //thirdPersonCam_Cam.enabled = true;
        //firstPersonCam_Cam.enabled = false;
        thirdPersonCam_Obj.SetActive(true);
        firstPersonCam_Obj.SetActive(false);
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
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        ItemSelect();

       

        if (currVolume != gameManager.instance.PlayerAudioSlider.value)
        {
            ChangePlayerVolume();
        }

        /*
        if (currGunVolume != gameManager.instance.GunVolumeSlider.value)
        {
            ChangeGunVolume();
        }
        */

        movement();

        StartCoroutine(PlaySteps());

        if (weaponModel.GetComponent<MeshFilter>().sharedMesh == gunStats.model.GetComponent<MeshFilter>().sharedMesh 
            && EquipmentManager.instance.currentEquipment[0] == gunStats)
        {
            anim.SetBool("IsRanged", true);
            StartCoroutine(shoot());
        }
        if (weaponModel.GetComponent<MeshFilter>().sharedMesh == swordStat.model.GetComponent<MeshFilter>().sharedMesh 
            && EquipmentManager.instance.currentEquipment[1] == swordStat && swordStat.hitsUntilBrokenCurrentAmount >= 0)
        {
            anim.SetBool("IsRanged", false);
            StartCoroutine(swing());
        }

        HP = Mathf.Clamp(HP, 0, HPOrig);
        updatePlayerHUD();

    }

    public IEnumerator shoot()
    {
        if (!gameManager.instance.npcDialogue.activeSelf && !gameManager.instance.shopInventory.activeSelf && !gameManager.instance.pauseMenu.activeSelf && !gameManager.instance.deathMenu.activeSelf)
        {
            if (Input.GetButton("Fire1") && !isShooting && gunStats.ammoCount > 0)
            {
                isShooting = true;
                gunStats.ammoCount--;
                //gameManager.instance.ReduceAmmo();
                //gameManager.instance.ammoCount = gunStats.ammoCount;

                RaycastHit hit;
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * gunStats.distance, Color.red, 20);
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, gunStats.distance))
                {
                    //  -------      WAITING ON IDAMAGE      -------
                    if (hit.collider.GetComponent<IDamage>() != null)
                    {
                        if (hit.GetType() == typeof(SphereCollider) && !hit.collider.isTrigger)
                            hit.collider.GetComponent<IDamage>().takeDamage((int)gunStats.strength * (int)headShotMultiplier);
                        else
                            hit.collider.GetComponent<IDamage>().takeDamage((int)gunStats.strength);
                        Instantiate(gunStats.hitFX, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                    }
                }

                aud.PlayOneShot(gunStats.sound);
                gameManager.instance.recoilScript.RecoilFire();
                gunSmoke.transform.localPosition = gunStats.muzzleLocations[barrel].position;
                gunSmoke.Play();


                if (barrel >= gunStats.muzzleLocations.Count - 1)
                    barrel = 0;
                else
                    barrel++;

                yield return new WaitForSeconds(gunStats.speed);
                isShooting = false;

            }
        }
    }

    void movement()
    {
        //Reset jump
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }

        //3rd vs. 1st person camera toggle
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            thirdPersonCam_Obj.SetActive(false);
            firstPersonCam_Obj.SetActive(true);
            Debug.Log("First Person");
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            thirdPersonCam_Obj.SetActive(true);
            firstPersonCam_Obj.SetActive(false);
            Debug.Log("Third Person");
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
            Debug.Log("Jumped");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    IEnumerator PlaySteps()
    {
        if (move.magnitude > 0.3f && !playingSteps && controller.isGrounded && !isOnSand)
        {
            playingSteps = true;

            aud.PlayOneShot(playerStepsAud[Random.Range(0, playerStepsAud.Length - 1)], currVolume);

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
    IEnumerator swing()
    {
        if (!gameManager.instance.npcDialogue.activeSelf && !gameManager.instance.shopInventory.activeSelf && !gameManager.instance.pauseMenu.activeSelf && !gameManager.instance.deathMenu.activeSelf)
        {
            if (Input.GetButton("Fire1") && !isSwinging)
            {
                isSwinging = true;

                anim.SetTrigger("Attacking");
                aud.PlayOneShot(gruntAudio[Random.Range(0, gruntAudio.Length)]);

                RaycastHit hit;
                if (Physics.BoxCast(Camera.main.transform.position, transform.lossyScale, Camera.main.transform.forward, out hit, Camera.main.transform.rotation, swordStat.distance))
                {
                    if (hit.collider.GetComponent<IDamage>() != null)
                    {
                        swordStat.hitsUntilBrokenCurrentAmount--;
                        hit.collider.GetComponent<IDamage>().takeDamage((int)swordStat.strength);
                        Instantiate(swordStat.hitFX, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                    }
                }

                recoilScript.MeleeSwing();

                yield return new WaitForSeconds(swordStat.speed);

                isSwinging = false;
            }
        }
    }

    void ItemSelect()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && EquipmentManager.instance.currentEquipment[0] != null)
        {
            EquipmentManager.instance.currentEquipment[0].Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && EquipmentManager.instance.currentEquipment[1] != null)
        {
            EquipmentManager.instance.currentEquipment[1].Use();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && EquipmentManager.instance.currentEquipment[2] != null)
            EquipmentManager.instance.currentEquipment[2].Use();
        if (Input.GetKeyDown(KeyCode.Alpha4) && EquipmentManager.instance.currentEquipment[3] != null)
            EquipmentManager.instance.currentEquipment[3].Use();
    }

    public void takeDamage(int dmg)
    {
        if (HP + dmg > HPOrig)
            HP = HPOrig;
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
    public void ChangePlayerVolume()
    {
        aud.volume = gameManager.instance.PlayerAudioSlider.value;
        currVolume = aud.volume;
    }
    public void ChangeGunVolume()
    {
        aud.volume = gameManager.instance.GunVolumeSlider.value;
        currGunVolume = aud.volume;
    }
}
