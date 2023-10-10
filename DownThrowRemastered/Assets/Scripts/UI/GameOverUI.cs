using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI newHighscoreText;
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI scoreText;

    public static GameOverUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetScoreText(int score)
    {
        scoreText.SetText("Score: " + score);
    }

    public void SetHighscoreText(bool isHighScore, int score, int highscore)
    {
        newHighscoreText.gameObject.SetActive(isHighScore);

        if (isHighScore)
        {
            highscoreText.SetText("Highscore: " + score.ToString());
        }
        else
        {
            highscoreText.SetText("Highscore: " + highscore.ToString());
        }
    }
}