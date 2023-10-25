using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] BackgroundManager backgroundManager;
    [Space(10)]
    [SerializeField] WorldMap worldMap;
    [Space(10)]
    [SerializeField] CurrencyUI currencyUI;
    [SerializeField] CurrentMonsterUI currentMonsterUI;
    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] NextMonsterUI nextMonsterUI;
    [SerializeField] AimUI aimerUI;
    [SerializeField] ScoreUI scoreUI;

    public static bool isDownThrowing;

    InGamePlayer player;
    CurrentMonsters currentMonsters;

    public static int WorldsCleared = 1;
    private const float WORLD_COMPLETE_INCREMENT = .33f;

    private int currentScore;
    private int highScore;

    const int SCORE_MULTIPLIER = 10;
    public const int MONSTER_DEFEAT_MULTIPLIER = 3;

    public const int START_PLAYER_BALLS = 10;
    public const int MAX_MONSTERS = 6;

    public static Camera Cam;

    public static GameManager Instance;

    private void Start()
    {
        Instance = this;
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

        currencyUI.Init();
        currentMonsterUI.Init();
        gameOverUI.Init();
        backgroundManager.Init();
        nextMonsterUI.Init();
        aimerUI.Init();

        player = new InGamePlayer(START_PLAYER_BALLS);
        AimUI.Instance.SetBallsLeftText(player);

        WorldsCleared = 1;

        currentScore = 0;
        highScore = PlayFabPlayerInfo.GetHighScore();

        scoreUI.SetHighScoreText(highScore);
        scoreUI.SetCurrentScoreText(currentScore);

        backgroundManager.SetRandomBackground();
    }

    public void LoadNode(int monsterCount)
    {
        MenuManager.Instance.DisplayMenu("Game");
        currentMonsters = new CurrentMonsters(monsterCount);
        isDownThrowing = true;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!isDownThrowing) return;

        if (!focus)
        {
            MenuManager.Instance.DisplayMenu("Pause");
        }
    }

    private void OnEnable()
    {
        EventManager.OnNodeClear += EventManager_OnNodeClear;
        EventManager.OnWorldClear += EventManager_OnWorldClear;

        EventManager.OnGameOver += EventManager_OnGameOver;

        EventManager.OnScoreChange += EventManager_OnScoreChange;
        EventManager.OnHighScoreChange += EventManager_OnHighScoreChange;
    }

    private void OnDisable()
    {
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

    public static float GetMonsterDifficulty()
    {
        return 1 + ((float)(WorldsCleared - 1) * WORLD_COMPLETE_INCREMENT);
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

    private void EventManager_OnWorldClear()
    {
        EventManager.Invoke(CustomEvent.ScoreChange, 25);
        WorldsCleared++;

        MenuManager.Instance.DisplayMenu("WorldClear");
        WorldClearUI.Instance.SetScoreText(currentScore);

        StaticSpawner.PlaySFX("areaClear");
        backgroundManager.SetRandomBackground();

        WorldMap.Instance.MakeWorldGraph();
    }

    private void EventManager_OnGameOver()
    {
        CheckNewHighScore();
        GameOverUI.Instance.SetGameOverUI(currentScore, highScore);
        MenuManager.Instance.DisplayMenu("GameOver");

        StaticSpawner.PlaySFX("gameOver");

        isDownThrowing = false;
        PlayFabPlayerInfo.SavePlayerInfo();
    }

    private void EventManager_OnNodeClear()
    {
        EventManager.Invoke(CustomEvent.ScoreChange, 5);
        StaticSpawner.PlaySFX("areaClear");

        MenuManager.Instance.DisplayMenu("NodeClear");
        AreaClearUI.Instance.SetScoreText(currentScore);

        WorldMap.CurrentWorldNode.GetComponent<MonsterNode>().OnNodeClear();

        isDownThrowing = false;
    }
}