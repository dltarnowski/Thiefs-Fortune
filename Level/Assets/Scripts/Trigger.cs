using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Trigger : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Explode());
    }
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(5);
        transform.GetComponent<Fracture>().Trigger();
    }
}