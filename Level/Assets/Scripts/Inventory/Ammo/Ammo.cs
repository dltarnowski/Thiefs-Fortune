using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Inventory/Ammo")]
public class Ammo : Consumable
{
    public override void Use()
    {
        base.Use();
        if(EquipmentManager.instance.currentEquipment[0] != null)
        {
            if (EquipmentManager.instance.currentEquipment[0] is Gun)
            {
                Gun currGun = EquipmentManager.instance.currentEquipment[0] as Gun;
                currGun.ammoCount = currGun.ammoStart;
                EquipmentManager.instance.currentEquipment[0] = currGun;
                if(this.numOfItems >= 0)
                {
                    if (this.numOfItems == 0)
                    {
                        EquipmentManager.instance.currentEquipment[(int)this.equipmentSlot] = null;
                    }
                    else
                        this.numOfItems--;
                Inventory.instance.onItemChangedCallback();
                }
            }
        }
    }
}
