using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {
        offset = player.position;
        offset.y = transform.position.y;
        transform.position = offset;

        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
