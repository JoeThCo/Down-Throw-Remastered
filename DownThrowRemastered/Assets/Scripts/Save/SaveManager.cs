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
        currentUserSave.SetInfo(key, value);
        Save();
        Debug.LogWarning("Set " + key.ToString() + " to " + value);
    }

    public static object GetInfo(SaveInfo key)
    {
        return currentUserSave.GetInfo(key);
    }

    public static void Init() 
    {
    
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
        Debug.LogWarning(JsonUtility.ToJson(currentUserSave));
        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(currentUserSave));
        PlayerPrefs.Save();
    }

    public static void LoadSave()
    {
        if (isSaveLoaded)
        {
            Debug.LogWarning("Game already loaded!");
            return;
        }

        Debug.LogWarning("Game Loaded");
        Debug.LogWarning(JsonUtility.ToJson(currentUserSave));
        currentUserSave = JsonUtility.FromJson<UserSave>(PlayerPrefs.GetString(SAVE_KEY));

        isSaveLoaded = true;
    }
}