using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public string itemType;
    public int itemCost;
    public Item(string _itemType, string _itemName, int _itemCost)
    {
        itemCost = _itemCost;
        itemName = _itemName;
        itemType = _itemType;
    }
}
