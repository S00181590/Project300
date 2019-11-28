using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SavableInventory : MonoBehaviour
{
    public List<int> Items = new List<int>();

    public void AddItem(int itemID)
    {
        Items.Add(itemID);
    }

    public void AddItem(string name)
    {
        Item foundItem = 
            GameManager.Instance.AllItemsInTheGame.
            GetItem(name);

        if (foundItem != null)
            AddItem(foundItem);
    }

    public void AddItem(Item item)
    {
        AddItem(item.ID);
    }

    public void Save(string filename)
    {
        string json = JsonUtility.ToJson(this, true);

        File.WriteAllText(Application.persistentDataPath + "/" + filename + ".json", json);
    }

    public void Load(string filename)
    {
        if(File.Exists(Application.persistentDataPath + "/" + filename + ".json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/" + filename + ".json");
            Items = JsonUtility.FromJson<SavableInventory>(json).Items;
        }
    }
}

