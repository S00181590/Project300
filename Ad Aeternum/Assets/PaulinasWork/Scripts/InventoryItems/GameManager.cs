using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public delegate void EmptyDelegate();

public class GameManager : MonoBehaviour
{
    static string dataPath;
    public static UserProfile Profile;
    public static event EmptyDelegate ProfileLoaded;

    public GameObject PopUpBoxPrefab;
    public Canvas RootCanvas;

    public static  List<Item> MasterCollection;

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

    public static string[] GetAllProfileNames()
    {
        string[] names = Directory.GetFiles(dataPath + "Saves/", "*.json");

        for(int i =0; i < names.Length;i++)
        {
            names[i] = Path.GetFileNameWithoutExtension(names[i]);
        }

        return names;
    }

    public static void DeleteUserProfile(string username)
    {
        string path = dataPath + "Saves/" + username + ".json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static void CreateUserProfile(string username,Color color, string imageName)
    {
        UserProfile profile = new UserProfile()
        {
            Username = username,
            Color = color,
            ImageName = imageName
        };

        SaveUserProfile(profile);
    }

    private void CreateRequiredDirectories()
    {
        if(!Directory.Exists(dataPath + "Saves/"))
        {
            Directory.CreateDirectory(dataPath + "Saves/");
        }
    }

    public static void SaveUserProfile(UserProfile profile)
    {
        SaveToJSON(dataPath + "Saves/" + profile.Username + ".json", profile);
    }

    public static void LoadAndSetUserProfile(string username)
    {
        Profile = LoadUserProfile(username);

        if (ProfileLoaded != null && Profile != null)
            ProfileLoaded();
    }

    public static UserProfile LoadUserProfile(string username)
    {
        return LoadFromJSON<UserProfile>(dataPath + "Saves/" + username + ".json");
    }

    public static void SaveToJSON(string path, object objectTosave)
    {
        string json = JsonUtility.ToJson(objectTosave);
        File.WriteAllText(path, json);
    }

    public static T LoadFromJSON<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }
}
