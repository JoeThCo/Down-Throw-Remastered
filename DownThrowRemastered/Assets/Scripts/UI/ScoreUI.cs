using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PrimeTween;
using UnityEngine.SceneManagement;
using System;

public class ScoreUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI currentScoreText;

    [SerializeField] float scoreShake = 1.5f;
    [SerializeField] float scoreTime = .35f;

    public static ScoreUI Instance;

    public void Init()
    {
        Instance = this;

        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        EventManager.OnScoreChange += EventManager_OnScoreChange;

        SetCurrentScoreText(0);
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnScoreChange -= EventManager_OnScoreChange;
    }

    private void EventManager_OnScoreChange(int change)
    {
        scoreJuice();
    }

    public void SetCurrentScoreText(int score)
    {
        currentScoreText.SetText("Score: " + score.ToString());
    }

    void scoreJuice()
    {
        Tween.ShakeScale(transform, Vector3.one * scoreShake, scoreTime);
    }
}