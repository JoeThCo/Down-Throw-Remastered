using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] BackgroundManager backgroundManager;
    [Space(10)]
    [SerializeField] WorldMap worldMap;
    [SerializeField] ScoreUI scoreUI;

    public static bool isDownThrowing;

    public static InGamePlayer player { get; set; }
    public static CurrentMonsters currentMonsters { get; set; }

    public static float CurrentDifficulty = 1;
    private const float WORLD_COMPLETE_INCREMENT = .33f;
    public static int currentScore { get; set; }
    public static int highScore { get; set; }
    const int SCORE_MULTIPLIER = 10;
    public const int MONSTER_DEFEAT_MULTIPLIER = 3;
    public const int START_PLAYER_BALLS = 5;
    public const int MAX_MONSTERS = 1;

    public static Camera Cam;

    private void Start()
    {
        Cam = Camera.main;

        PlayFabPlayerInfo.OfflinePlay();
        Application.targetFrameRate = -1;

        StaticSpawner.Load();

        GameStart();

        isDownThrowing = false;
    }

    void GameStart()
    {
        QualitySettings.vSyncCount = 1;

        worldMap.Init();
        scoreUI.Init();

        player = new InGamePlayer(START_PLAYER_BALLS);

        CurrentDifficulty = 1;

        currentScore = 0;
        highScore = PlayFabPlayerInfo.GetHighScore();

        scoreUI.SetHighScoreText(highScore);
        scoreUI.SetCurrentScoreText(currentScore);

        backgroundManager.SetRandomBackground();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!isDownThrowing) return;

        if (!focus)
        {
            MenuManager.Instance.DisplayMenus("Pause");
        }
    }

    private void OnEnable()
    {
        EventManager.OnNodeEnter += EventManager_OnNodeEnter;
        EventManager.OnNodeClear += EventManager_OnNodeClear;

        EventManager.OnWorldClear += EventManager_OnWorldClear;

        EventManager.OnGameOver += EventManager_OnGameOver;

        EventManager.OnScoreChange += EventManager_OnScoreChange;
        EventManager.OnHighScoreChange += EventManager_OnHighScoreChange;
    }

    private void OnDisable()
    {
        EventManager.OnNodeEnter -= EventManager_OnNodeEnter;
        EventManager.OnNodeClear -= EventManager_OnNodeClear;

        EventManager.OnWorldClear -= EventManager_OnWorldClear;

        EventManager.OnGameOver -= EventManager_OnGameOver;

        EventManager.OnScoreChange -= EventManager_OnScoreChange;
        EventManager.OnHighScoreChange -= EventManager_OnHighScoreChange;
    }

    public void SetIsPlaying(bool state)
    {
        isDownThrowing = state;
    }

    #region Score
    private void EventManager_OnHighScoreChange()
    {
        Debug.Log("New highscore from " + highScore + " -> " + currentScore);
        PlayFabPlayerInfo.SetHighScore(currentScore);
    }

    private void EventManager_OnScoreChange(int change)
    {
        currentScore += change * SCORE_MULTIPLIER;
        scoreUI.SetCurrentScoreText(currentScore);
    }

    bool isNewHighScore()
    {
        return currentScore > highScore;
    }

    void CheckNewHighScore()
    {
        if (!isNewHighScore()) return;
        EventManager.Invoke(CustomEvent.HighScoreChange);
    }
    #endregion

    private void EventManager_OnNodeEnter(int monsterCount)
    {
        currentMonsters = new CurrentMonsters(monsterCount);
        isDownThrowing = true;
    }

    private void EventManager_OnWorldClear()
    {
        CurrentDifficulty += WORLD_COMPLETE_INCREMENT;

        StaticSpawner.PlaySFX("areaClear");
        PlayFabPlayerInfo.SavePlayerInfo();
    }

    private void EventManager_OnGameOver()
    {
        CheckNewHighScore();
        isDownThrowing = false;

        StaticSpawner.PlaySFX("gameOver");
        PlayFabPlayerInfo.SavePlayerInfo();
    }

    private void EventManager_OnNodeClear()
    {
        StaticSpawner.PlaySFX("areaClear");
        isDownThrowing = false;
    }
}