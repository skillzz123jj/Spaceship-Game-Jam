using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (item == null) return false;
        if (items.Contains(item)) return false;
        items.Add(item);
        return true;
    }
    public void ClearAll()
    {
        items.Clear();
    }
    public bool Has(Item item)
    {
        return item != null && items.Contains(item);
    }
}
