using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI newHighscoreText;
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI scoreText;

    public static GameOverUI Instance;

    public void Init()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        SetGameOverUI(GameManager.currentScore, GameManager.highScore);
    }

    void SetGameOverUI(int score, int highscore)
    {
        newHighscoreText.gameObject.SetActive(score > highscore);

        if (score > highscore)
        {
            highscoreText.SetText("Highscore: " + score.ToString());
        }
        else
        {
            highscoreText.SetText("Highscore: " + highscore.ToString());
        }

        scoreText.SetText("Score: " + score);
    }
}