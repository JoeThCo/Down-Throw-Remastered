using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PegBoard pegSpawner;
    [SerializeField] AimerUI aimerUI;
    [SerializeField] ScoreUI scoreUI;

    Player player;
    CurrentMonsters currentMonsters;

    private int currentScore;
    private int highScore;

    const int SCORE_MULTIPLIER = 10;
    public const int MONSTER_DEFEAT_MULTIPLIER = 3;

    const string HIGHSCORE_ID = "HighScore";

    static MonsterSO[] allMonsters;

    private void Start()
    {
        Load();
        Init();

        pegSpawner.NewBoard();
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

    #region Score
    private void EventManager_OnHighScoreChange()
    {
        SetNewHighScore(currentScore);
    }

    bool isNewHighScore()
    {
        return currentScore > highScore;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGHSCORE_ID);
    }

    public void SetNewHighScore(int highscore)
    {
        PlayerPrefs.SetInt(HIGHSCORE_ID, highscore);
    }

    private void EventManager_OnScoreChange(int change)
    {
        currentScore += change * SCORE_MULTIPLIER;
        scoreUI.SetCurrentScoreText(currentScore);
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
        MenuManager.Instance.DisplayMenus("GameOver");
    }

    private void EventManager_OnAreaClear()
    {
        Debug.Log("Area clear!");
        MenuManager.Instance.DisplayMenus("AreaClear");
        currentMonsters = new CurrentMonsters(3);
    }

    void Load()
    {
        ItemSpawner.Load();
        allMonsters = Resources.LoadAll<MonsterSO>("Monsters");
    }

    void Init()
    {
        aimerUI.Init();

        player = new Player(1);
        AimerUI.Instance.SetBallsLeftText(player);
        currentMonsters = new CurrentMonsters(3);

        currentScore = 0;
        highScore = GetHighScore();

        scoreUI.SetHighScoreText(highScore);
        scoreUI.SetCurrentScoreText(currentScore);
    }

    public static MonsterSO GetRandomMonster()
    {
        return allMonsters[Random.Range(0, allMonsters.Length)];
    }
}