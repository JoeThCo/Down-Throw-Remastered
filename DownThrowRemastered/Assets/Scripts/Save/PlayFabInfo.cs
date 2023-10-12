using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public static class PlayFabInfo
{
    static PlayerInfo playerInfo;
    private const string PLAYER_INFO_KEY = "PLAYER_INFO";

    public static bool isLoggedIn = false;

    public static void SavePlayerInfo()
    {
        if (!isLoggedIn) return;
        Debug.Log(playerInfo.ToJson());

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {PLAYER_INFO_KEY, playerInfo.ToJson() }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnPlayerInfoSuccess, OnPlayFabError);
    }

    private static void OnPlayerInfoSuccess(UpdateUserDataResult result)
    {
        Debug.LogWarning("Successfully updated player info");
    }

    private static void OnPlayFabError(PlayFabError error)
    {
        MessageUI.Instance.OnError(error.Error.ToString());
    }

    public static void LoadPlayerInfo()
    {
        var request = new GetUserDataRequest
        {
            Keys = new List<string> { PLAYER_INFO_KEY }
        };

        PlayFabClientAPI.GetUserData(request, OnDataLoadSuccess, OnPlayFabError);
    }

    static void OnDataLoadSuccess(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey(PLAYER_INFO_KEY))
        {
            Debug.Log("Successfully loaded player data");
            string jsonData = result.Data[PLAYER_INFO_KEY].Value;

            playerInfo = JsonUtility.FromJson<PlayerInfo>(jsonData);
            SettingsManager.SetPlayerSettings(playerInfo.playerSettings);

            Debug.Log(jsonData);

            MenuManager.Instance.LoadAScene("MainMenu");
            isLoggedIn = true;
        }
    }

    public static void NewPlayer()
    {
        Debug.Log("No Player Info, creating...");
        playerInfo = new PlayerInfo();
        SavePlayerInfo();
    }

    public static void SetName(string name)
    {
        playerInfo.name = name;
        SavePlayerInfo();
    }

    public static string GetName()
    {
        return playerInfo.name;
    }

    public static void SetHighScore(int highScore)
    {
        playerInfo.highScore = highScore;
        SavePlayerInfo();
    }

    public static int GetHighScore()
    {
        return playerInfo.highScore;
    }

    public static void OfflinePlay()
    {
        if (playerInfo != null) return;

        Debug.LogWarning("You are not connected to Playfab!");
        playerInfo = new PlayerInfo();
    }
}

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public int highScore;

    public PlayerSettings playerSettings;

    public PlayerInfo()
    {
        name = "Offline";
        highScore = 0;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}