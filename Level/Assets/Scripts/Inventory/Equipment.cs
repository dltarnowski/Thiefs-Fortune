using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public override void Use()
    {
        base.Use();

    }

    public override void Equip()
    {
        base.Equip();
        EquipmentManager.instance.Equip(this);

        RemoveFromInventory();
    }

    public override void unUse()
    {
        base.unUse();

        EquipmentManager.instance.Unequip((int)this.equipmentSlot);

    }
}
