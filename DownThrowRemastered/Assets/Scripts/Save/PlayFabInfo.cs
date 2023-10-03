using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public static class PlayFabInfo
{
    static PlayerInfo playerInfo;

    private const string PLAYER_INFO_KEY = "PLAYER_INFO";

    public static void SavePlayerInfo()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {PLAYER_INFO_KEY, playerInfo.ToJson() }
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnScoreUpdateSuccess, OnScoreUpdateFailure);
    }

    private static void OnScoreUpdateSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated player score");
    }

    private static void OnScoreUpdateFailure(PlayFabError error)
    {
        Debug.LogError("Error updating player score: " + error.GenerateErrorReport());
    }

    public static void LoadPlayerInfo()
    {
        var request = new GetUserDataRequest
        {
            Keys = new List<string> { PLAYER_INFO_KEY }
        };

        PlayFabClientAPI.GetUserData(request, OnDataLoadSuccess, OnDataLoadFailure);
    }

    static void OnDataLoadSuccess(GetUserDataResult result)
    {
        if (result.Data == null || !result.Data.ContainsKey(PLAYER_INFO_KEY))
        {
            Debug.Log("No Player Info, creating...");
            playerInfo = new PlayerInfo();
            SavePlayerInfo();
        }
        else
        {
            Debug.Log("Successfully loaded player data");
            string jsonData = result.Data[PLAYER_INFO_KEY].Value;
            playerInfo = JsonUtility.FromJson<PlayerInfo>(jsonData);
            playerInfo.DebugInfo();
        }
    }

    static void OnDataLoadFailure(PlayFabError error)
    {
        Debug.LogError("Error loading player data: " + error.GenerateErrorReport());
    }

    public static void SetHighScore(int highScore)
    {
        playerInfo.highScore = highScore;
    }

    public static int GetHighScore()
    {
        return playerInfo.highScore;
    }
}

public class PlayerInfo
{
    public int highScore;

    public PlayerInfo()
    {
        highScore = 0;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void DebugInfo()
    {
        Debug.Log("Highscore: " + highScore);
    }
}