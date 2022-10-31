using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMovement : MonoBehaviour
{
    Vector3 move;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        move = transform.forward * Input.GetAxis("Vertical");
        transform.position += move * speed * Time.deltaTime;
    }
}
