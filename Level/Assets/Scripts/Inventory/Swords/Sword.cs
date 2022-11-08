using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Inventory/Sword")]
public class Sword : Weapon
{
    public int hitsUntilBrokenCurrentAmount;
    public int hitsUntilBrokenStartAmount;
    public virtual void PickUp(Sword weapon)
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
        hitsUntilBrokenCurrentAmount = weapon.hitsUntilBrokenCurrentAmount;
        hitsUntilBrokenStartAmount = weapon.hitsUntilBrokenStartAmount;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
