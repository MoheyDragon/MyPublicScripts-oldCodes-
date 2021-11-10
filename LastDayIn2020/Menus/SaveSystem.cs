using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{

    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Save.sav";
        FileStream stream = new FileStream(path, FileMode.Create);
        DataSave data = new DataSave();
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static DataSave Load()
    {
        string path = Application.persistentDataPath + "/Save.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DataSave data = formatter.Deserialize(stream) as DataSave;
            stream.Close();
            return data;
        }
        else
            return null;
    }
}
