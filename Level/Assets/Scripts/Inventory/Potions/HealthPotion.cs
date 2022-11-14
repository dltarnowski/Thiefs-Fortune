using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Inventory/HealthPotion")]
public class HealthPotion : Consumable
{

    // Start is called before the first frame update
    public override void Use()
    {
        base.Use();
        gameManager.instance.playerScript.GetHealth(-(int)this.strength);
        if (this.numOfItems >= 0)
        {
            if (this.numOfItems == 0)
            {
                EquipmentManager.instance.currentEquipment[(int)this.equipmentSlot] = null;
                Inventory.instance.onItemChangedCallback();
                // Play UI message that alerts player
            }
            else
                this.numOfItems--;
        }
    }
}
