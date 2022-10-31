using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] Transform player;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
