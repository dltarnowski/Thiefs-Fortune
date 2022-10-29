using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public Item item;

    bool isSwapped;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isSwapped =Inventory.instance.Add(item);

            if(isSwapped)
                Destroy(gameObject);
        }
    }
}
