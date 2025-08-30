using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ItemUI : MonoBehaviour
{
    [Header("Log overflow control")]
    public bool limitLines = true;
    public int maxLines = 6;
    public bool autoExpire = false;
    public float entryLifetimeSeconds = 10f;

    [Header("Spam control")]
    public bool collapseDuplicates = true;
    public float minRepeatInterval = 1.0f; // sekunneissa

    private struct LogEntry { public string text; public float time; public int count; }
    private readonly List<LogEntry> logLines = new List<LogEntry>();

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
    private void Update()
    {
        if (!autoExpire || entryLifetimeSeconds <= 0f || logLines.Count == 0) return;

        bool changed = false;
        float now = Time.time;

        // Poista rivit, jotka ovat vanhempia kuin entryLifetimeSeconds
        for (int i = logLines.Count - 1; i >= 0; i--)
        {
            if (now - logLines[i].time > entryLifetimeSeconds)
            {
                logLines.RemoveAt(i);
                changed = true;
            }
        }

        if (changed && pickupText != null)
            pickupText.text = BuildLogString();
    }


    public void AddLogEntry(string itemName)
    {
        currentLog += "\n" + itemName;
        if (pickupText != null) pickupText.text = currentLog;
    }

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
    public void ShowPrompt(string text, Vector3 worldPos)
    {
        if (itemPrompt == null || itemText == null) return;
        itemText.text = text;
        itemPrompt.transform.position = worldPos + promptOffset;
        itemPrompt.SetActive(true);
    }
    public void AddLogLine(string text)
    {
        float now = Time.time;

        if (collapseDuplicates && logLines.Count > 0 && logLines[logLines.Count - 1].text == text)
        {
            var last = logLines[logLines.Count - 1];
            if (now - last.time < minRepeatInterval)
            {              
                last.count++;
                last.time = now;
                logLines[logLines.Count - 1] = last;
            }
            else
            {        
                logLines.Add(new LogEntry { text = text, time = now, count = 1 });
            }
        }
        else
        {
            logLines.Add(new LogEntry { text = text, time = now, count = 1 });
        }

        if (limitLines && maxLines > 0)
            while (logLines.Count > maxLines) logLines.RemoveAt(0);

        if (pickupText != null) pickupText.text = BuildLogString();
    }

    private string BuildLogString()
    {
        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < logLines.Count; i++)
        {
            if (i > 0) sb.Append('\n');
            sb.Append(logLines[i].text);
            if (logLines[i].count > 1) sb.Append(" ");
        }
        return sb.ToString();
    }


}
