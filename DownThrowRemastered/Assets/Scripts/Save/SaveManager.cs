using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    static UserSave currentUserSave = new UserSave();
    private const string SAVE_KEY = "userSaveInfo";

    public static void SetInfo(SaveInfo key, object value)
    {
        Debug.Log("Set " + key.ToString());
        currentUserSave.SetInfo(key, value);
        Save();
    }

    public static object GetInfo(SaveInfo key)
    {
        return currentUserSave.GetInfo(key);
    }

    public static void NewSave()
    {
        Debug.LogWarning("New Save");
        currentUserSave = new UserSave();
        Save();
    }

    static void Save()
    {
        Debug.LogWarning("Game Save");
        //Debug.Log(JsonUtility.ToJson(currentUserSave));
        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(currentUserSave));
    }

    public static void LoadSave()
    {
        Debug.LogWarning("Game Loaded");
        //Debug.Log(PlayerPrefs.GetString(SAVE_KEY));
        currentUserSave = JsonUtility.FromJson<UserSave>(PlayerPrefs.GetString(SAVE_KEY));
    }
}