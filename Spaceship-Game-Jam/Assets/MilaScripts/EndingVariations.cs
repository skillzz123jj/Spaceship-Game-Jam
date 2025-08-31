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
        { "Ducky", "An asteroid came and made a hole into the rocket, esa had nothing to block it with." },
        { "Rocket manuals", "Like the ÍKEA manuals we all need, esa had no idea how to assemble the rocket without his manuals." },
        { "Coffee mug", "Without his trusted coffee mug, Esa fell asleep mid-flight and the rocket drifted endlessly through space." },
        { "Nokia 3310", "Esa forgot his indestructible Nokia 3310 — without it, he had no way to call Earth, and even worse, no Snake to pass the time." },
        { "Antenna", "Without the antenna, Esa’s distress call never got out. He died waving at empty space." },
        { "Disco ball", "No disco ball meant no party. Without it, morale flatlined — just like Esa." },
        { "Ducktape", "A crack split the hull. Without duct tape, Esa learned how quickly lungs collapse in space." },
        { "FloppyDisk", "The launch codes were on a floppy. Esa brought a USB stick instead." },
        { "Hammer", "One bolt rattled loose. Without a hammer, the ship unraveled piece by piece… with him inside." },
        { "HDD", "The star maps were on his hard drive. Without it, Esa took the longest scenic route into nothingness." },
        { "JoyStick", "Esa forgot his joystick — the rocket spun wildly, and he became the first human to orbit endlessly without a single pause." },
        { "Oxytank", "Forgot O2 tank. Tried holding breath. High score: 27 seconds." },
        { "SatDish", "No satellite dish = no signal. Esa died with 0 bars and unlimited roaming."},
        { "Toilet Seat", "The toilet seat was missing. Esa fell into the void mid-relief, leaving only a cosmic stain behind." },
        { "Toolcase", "No tools meant no fixing the engine. Esa gave it a good kick instead — and the rocket kicked back." }
        
    };
   
    [SerializeField] TMP_Text endingText;

    void Start()
    {

        string randomItem = GetRandomUnusedItem();
        endingText.text = endings[randomItem];
    }

    string GetRandomUnusedItem()
    {
        List<string> availableItems = new List<string>();

        foreach (var key in endings.Keys)
        {
            if (!GameData.items.Exists(item => item.itemName == key))
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
