using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] int speed;
    [SerializeField] int damage;
    [SerializeField] int destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * speed;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.takeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Ranged") || other.CompareTag("Melee"))
        {
            other.GetComponent<enemyAI>().takeDamage(damage);
            Destroy(gameObject);
        }

    }
}
