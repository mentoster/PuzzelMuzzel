using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveData(LoadManager loadManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string dataPath = Application.dataPath + "/save.mlya";
        FileStream stream = new FileStream(dataPath, FileMode.Create);
        GameData data = new GameData(loadManager);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static GameData LoadSave()
    {
        string dataPath = Application.dataPath + "/save.mlya";
        if (File.Exists(dataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(dataPath, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + dataPath);
            return null;
        }

    }
}