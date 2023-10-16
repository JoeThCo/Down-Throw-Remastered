using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAccountInfo
{
    public string name;
    public int highScore;

    public int gems;

    public PlayerSettings playerSettings;

    public PlayerAccountInfo()
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