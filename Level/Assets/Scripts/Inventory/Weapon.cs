using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Weapon : Item
{
    //Stats
    public float speed;
    public GameObject model;
    public AudioClip sound;
    public GameObject hitFX;
    public float recoilX, recoilY, recoilZ;
    public float snappiness;
    public float returnSpeed;
}
