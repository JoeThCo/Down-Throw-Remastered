using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string name;
    public int highScore;

    public int gems;

    public PlayerSettings playerSettings;

    public PlayerInfo()
    {
        name = "Offline";

        highScore = 0;
        gems = 0;

        playerSettings = new PlayerSettings();
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}