using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]


public class GunStats : ScriptableObject
{
    public float shootSpeed;
    public int shootDist;
    public int shootDamage;
    //public int ammoCount;
    public GameObject gunModel;
    public AudioClip gunSound;
    public GameObject hitEffect;
    public GameObject muzzleEffect;
    public Transform[] muzzleLocations;

    [Header("-----Recoil-----")]
    public float recoilX;
    public float recoilY;
    public float recoilZ;

    public float snappiness;
    public float returnSpeed;
}
