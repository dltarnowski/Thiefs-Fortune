using System.Collections;
using UnityEngine;

public class shipMovement : MonoBehaviour
{
    public static shipMovement instance;
    Vector3 move;
    [SerializeField] shipCameraControl shipCam;
    [SerializeField] float speed;
    [SerializeField] float speedIncTimer;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] public ParticleSystem wake;
    bool isMoving;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            if(!wake.isPlaying)
                wake.Play();

            StartCoroutine(speedInc());
            move = transform.forward * Input.GetAxis("Vertical");
            transform.position += move * speed * Time.deltaTime;
            if(Input.GetAxis("Mouse X") != 0 )
            {
                shipCam.sensHort = speed * .65f;
            }
        }
        else
        {
            shipCam.sensHort = 0;
            speed = 0;
            wake.Stop();
        }
    }

    IEnumerator speedInc()
    {
        if(!isMoving)
        {
            isMoving = true;
            if(speed < maxSpeed)
                speed += 1;
            yield return new WaitForSeconds(speedIncTimer);
            isMoving = false;
        }
    }
}
