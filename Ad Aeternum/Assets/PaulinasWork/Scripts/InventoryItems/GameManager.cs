using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour
{
    static string dataPath;

    public GameObject PopUpBoxPrefab;
    public Canvas RootCanvas;

    public static List<Item> MasterCollection;

    void Awake()
    {
        #region Directory Setup
        dataPath = Application.persistentDataPath + "/";

        CreateRequiredDirectories();
        #endregion

        MasterCollection = new List<Item>();

        for( int i = 0; i < 100; i++)
        {
            MasterCollection.Add(new Item()
            {
                ID = i,
                Name = "Item " + i,
                Description = "Description Text " + i,
                Value = i * 10,
                IconName = "test_icon"
            });
        }
    }

    public static Item FindInventoryItemByID(int itemID)
    {
        return MasterCollection.Find(i => i.ID == itemID);
    }

    public static T LoadAssetFromResources<T>(string assetPath) where T : UnityEngine.Object
    {
        return Resources.Load<T>(assetPath);
    }

    public void ShowPopUp(string message)
    {
        GameObject popup = Instantiate(PopUpBoxPrefab, RootCanvas.transform);
        popup.GetComponent<PopUpBoxControl>().InitializePopUp(message);
    }

    private void CreateRequiredDirectories()
    {
        if(!Directory.Exists(dataPath + "Saves/"))
        {
            Directory.CreateDirectory(dataPath + "Saves/");
        }
    }
}
