using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Gun : Weapon
{
    public int distance;
    public int ammoStart;
    public int ammoCount;
    public List<Transform> muzzleLocations;

    bool isShooting;
    public float headShotMultiplier;

    public virtual void PickUp(Gun weapon)
    {
        name = weapon.name;
        strength = weapon.strength;
        speed = weapon.speed;

        model.GetComponent<MeshFilter>().sharedMesh = weapon.model.GetComponent<MeshFilter>().sharedMesh;
        model.GetComponent<MeshRenderer>().sharedMaterial = weapon.model.GetComponent<MeshRenderer>().sharedMaterial;

        sound = weapon.sound;
        hitFX = weapon.hitFX;
        recoilX = weapon.recoilX;
        recoilY = weapon.recoilY;
        recoilZ = weapon.recoilZ;
        snappiness = weapon.snappiness;
        returnSpeed = weapon.returnSpeed;
        distance = weapon.distance;
        ammoStart = weapon.ammoStart;
        ammoCount = weapon.ammoCount;
        CopyMuzzleLocations(weapon.muzzleLocations);
    }

    void CopyMuzzleLocations(List<Transform> list)
    {
        muzzleLocations.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            muzzleLocations.Add(list[i]);
        }
    }

    public IEnumerator shoot()
    {
        if (!gameManager.instance.npcDialogue.activeSelf && !gameManager.instance.shopInventory.activeSelf && !gameManager.instance.pauseMenu.activeSelf && !gameManager.instance.deathMenu.activeSelf)
        {
            if (gameManager.instance.inventory.items.Count > 0 && Input.GetButton("Fire1") && !isShooting && ammoCount > 0)
            {
                isShooting = true;

                ammoCount--;
                Debug.Log("SHOOT!");

                //gameManager.instance.ReduceAmmo();
                gameManager.instance.ammoCount = ammoCount;

                RaycastHit hit;
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * distance, Color.red, 20);
                if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, distance))
                {
                    //  -------      WAITING ON IDAMAGE      -------
                    if (hit.collider.GetComponent<IDamage>() != null)
                    {
                        if (hit.GetType() == typeof(SphereCollider) && !hit.collider.isTrigger)
                            hit.collider.GetComponent<IDamage>().takeDamage((int)strength * (int)headShotMultiplier);
                        else
                            hit.collider.GetComponent<IDamage>().takeDamage((int)strength);
                        Debug.Log("Bodyshot");
                        Instantiate(hitFX, hit.point, hit.collider.gameObject.transform.rotation, hit.collider.gameObject.transform);
                    }
                }

                gameManager.instance.playerScript.aud.PlayOneShot(sound);
                gameManager.instance.recoilScript.RecoilFire();
                gameManager.instance.playerScript.gunSmoke.transform.localPosition = muzzleLocations[gameManager.instance.playerScript.barrel].position;
                gameManager.instance.playerScript.gunSmoke.Play();


                if (gameManager.instance.playerScript.barrel >= muzzleLocations.Count - 1)
                    gameManager.instance.playerScript.barrel = 0;
                else
                    gameManager.instance.playerScript.barrel++;

                Debug.Log("Shoot!");
                yield return new WaitForSeconds(speed);
                isShooting = false;

            }
        }
    }
}
