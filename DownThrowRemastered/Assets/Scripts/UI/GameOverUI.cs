using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI scoreText;

    public static GameOverUI Instance;

    public void Init()
    {
        Instance = this;
    }

    public void SetGameOverUI(int score)
    {
        scoreText.SetText("Score: " + score);
    }
}