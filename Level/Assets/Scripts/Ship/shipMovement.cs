using UnityEngine;

public class shipMovement : MonoBehaviour
{
    Vector3 move;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

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
