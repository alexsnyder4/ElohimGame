using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Items> items = new();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Items item)
    {
        items.Add(item);
    }
    public void Remove(Items item)
    {
        items.Remove(item);
    }
}
