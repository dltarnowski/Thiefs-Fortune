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
    public int distance;
    public GameObject hitFX;
    public float recoilX, recoilY, recoilZ;
    public float snappiness;
    public float returnSpeed;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);

        RemoveFromInventory();
    }

    public override void unUse()
    {
        base.unUse();

        EquipmentManager.instance.Unequip((int)this.weaponSlot);

    }

}

