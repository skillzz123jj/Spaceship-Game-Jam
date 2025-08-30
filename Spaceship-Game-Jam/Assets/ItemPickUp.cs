using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public List<Item> items = new List<Item>();
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            item.isPickedUp = true;
            AddItem(item);
            Destroy(gameObject);
        }
    }
    
    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log($"Picked up {item.itemName}.");
    }


}
