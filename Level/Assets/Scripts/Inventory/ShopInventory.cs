using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public static ShopInventory instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 12;

    public List<Item> items = new List<Item>();

    void Awake()
    {
        if (instance != null)
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

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
                return false;

            items.Add(item);


            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();

        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}

