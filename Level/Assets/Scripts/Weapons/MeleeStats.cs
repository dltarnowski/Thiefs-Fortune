using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class MeleeStats : ScriptableObject
{
    public float swingSpeed;
    public int meleeDamage;
    public int hitsUntilBrokenStartAmmount;
    public int hitsUntilBrokenCurrentAmount;
    public GameObject meleeModel;
    public AudioClip meleeSound;
    public GameObject meleeHitEffect;

    [Header("-----Recoil-----")]
    public float moveX;
    public float moveY;
    public float moveZ;

    public float snappiness;
    public float returnSpeed;
}
