using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName ="Inventory", menuName ="Items/List")]
public class Inventory : ScriptableObject
{
    public List<Item> Items = new List<Item>();

    public Item GetItem(int itemID)
    {
        return Items.Find(item => item.ID == itemID);
    }

    public Item GetItem (string itemName)
    {
        return Items.Find(item => item.Name == itemName);
    }
}
