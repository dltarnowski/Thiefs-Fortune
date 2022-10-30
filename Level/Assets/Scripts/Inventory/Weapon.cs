using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public abstract class Weapon : Equipment
{
    //Stats
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
    }

}



