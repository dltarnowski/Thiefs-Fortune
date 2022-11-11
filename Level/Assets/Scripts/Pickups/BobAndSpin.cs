using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAndSpin : MonoBehaviour
{

    /////////**************** Height needs to be about twice the speed for it to work correctly ******************/////////////

    [SerializeField] float bounceHeight = .5f;
    [SerializeField] float bounceSpeed = .25f;
    [SerializeField] int rotationSpeed = 45;


    private void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector3(0, (Mathf.PingPong(bounceSpeed * Time.time, bounceHeight) - bounceHeight / 2) / 100, 0));

        if(CompareTag("Barrel"))
            gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        else
            gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
