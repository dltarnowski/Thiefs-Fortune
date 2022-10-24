using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] int speed;
    [SerializeField] int damage;
    [SerializeField] int destroyTime;
    [SerializeField] float arcMultiplier;
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject CannonCamera;
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(CompareTag("Bullet"))
            rb.velocity = transform.forward * speed;
        else if(CompareTag("CannonBall"))
        {
            CannonCamera = GameObject.FindGameObjectWithTag("CannonCamera");
            if (CannonCamera != null && CannonCamera.transform.parent.GetChild(0).GetChild(0).gameObject.CompareTag("Barrel"))
            {
                barrel = CannonCamera.transform.parent.GetChild(0).GetChild(0).gameObject;
                direction = transform.forward + (Vector3.up * (barrel.transform.rotation.normalized.x/-1 * arcMultiplier));
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
        }
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
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
            if(other.CompareTag("Tower"))
            {
                other.GetComponent<Trigger>().takeDamage(damage);
            }

        }
}
