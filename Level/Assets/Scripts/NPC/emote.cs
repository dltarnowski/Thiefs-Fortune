using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emote : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] int emoteNum;
    [SerializeField] bool canEmote;
    void Start()
    {
        anim.SetBool("emote", canEmote);
        anim.SetInteger("emoteNum", emoteNum);
    }
}
