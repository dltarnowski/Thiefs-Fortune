using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public Item item;

    bool isSwapped;

    private void Start()
    {
        Destroy(gameObject, 30);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(item is Currency)
            {
                gameManager.instance.currencyNumber += (int)item.strength;
                Destroy(gameObject);
                return;
            }   
            if(item is blackSpotPotion)
            {
                gameManager.instance.blackspot.blackSpotMultiplier *= .9f;
                Destroy(gameObject);
                return;
            }

            if(!(item is Weapon))
            {
                if (Inventory.instance.items.Contains(item))
                    Inventory.instance.items[Inventory.instance.items.IndexOf(item)].numOfItems++;
            }


            if(!Inventory.instance.items.Contains(item))
                isSwapped = Inventory.instance.Add(item);

            if(isSwapped || !(item is Weapon))
                Destroy(gameObject);
        }
    }
}
