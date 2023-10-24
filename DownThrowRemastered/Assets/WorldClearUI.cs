using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldClearUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public static WorldClearUI Instance;

    public void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        SetScoreText();
    }
    public void SetScoreText()
    {
        scoreText.SetText("Score: " + GameManager.currentScore);
    }
}