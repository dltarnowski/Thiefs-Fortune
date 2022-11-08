using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Inventory/Gun")]
public class Gun : Weapon
{
    public int ammoStart;
    public int ammoCount;
    public List<Transform> muzzleLocations;


    public virtual void PickUp(Gun weapon)
    {
        name = weapon.name;
        strength = weapon.strength;
        speed = weapon.speed;
        model = weapon.model;

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
}
