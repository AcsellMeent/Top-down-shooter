using UnityEngine;
using YG;

public static class StorageSystem
{
    public static void SavePlayerRecord(PlayerRecord playerRecord)
    {
        YandexGame.savesData += playerRecord;
        YandexGame.SaveProgress();
    }

    public static PlayerRecord LoadPlayerRecord()
    {
        return new PlayerRecord(YandexGame.savesData.points);
    }
}