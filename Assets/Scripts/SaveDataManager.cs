using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveDataManager
{
    [Serializable]

    class PlayerProfilesData
    {
        public List<string> playerNames;
        public PlayerProfilesData(List<string> names)
        {
            playerNames = names;
        }
    }
    static string GetSaveDir()
    {
        return Application.persistentDataPath;
    }

    static string GetPlayerProfileName()
    {
        return "players.json";
    }

    static string GetPlayerProfileSaveDir()
    {
        return GetSaveDir() + "/" + GetPlayerProfileName();
    }

    public static void SavePlayerProfile(string playerName)
    {
        PlayerProfilesData data = new PlayerProfilesData(new List<string> { playerName });
        string dataJSON = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPlayerProfileSaveDir(), dataJSON);
    }
}
