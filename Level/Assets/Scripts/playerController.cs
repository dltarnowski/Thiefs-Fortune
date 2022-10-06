using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;


    [Header("----- Player Stats -----")]
    [Range(1, 5)] [SerializeField] float playerSpeed;
    [Range(8, 15)] [SerializeField] float jumpHeight;
    [Range(-5, -35)] [SerializeField] float gravityValue;
    [Range(1, 3)] [SerializeField] int jumpsMax;

    [Header("----- Gun Stats -----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shootDamage;
    [SerializeField] GameObject gunModel;
    [SerializeField] List<GunStats> gunStat = new List<GunStats>();

    private Vector3 playerVelocity;
    private int timesJumped;
    bool isShooting;
    int selectGun;


    void Start()
    {
        
    }


    void Update()
    {
       movement();
       StartCoroutine(shoot());
       GunSelect();
    }

    void movement()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") +
                       transform.forward * Input.GetAxis("Vertical");

        controller.Move(move * Time.deltaTime * playerSpeed);


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
        if (gunStat.Count > 0 && Input.GetButton("Shoot") && !isShooting)
        {
            isShooting = true;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
            {
               //  -------      WAITING ON IDAMAGE      -------
               // if (hit.collider.GetComponent<IDamage>() != null)
               //     hit.collider.GetComponent<IDamage>().takeDamage(shootDamage);
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
        gunModel.GetComponent<MeshFilter>().sharedMesh = stats.gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = stats.gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        gunStat.Add(stats);
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

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunStat[selectGun].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunStat[selectGun].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    }
}
