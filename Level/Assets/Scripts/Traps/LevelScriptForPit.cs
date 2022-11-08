using UnityEngine;

public class LevelScriptForPit : MonoBehaviour
{
    [SerializeField] MeshDeformer meshDeformer;
    [SerializeField] Vector3 point;

    [SerializeField] float radius;
    [SerializeField] float stepRadius;
    [SerializeField] float strength;
    [SerializeField] float stepStrength;

    // Start is called before the first frame update
    void Start()
    {
        meshDeformer = this.gameObject.transform.GetComponent<MeshDeformer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshDeformer.Deform(point, radius, stepRadius, strength, stepStrength, Vector3.down);
    }
}
