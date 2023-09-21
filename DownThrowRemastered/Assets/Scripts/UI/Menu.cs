using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string id;

    public void ShowMenu(bool isVisable)
    {
        gameObject.SetActive(isVisable);
    }

    public bool isVisable()
    {
        return gameObject.activeSelf;
    }
}