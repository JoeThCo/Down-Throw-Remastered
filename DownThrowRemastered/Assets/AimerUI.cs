using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ballsLeftText;
    public static AimerUI Instance;

    public void Init() 
    {
        Instance = this;
    }

    public void SetBallsLeftText(Player player)
    {
        ballsLeftText.SetText(player.GetHealth().ToString());
    }
}
