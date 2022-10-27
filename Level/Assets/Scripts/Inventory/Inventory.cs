using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Item item)
    {
        Debug.Log("Item Added");
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

}

