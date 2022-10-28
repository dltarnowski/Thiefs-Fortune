using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class Weapon : Item
{
    //Stats
    public WeaponSlot weaponSlot;
    public float speed;
    public AudioClip sound;
    public GameObject hitFX;
    public int distance;
    public float recoilX, recoilY, recoilZ;
    public float snappiness;
    public float returnSpeed;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum WeaponSlot { Gun1, Gun2, Sword1, Sword2 }