using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public static class PlayFabPlayerInfo
{
    public static PlayerAccountInfo playerAccountInfo { get; private set; }
    public static PlayerShopInfo playerShopInfo { get; private set; }

    private const string PLAYER_INFO_KEY = "PLAYER_INFO";
    public static bool isLoggedIn = false;

    public static void SavePlayerInfo()
    {
        if (!isLoggedIn) return;
        //Debug.Log(playerInfo.ToJson());

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {PLAYER_INFO_KEY, playerAccountInfo.ToJson() }
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

            playerAccountInfo = JsonUtility.FromJson<PlayerAccountInfo>(jsonData);
            SettingsManager.SetPlayerSettings(playerAccountInfo.playerSettings);

            Debug.Log(jsonData);

            MenuManager.Instance.LoadAScene("MainMenu");
            isLoggedIn = true;
        }
    }

    public static void NewPlayer()
    {
        Debug.Log("No Player Info, creating...");

        playerAccountInfo = new PlayerAccountInfo();
        playerShopInfo = new PlayerShopInfo();

        SavePlayerInfo();
    }

    public static void SetName(string name)
    {
        playerAccountInfo.name = name;
        SavePlayerInfo();
    }

    public static void SetHighScore(int highScore)
    {
        playerAccountInfo.highScore = highScore;
        SavePlayerInfo();
    }

    public static void ChangeGold(int change)
    {
        playerAccountInfo.gold -= change;
        SavePlayerInfo();
    }

    public static void OfflinePlay()
    {
        if (playerAccountInfo != null) return;

        Debug.LogWarning("You are not connected to Playfab!");
        playerAccountInfo = new PlayerAccountInfo();
        SettingsManager.SetPlayerSettings(null);
    }
}