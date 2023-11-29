using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] BackgroundManager backgroundManager;
    [Space(10)]
    [SerializeField] bool useSetSeed = false;
    [SerializeField] int seed = 0;
    [Space(10)]
    public Transform gameTransform;
    [SerializeField] WorldMap worldMap;
    [Space(10)]
    [SerializeField] SideBarUI sideBarUI;
    [SerializeField] CurrencyUI currencyUI;
    [SerializeField] CurrentMonsterUI currentMonsterUI;
    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] NextMonsterUI nextMonsterUI;
    [SerializeField] AimUI aimerUI;
    [SerializeField] ScoreUI scoreUI;

    public static bool isDownThrowing;

    public static InGamePlayer player;
    CurrentMonsters currentMonsters;

    public static int WorldsCleared = 1;
    private const float WORLD_COMPLETE_INCREMENT = .25f;

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

        setSeed();
        Application.targetFrameRate = -1;
        StaticSpawner.Load();

        GameStart();
        isDownThrowing = false;
    }

    void GameStart()
    {
        QualitySettings.vSyncCount = 1;
        player = new InGamePlayer(START_PLAYER_BALLS);

        worldMap.Init();

        sideBarUI.Init();
        currencyUI.Init();
        currentMonsterUI.Init();
        gameOverUI.Init();
        backgroundManager.Init();
        nextMonsterUI.Init();
        aimerUI.Init();

        AimUI.Instance.SetBallsLeftText(player);

        WorldsCleared = 1;
        currentScore = 0;

        scoreUI.SetCurrentScoreText(currentScore);

        backgroundManager.SetRandomBackground();
    }

    public void LoadNode(int monsterCount)
    {
        MenuManager.Instance.DisplayMenu("Game");
        currentMonsters = new CurrentMonsters(monsterCount);
        isDownThrowing = true;
    }

    private void OnEnable()
    {
        EventManager.OnNodeClear += EventManager_OnNodeClear;
        EventManager.OnWorldClear += EventManager_OnWorldClear;

        EventManager.OnGameOver += EventManager_OnGameOver;

        EventManager.OnScoreChange += EventManager_OnScoreChange;
    }

    private void OnDisable()
    {
        EventManager.OnNodeClear -= EventManager_OnNodeClear;
        EventManager.OnWorldClear -= EventManager_OnWorldClear;

        EventManager.OnGameOver -= EventManager_OnGameOver;

        EventManager.OnScoreChange -= EventManager_OnScoreChange;
    }

    void setSeed()
    {
        if (!useSetSeed)
        {
            seed = Random.Range(-999999999, 999999999);
        }

        Random.InitState(seed);
        Debug.LogWarning("Seed: " + seed);
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
    private void EventManager_OnScoreChange(int change)
    {
        currentScore += change * SCORE_MULTIPLIER;
        scoreUI.SetCurrentScoreText(currentScore);
    }

    #endregion

    private void EventManager_OnWorldClear()
    {
        EventManager.InvokeScoreChange(25);
        WorldsCleared++;

        MenuManager.Instance.DisplayMenu("WorldClear");
        WorldClearUI.Instance.SetScoreText(currentScore);

        StaticSpawner.PlaySFX("areaClear");
        backgroundManager.SetRandomBackground();

        WorldMap.Instance.MakeWorldGraph();
    }

    private void EventManager_OnGameOver()
    {
        GameOverUI.Instance.SetGameOverUI(currentScore);
        MenuManager.Instance.DisplayMenu("GameOver");

        StaticSpawner.PlaySFX("gameOver");

        isDownThrowing = false;
    }

    private void EventManager_OnNodeClear()
    {
        EventManager.InvokeScoreChange(5);
        StaticSpawner.PlaySFX("areaClear");

        MenuManager.Instance.DisplayMenu("NodeClear");
        AreaClearUI.Instance.SetScoreText(currentScore);

        WorldMap.CurrentWorldNode.GetComponent<MonsterNode>().OnNodeClear();
        isDownThrowing = false;
    }
}