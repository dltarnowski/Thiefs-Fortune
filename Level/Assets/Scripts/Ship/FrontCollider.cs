using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCollider : MonoBehaviour
{
    shipMovement shipMovementScript;

    private void Start()
    {
        shipMovementScript = gameObject.GetComponentInParent<shipMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject island in gameManager.instance.islandObjects)
        {
            if (other.gameObject == island)
            {
                shipMovementScript.speed = -shipMovementScript.bounceOffObject;
                StartCoroutine(RecentCollision());
            }
        }
    }

    IEnumerator RecentCollision()
    {
        shipMovementScript.isColliding = true;
        yield return new WaitForSeconds(.1f);
        shipMovementScript.isColliding = false;
    }
}
