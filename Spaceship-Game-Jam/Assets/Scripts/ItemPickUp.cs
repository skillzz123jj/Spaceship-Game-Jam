using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    private ItemUI itemUI;
    private PlayerInventory playerInv;
    private bool playerInRange = false;

    private void Start()
    {
        itemUI = FindFirstObjectByType<ItemUI>();
        GameData.items.Clear();
        if (itemUI == null)
            Debug.LogWarning("ItemPickUp: ItemUI not found in scene.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        playerInv = other.GetComponent<PlayerInventory>();
        if (playerInv == null) playerInv = other.GetComponentInParent<PlayerInventory>();
 
        if (itemUI != null && item != null)
            itemUI.ShowItemText(item.itemName, transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        playerInv = null;

        if (itemUI != null)
            itemUI.HideItemText();
    }

    private void Update()
    {
        if (!playerInRange || playerInv == null) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (item != null) item.isPickedUp = true;

            if (!playerInv.items.Contains(item))
            {
                playerInv.items.Add(item);
                Debug.Log($"Added '{item.itemName}' to inventory. Count now: {playerInv.items.Count}");
                GameData.items.Add(item);
            }

            if (itemUI != null && item != null)
            {
                itemUI.AddLogLine($"Picked up: {item.itemName}");
                itemUI.HideItemText();
            }

            Destroy(gameObject);
        }
    }
}
