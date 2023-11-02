using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SideBarUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ballsLeft;
    public static SideBarUI Instance;

    public void Init()
    {
        Instance = this;
    }
    public void SetBallsLeft()
    {
        ballsLeft.SetText("Balls Left: " + GameManager.player.Health.ToString());
    }
}