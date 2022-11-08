using System.Collections.Generic;
using UnityEngine;

public class ConsumableInventory : MonoBehaviour
{
    public static ConsumableInventory instance;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 2;

    public List<Consumable> items = new List<Consumable>();

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

    public bool Add(Consumable item)
    {
        if (items.Count >= space)
            return false;
        items.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true;
    }

    public void Remove(Consumable item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}

