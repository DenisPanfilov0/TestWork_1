using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCollection", menuName = "ScriptableObjects/ItemCollection")]
public class ItemCollection : ScriptableObject
{
    public List<Item> itemCollection;
}