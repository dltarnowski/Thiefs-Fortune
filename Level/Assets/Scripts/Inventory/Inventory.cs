using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 6;

    public List<Item> items = new List<Item>();

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one instance of Inventory Found");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        items.Capacity = space;
    }

    public bool Add(Item item, int slot)
    {
        if(!item.isDefaultItem)
        {
            if(items[slot] == null)
                items[slot] = item;
            else if (items.Count >= space)
            {
                Debug.Log("Not enough room");
                return false;
            }


            if(onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        items[(int)item.itemSlot] = null;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}

