using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    [SerializeField] Transform character;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(90, character.eulerAngles.y + 270, 0);
    }
}
