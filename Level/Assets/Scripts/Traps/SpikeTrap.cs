using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] bool timedSpikes;
    [SerializeField] float secondsUntilSpikes;
    [SerializeField] Vector3 originalColliderPos;
    [SerializeField] Collider triggerCollider;
    bool openClose = true;
    float damage;
    bool damageToPlayer;
    bool playerInTrigger;

    private void FixedUpdate()
    {
        if (timedSpikes)
            if (Time.fixedTime % secondsUntilSpikes == 0)
                ActivateSpikes();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInTrigger = true;
        damageToPlayer = true;

        if (!timedSpikes)
        {
            if (other.CompareTag("Player"))
            {
                damage = gameManager.instance.playerScript.HPOrig / 5;
                anim.SetTrigger("open");
                openClose = true;
                StartCoroutine(FinishAnimation());
                gameManager.instance.playerScript.takeDamage((int)damage);
                damageToPlayer = false;
            }
        }
        else if (timedSpikes)
        {
            if (other.CompareTag("Player"))
            {
                if (anim.GetBool("IsOpen"))
                {
                    if (damageToPlayer)
                        gameManager.instance.playerScript.takeDamage((int)damage);
                    damageToPlayer = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageToPlayer = false;
            playerInTrigger = false;
        }
    }

    IEnumerator FinishAnimation()
    {
        yield return new WaitForSeconds(.1f);
        anim.SetBool("IsOpen", openClose);
        openClose = !openClose;

    }

    void ActivateSpikes()
    {
        damage = gameManager.instance.playerScript.HPOrig / 5;

        if (openClose)
        {
            anim.SetTrigger("open");
            StartCoroutine(FinishAnimation());

            if (damageToPlayer || playerInTrigger)
                gameManager.instance.playerScript.takeDamage((int)damage);
        }
        else
        {
            damageToPlayer = false;
            anim.SetTrigger("close");
            StartCoroutine(FinishAnimation());
        }
    }
}
