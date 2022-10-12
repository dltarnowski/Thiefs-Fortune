using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]


public class GunStats : ScriptableObject
{
    public float shootSpeed;
    public int shootDist;
    public int shootDamage;
    public int ammoCount;
    public GameObject gunModel;
    public AudioClip gunSound;
    public GameObject hitEffect;
    public GameObject muzzleEffect;
}
