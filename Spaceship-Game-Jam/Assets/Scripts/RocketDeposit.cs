using System.Collections.Generic;
using UnityEngine;

public class RocketDeposit : MonoBehaviour
{
    [Header("UI spam control")]
    public float emptyDepositCooldown = 1.5f;  // sek

    public List<Item> rocketItems = new List<Item>();

    private ItemUI itemUI;
    private PlayerInventory playerInv;
    private bool playerInRange = false;

    private void Start()
    {
        itemUI = FindFirstObjectByType<ItemUI>();

        if (itemUI == null)
            Debug.LogWarning("RocketDeposit: ItemUI not found in scene.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Entered");

        playerInRange = true;
        playerInv = other.GetComponent<PlayerInventory>();
        if (playerInv == null) playerInv = other.GetComponentInParent<PlayerInventory>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        playerInv = null;

        if (itemUI != null)
            itemUI.HideItemText(); //
    }

    private void Update()
    {
        if (!playerInRange || playerInv == null) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            int count = playerInv.items.Count;
            if (count == 0)
            {
                itemUI?.AddLogLine("Nothing to deposit.");
                itemUI?.HideItemText();
                return;
            }

            var names = new List<string>();
            
            foreach (var it in playerInv.items)
            {
                if (it == null) continue;
                if (!rocketItems.Contains(it))
                    rocketItems.Add(it);
                names.Add(it.itemName);
            }
            playerInv.items.Clear();

            itemUI?.AddLogLine("Deposited: " + string.Join(", ", names));
            itemUI?.HideItemText();
        }
    }
}
