using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class StorageSystem
{
    public static void SavePlayerRecord(PlayerRecord playerRecord)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/PlayerRecord.data";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, playerRecord);
        fileStream.Close();
    }

    public static PlayerRecord LoadPlayerRecord()
    {
        string path = Application.dataPath + "/PlayerRecord.data";
        if (!File.Exists(path)) return null;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Open);

        PlayerRecord playerRecord = formatter.Deserialize(fileStream) as PlayerRecord;
        fileStream.Close();

        return playerRecord;
    }
}
