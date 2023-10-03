using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    static UserSave currentUserSave;
    private const string SAVE_KEY = "userSaveInfo";

    public static bool isSaveLoaded = false;

    public static void SetInfo(SaveInfo key, object value)
    {
        Debug.LogWarning("Set " + key.ToString() + " to " + value);
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

        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(currentUserSave));
        DebugJson();
    }

    public static void LoadSave()
    {
        if (isSaveLoaded)
        {
            Debug.LogWarning("Game already loaded!");
            return;
        }

        Debug.LogWarning("Game Loaded");

        currentUserSave = JsonUtility.FromJson<UserSave>(PlayerPrefs.GetString(SAVE_KEY));
        DebugJson();

        isSaveLoaded = true;
    }

    static void DebugJson()
    {
        Debug.LogWarning(PlayerPrefs.GetString(SAVE_KEY));
    }
}