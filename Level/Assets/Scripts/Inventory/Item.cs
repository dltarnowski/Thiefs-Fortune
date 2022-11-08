using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    new public string name = "New Item";
    public EquipmentSlot equipmentSlot;
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public float strength;
    public GameObject model;
    public int numOfItems = 0;
    public int buyPrice;
    public int sellPrice;


    public virtual void Use()
    {
        // use the item
        // Something might happen
    }    

    public virtual void unUse()
    {

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public virtual void Equip()
    {

    }

}

public enum EquipmentSlot { Gun, Sword, Ammo, HP, blackSpotPotion };

