using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    private ItemUI itemUI;
    private bool playerInRange = false;

    private void Start()
    {
#if UNITY_2023_1_OR_NEWER
        itemUI = FindFirstObjectByType<ItemUI>();
#else
        itemUI = FindObjectOfType<ItemUI>();
#endif
        if (itemUI == null)
            Debug.LogWarning("ItemPickUp: ItemUI not found in scene.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (itemUI != null && item != null)
                itemUI.ShowItemText(item.itemName, transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (itemUI != null)
                itemUI.HideItemText();
        }
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (item != null) item.isPickedUp = true;
            if (itemUI != null && item != null)
            {
                itemUI.AddLogEntry(item.itemName);
                itemUI.HideItemText();
            }

            Destroy(gameObject);
        }
    }
}
