using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingVariations : MonoBehaviour
{
    private Dictionary<string, string> endings = new Dictionary<string, string>()
    {
        { "Antenna", "Without the antenna, Esa’s distress call never got out. He died waving at empty space." },
        { "Coffeemug", "Without his trusted coffee mug, Esa fell asleep mid-flight and the rocket drifted endlessly through space." },
        { "DiscoBall", "No disco ball meant no party. Without it, morale flatlined — just like Esa." },
        { "Ducktape", "A crack split the hull. Without duct tape, Esa learned how quickly lungs collapse in space." },
        { "Ducky", "An asteroid came and made a hole into the rocket, esa had nothing to block it with." },
        { "Floppydisk", "The launch codes were on a floppy. Esa brought a USB stick instead." },
        { "GPS", "Esa didn't know the way back to earth and was left out forever in space." },
        { "Hammer", "One bolt rattled loose. Without a hammer, the ship unraveled piece by piece… with him inside." },
        { "HDD", "The star maps were on his hard drive. Without it, Esa took the longest scenic route into nothingness." },
        { "Jerrycan", "Esa was unable to refuel the rocket and didn't get off the surface of moon."},
        { "Joystick", "Esa forgot his joystick — the rocket spun wildly, and he became the first human to orbit endlessly without a single pause." },
        { "Manual", "Like the ÍKEA manuals we all need, esa had no idea how to assemble the rocket without his manuals." },
        { "Oxytank", "Forgot O2 tank. Tried holding breath. High score: 27 seconds." },
        { "Nokia 3310", "Esa forgot his indestructible Nokia 3310 — without it, he had no way to call Earth, and even worse, no Snake to pass the time." },
        { "Satellite Disk", "No satellite dish = no signal. Esa died with 0 bars and unlimited roaming."},
        { "Toiletseat", "The toilet seat was missing. Esa fell into the void mid-relief, leaving only a cosmic stain behind." },
        { "ToiletPaperRoll", "Esa had nothing to wipe with and improvised by opening the airlock."},
        { "Toolcase", "No tools meant no fixing the engine. Esa gave it a good kick instead — and the rocket kicked back." }
        
    };
   
    [SerializeField] TMP_Text endingText;

    void Start()
    {

        string randomItem = GetRandomUnusedItem();
        if (randomItem != null)
        {
            endingText.text = endings[randomItem];
        }
        else
        {
            endingText.text = "Congratulations! Esa made it back home!";
        }
    }

    string GetRandomUnusedItem()
    {
        List<string> availableItems = new List<string>();
        string ending;
        foreach (var key in endings.Keys)
        {
            if (!GameData.items.Exists(item => item.itemName == key))
            {
                availableItems.Add(key);
            }
        }

        if (availableItems.Count == 0)
        {
            Debug.Log("All items found - Player Won!");
            return null;
        }
        else
        {
            int randomIndex = Random.Range(0, availableItems.Count);
            ending = availableItems[randomIndex];
        }
        return ending;
    }

}
