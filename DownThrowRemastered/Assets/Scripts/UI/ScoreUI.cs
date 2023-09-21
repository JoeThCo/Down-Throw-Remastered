using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;

    public static ScoreUI Instance;

    private void Awake()
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