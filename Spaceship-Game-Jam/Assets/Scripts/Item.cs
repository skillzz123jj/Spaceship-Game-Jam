using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string description;
    public float weight;
    public bool isPickedUp = false;

}
