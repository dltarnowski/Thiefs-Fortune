using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScriptForPit : MonoBehaviour
{
    [SerializeField] MeshDeformer meshDeformer;
    [SerializeField] Vector3 point;

    [SerializeField] float one;
    [SerializeField] float two;
    [SerializeField] float three;
    [SerializeField] float four;

    // Start is called before the first frame update
    void Start()
    {
        meshDeformer = this.gameObject.transform.GetComponent<MeshDeformer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshDeformer.Deform(point, one, two, three, four, Vector3.down);
    }
}
