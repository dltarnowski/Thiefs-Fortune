using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float damage;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damage = gameManager.instance.playerScript.HPOrig / 5;
            anim.SetTrigger("open");
            StartCoroutine(FinishAnimation());
            gameManager.instance.playerScript.takeDamage((int)damage);
        }
    }

    IEnumerator FinishAnimation()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("IsOpen", true);
    }
}
