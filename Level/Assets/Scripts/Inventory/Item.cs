using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public float strength;

    public virtual void Use()
    {

    }    

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

