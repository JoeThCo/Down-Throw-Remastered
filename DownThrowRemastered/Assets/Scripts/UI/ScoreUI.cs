using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;

    public static ScoreUI Instance;

    public void Init()
    {
        Instance = this;
    }

    public void SetHighScoreText(int highScore)
    {
        highScoreText.SetText("High: " + highScore);
    }

    public void SetCurrentScoreText(int score)
    {
        currentScoreText.SetText("Score: " + score.ToString());
    }
}