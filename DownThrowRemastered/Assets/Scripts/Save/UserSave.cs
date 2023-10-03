using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSave
{
    [SerializeField] int highScore = 0;

    public UserSave()
    {
        SetInfo(SaveInfo.HighScore, 0);
    }

    public void SetInfo(SaveInfo key, object value)
    {
        switch (key)
        {
            case SaveInfo.HighScore:
                highScore = (int)value;
                break;

            default:
                Debug.LogError("Invalid Key!");
                break;
        }
    }

    public object GetInfo(SaveInfo key)
    {
        switch (key)
        {
            case SaveInfo.HighScore:
                return highScore;

            default:
                Debug.LogError("Invalid Key!");
                return null;
        }
    }
}
