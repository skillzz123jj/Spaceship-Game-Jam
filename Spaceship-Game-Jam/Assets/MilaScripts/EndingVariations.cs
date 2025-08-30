using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingVariations : MonoBehaviour
{
    private Dictionary<string, string> endings = new Dictionary<string, string>()
    {
        { "Jerry Can", "Esa was unable to refuel the rocket and didn't get off the surface of moon."},
        { "Toilet paper ", "Esa had nothing to wipe with and improvised by opening the airlock."},
        { "GPS device", "Esa didn't know the way back to earth and was left out forever in space." },
        { "Rubberduck", "An asteroid came and made a hole into the rocket, esa had nothing to block it with." },
        { "Rocket manuals", "Like the ÍKEA manuals we all need, esa had no idea how to assemble the rocket without his manuals." },
        { "Coffee mug", "Without his trusted coffee mug, Esa fell asleep mid-flight and the rocket drifted endlessly through space." },
        { "Nokia 3310", "Esa forgot his indestructible Nokia 3310 — without it, he had no way to call Earth, and even worse, no Snake to pass the time." }

    };
    private List<string> pickedItems = new List<string>();
    [SerializeField] TMP_Text endingText;

    void Start()
    {
        pickedItems.Add("Jerry can");
        pickedItems.Add("Coffee mug");

        string randomItem = GetRandomUnusedItem();
        endingText.text = endings[randomItem];
    }

    string GetRandomUnusedItem()
    {
        List<string> availableItems = new List<string>();

        foreach (var key in endings.Keys)
        {
            if (!pickedItems.Contains(key))
            {
                availableItems.Add(key);
            }
        }

        if (availableItems.Count == 0)
        {
            Debug.LogWarning("No items left!");
            return null;
        }

        int randomIndex = Random.Range(0, availableItems.Count);
        return availableItems[randomIndex];
    }
}
