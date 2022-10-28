using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    public ItemSlot itemSlot;
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public float strength;
    public GameObject model;


    public virtual void Use()
    {
        // use the item
        // Something might happen

        Debug.Log("Using" + name);
    }    

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}

public enum ItemSlot { Gun1, Gun2, Sword1, Sword2, potion, ammo }

