using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaClearUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public static AreaClearUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetScoreText(int score)
    {
        scoreText.SetText("Score: " + score);
    }
}
