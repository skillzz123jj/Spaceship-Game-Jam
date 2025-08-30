using UnityEngine;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [Header("Log (bottom-left)")]
    public TextMeshProUGUI pickupText;
    private string currentLog = "";

    [Header("Prompt (world space above item)")]
    public GameObject itemPrompt;         // World Space Canvas (panel + TMP text)
    public TextMeshProUGUI itemText;      // TMP text inside the prompt
    public Vector3 promptOffset = new Vector3(0f, 1.2f, 0f);

    private void Start()
    {
        if (pickupText != null) pickupText.text = "";
        if (itemPrompt != null) itemPrompt.SetActive(false);
    }

    public void AddLogEntry(string itemName)
    {
        currentLog += "\nYou picked up " + itemName;
        if (pickupText != null) pickupText.text = currentLog;
    }

    // Show prompt above given world position
    public void ShowItemText(string itemName, Vector3 worldPos)
    {
        if (itemPrompt != null && itemText != null)
        {
            itemText.text = itemName + "\nPress F to pick up";
            itemPrompt.transform.position = worldPos + promptOffset;
            itemPrompt.SetActive(true);
        }
    }

    public void HideItemText()
    {
        if (itemPrompt != null) itemPrompt.SetActive(false);
    }
}
