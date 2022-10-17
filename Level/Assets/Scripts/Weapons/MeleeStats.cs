using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStats : ScriptableObject
{
    public float swingSpeed;
    public int damage;
    public int hitsUntilBroken;
    public GameObject model;
    public AudioClip sound;
    public GameObject hitEffect;
}
