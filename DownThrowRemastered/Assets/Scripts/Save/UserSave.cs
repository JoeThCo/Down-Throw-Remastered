using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSave
{
    [SerializeField] string name = "Joe";
    [SerializeField] int highScore = 0;

    public UserSave()
    {
        SetInfo(SaveInfo.HighScore, 15);
        SetInfo(SaveInfo.Name, "Joe");
    }

    public void SetInfo(SaveInfo key, object value)
    {
        switch (key)
        {
            case SaveInfo.HighScore:
                highScore = (int)value;
                break;

            case SaveInfo.Name:
                name = (string)value;
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

            case SaveInfo.Name:
                return name;

            default:
                Debug.LogError("Invalid Key!");
                return null;
        }
    }
}
