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
        GetSavedPlayerProfile(out List<string> players);
        if (players.Contains(playerName))
        {
            return;
        }
        players.Insert(0,playerName);
        PlayerProfilesData data = new PlayerProfilesData(players);
        string dataJSON = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPlayerProfileSaveDir(), dataJSON);
    }

    public static bool GetSavedPlayerProfile(out List<string> data)
    {
        if (File.Exists(GetPlayerProfileSaveDir()))
        {
            string dataJSON = File.ReadAllText(GetPlayerProfileSaveDir());
            PlayerProfilesData loadedData = JsonUtility.FromJson<PlayerProfilesData>(dataJSON);
            data = loadedData.playerNames;
            return true;
        }
        data = new List<string>();
        return false;
        
    }
}
