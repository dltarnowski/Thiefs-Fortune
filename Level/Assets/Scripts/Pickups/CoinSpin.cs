using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    Vector3 originalPos;

    [SerializeField] float bounceHeight = .5f;
    [SerializeField] float bounceSpeed = .25f;
    [SerializeField] int rotationSpeed = 45;

    private void Start()
    {
        originalPos = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(originalPos.x,
                                                    Mathf.PingPong(bounceSpeed * Time.time, bounceHeight) + originalPos.y,
                                                    originalPos.z);
        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
