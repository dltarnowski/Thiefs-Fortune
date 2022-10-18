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
}
