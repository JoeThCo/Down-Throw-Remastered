using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SideBarUI : MonoBehaviour
{
    public static SideBarUI Instance;

    public void Init()
    {
        Instance = this;
    }
}