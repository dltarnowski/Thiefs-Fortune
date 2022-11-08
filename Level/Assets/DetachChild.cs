using UnityEngine;

public class DetachChild : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 1)
        {
            transform.DetachChildren();
            Destroy(gameObject);
        }
    }
}
