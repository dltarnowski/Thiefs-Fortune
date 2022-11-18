using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public Item item;
    [SerializeField] public int despawn;

    bool containsItem;
    bool isSwapped;

    private void Start()
    {
        if(!(item is Consumable))
            item = Instantiate(item);
        if(despawn > 0)
            Destroy(gameObject, despawn);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < Inventory.instance.items.Count; i++)
            {
                if (Inventory.instance.items[i].name == item.name)
                    containsItem = true;
            }

            for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
            {
                if (EquipmentManager.instance.currentEquipment[i] != null)
                    if (EquipmentManager.instance.currentEquipment[i].name == item.name)
                        containsItem = true;
            }

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

            if (!containsItem && item is Weapon)
                isSwapped = Inventory.instance.Add(item);

            if(TutorialManager.instance.tutorialActive && TutorialManager.instance.ammoBag != null)
            {
                TutorialManager.instance.ammoBag.SetActive(false);
            }

            if(TutorialManager.instance.tutorialActive && TutorialManager.instance.combatTrigger)
            {
                TutorialManager.instance.pickedUp = true;
            }

            if(isSwapped || !(item is Weapon))
                Destroy(gameObject);

        }
    }
}
