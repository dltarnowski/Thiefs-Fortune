using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAndSpin : MonoBehaviour
{
    Vector3 originalPos;

    float bounceHeight = .5f;
    float bounceSpeed = .25f;
    int rotationSpeed = 45;

    private void Start()
    {
        originalPos = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(originalPos.x,
                                                    Mathf.PingPong(bounceSpeed * Time.realtimeSinceStartup, bounceHeight) + originalPos.y,
                                                    originalPos.z);
        gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
