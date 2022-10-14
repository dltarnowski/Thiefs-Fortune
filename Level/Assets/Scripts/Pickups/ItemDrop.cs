using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject itemModel;
    public static ItemDrop instance;
    void Awake()
    {
        instance = this;
    }

    public void DropItem()
    {
        Vector3 position = transform.position;
        GameObject item = Instantiate(itemModel, position, Quaternion.identity);
    }
}
