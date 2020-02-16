using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public PlayerData data;

    private string file = "player_data.txt";

    public void OnDeathSave()
    {
        string json = JsonUtility.ToJson(data);
        WriteToJSON(file, json);
    }

    public void OnSpawnLoad()
    {
        data = new PlayerData();
        string json = ReadFromJSON(file);
        JsonUtility.FromJsonOverwrite(json, data);
    }

    private void WriteToJSON(string fileName, string json)
    {
        string path = GetPath(fileName);

        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromJSON(string fileName)
    {
        string path = GetPath(fileName);

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();

                return json;
            }
        }
        else
        {
            return "";
        }
    }

    private string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
