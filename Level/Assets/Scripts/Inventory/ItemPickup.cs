using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public Item item;
    [SerializeField] public int despawn;

    bool isSwapped;

    private void Start()
    {
        item = Instantiate(item);
        if(despawn > 0)
            Destroy(gameObject, despawn);    
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

            if(item is Consumable)
            {
                if (Inventory.instance.items.Contains(item))
                    Inventory.instance.items[Inventory.instance.items.IndexOf(item)].numOfItems++;
                else if (EquipmentManager.instance.currentEquipment[2] != null)
                    EquipmentManager.instance.currentEquipment[2].numOfItems++;
                else if (EquipmentManager.instance.currentEquipment[3] != null)
                    EquipmentManager.instance.currentEquipment[3].numOfItems++;
                else
                    item.numOfItems = 1;
            }

            if(!Inventory.instance.items.Contains(item) && EquipmentManager.instance.currentEquipment[2] == null && item.name == "Ammo")
                isSwapped = Inventory.instance.Add(item);
            if(!Inventory.instance.items.Contains(item) && EquipmentManager.instance.currentEquipment[3] == null && item.name == "HealthPotion")
                isSwapped = Inventory.instance.Add(item);

            if(TutorialManager.instance.tutorialActive && TutorialManager.instance.ammoBag != null)
            {
                TutorialManager.instance.ammoBag.SetActive(false);
            }

            if(isSwapped || !(item is Weapon))
                Destroy(gameObject);

        }
    }
}
