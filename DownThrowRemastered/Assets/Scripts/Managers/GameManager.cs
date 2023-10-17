using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] BackgroundManager backgroundManager;
    [Space(10)]
    [SerializeField] CurrencyUI currencyUI;
    [SerializeField] CurrentMonsterUI currentMonsterUI;
    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] NextMonsterUI nextMonsterUI;
    [SerializeField] AimUI aimerUI;
    [SerializeField] ScoreUI scoreUI;

    public static bool isPlaying;

    InGamePlayer player;
    CurrentMonsters currentMonsters;

    public static float CurrentDifficulty = 1;
    private const float AREA_COMPLETE_INCREMENT = .33f;

    private int currentScore;
    private int highScore;

    const int SCORE_MULTIPLIER = 10;
    public const int MONSTER_DEFEAT_MULTIPLIER = 3;

    public const int START_PLAYER_BALLS = 5;
    public const int CURRENT_TEST_MONSTERS = 6;
    public const int MAX_MONSTERS = 6;

    public static Camera Cam;

    private void Start()
    {
        Cam = Camera.main;

        PlayFabPlayerInfo.OfflinePlay();
        Application.targetFrameRate = -1;

        Load();
        Init();

        isPlaying = true;
    }

    void Load()
    {
        StaticSpawner.Load();
    }

    void Init()
    {
        QualitySettings.vSyncCount = 1;

        currencyUI.Init();
        currentMonsterUI.Init();
        gameOverUI.Init();
        backgroundManager.Init();
        nextMonsterUI.Init();
        aimerUI.Init();

        player = new InGamePlayer(START_PLAYER_BALLS);
        AimUI.Instance.SetBallsLeftText(player);

        CurrentDifficulty = 1;
        currentMonsters = new CurrentMonsters(CURRENT_TEST_MONSTERS);

        currentScore = 0;
        highScore = PlayFabPlayerInfo.GetHighScore();

        scoreUI.SetHighScoreText(highScore);
        scoreUI.SetCurrentScoreText(currentScore);

        backgroundManager.SetBackground();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!isPlaying) return;

        if (!focus)
        {
            MenuManager.Instance.DisplayMenus("Pause");
        }
    }

    private void OnEnable()
    {
        EventManager.OnAreaClear += EventManager_OnAreaClear;
        EventManager.OnGameOver += EventManager_OnGameOver;

        EventManager.OnScoreChange += EventManager_OnScoreChange;
        EventManager.OnHighScoreChange += EventManager_OnHighScoreChange;
    }

    private void OnDisable()
    {
        EventManager.OnAreaClear -= EventManager_OnAreaClear;
        EventManager.OnGameOver -= EventManager_OnGameOver;

        EventManager.OnScoreChange -= EventManager_OnScoreChange;
        EventManager.OnHighScoreChange -= EventManager_OnHighScoreChange;
    }

    public void SetIsPlaying(bool state)
    {
        isPlaying = state;
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

    private void EventManager_OnGameOver()
    {
        CheckNewHighScore();

        gameOverUI.SetGameOverUI(currentScore, highScore);

        MenuManager.Instance.DisplayMenus("GameOver");
        StaticSpawner.PlaySFX("gameOver");

        isPlaying = false;

        PlayFabPlayerInfo.SavePlayerInfo();
    }

    private void EventManager_OnAreaClear()
    {
        Debug.Log("Area clear!");
        StaticSpawner.PlaySFX("areaClear");
        CurrentDifficulty += AREA_COMPLETE_INCREMENT;

        MenuManager.Instance.DisplayMenus("AreaClear");
        AreaClearUI.Instance.SetScoreText(currentScore);

        currentMonsters = new CurrentMonsters(CURRENT_TEST_MONSTERS);
        backgroundManager.SetBackground();
    }
}