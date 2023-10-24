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

    public static float CurrentDifficulty = 1;
    private const float WORLD_COMPLETE_INCREMENT = .33f;

    public static int currentScore { get; set; }
    public static int highScore { get; set; }

    const int SCORE_MULTIPLIER = 10;
    public const int MONSTER_DEFEAT_MULTIPLIER = 3;

    public const int START_PLAYER_BALLS = 5;
    public const int MAX_MONSTERS = 1;

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

        CurrentDifficulty = 1;

        currentScore = 0;
        highScore = PlayFabPlayerInfo.GetHighScore();

        scoreUI.SetHighScoreText(highScore);
        scoreUI.SetCurrentScoreText(currentScore);

        backgroundManager.SetRandomBackground();
    }

    public void LoadNode(int monsterCount)
    {
        MenuManager.Instance.DisplayMenus("Game");
        currentMonsters = new CurrentMonsters(monsterCount);
        isDownThrowing = true;
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
        Debug.Log("Area clear!");
        StaticSpawner.PlaySFX("areaClear");

        MenuManager.Instance.DisplayMenus("AreaClear");
        AreaClearUI.Instance.SetScoreText(currentScore);

        WorldMap.CurrentWorldNode.GetComponent<MonsterNode>().OnNodeClear();

        isDownThrowing = false;
    }
}