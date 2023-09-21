using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PegBoard pegSpawner;
    [SerializeField] AimerUI aimerUI;
    [SerializeField] ScoreUI scoreUI;

    static Player player;
    static CurrentMonsters currentMonsters;

    private int currentScore;
    private int highScore;

    const int SCORE_MULTIPLIER = 10;
    const string HIGHSCORE_ID = "HighScore";

    static MonsterSO[] allMonsters;

    private void Start()
    {
        Load();
        Init();

        pegSpawner.NewBoard();

        EventManager.OnAreaClear += EventManager_OnAreaClear;

        EventManager.OnYouWin += EventManager_OnYouWin;
        EventManager.OnGameOver += EventManager_OnGameOver;

        EventManager.OnScoreChange += EventManager_OnScoreChange;
        EventManager.OnHighScoreChange += EventManager_OnHighScoreChange;
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

    private void EventManager_OnYouWin()
    {
        CheckNewHighScore();
        MenuManager.Instance.DisplayMenus("YouWin");
    }

    private void EventManager_OnGameOver()
    {
        CheckNewHighScore();
        MenuManager.Instance.DisplayMenus("GameOver");
    }

    private void EventManager_OnAreaClear()
    {
        Debug.Log("Area clear!");
    }

    void Load()
    {
        ItemSpawner.Load();
        LoadMonsters();
    }

    void Init()
    {
        aimerUI.Init();
        PlayerInit();
        CurrentMonsterInit();

        currentScore = 0;
        highScore = GetHighScore();

        scoreUI.SetHighScoreText(highScore);
        scoreUI.SetCurrentScoreText(currentScore);
    }

    void LoadMonsters()
    {
        allMonsters = Resources.LoadAll<MonsterSO>("Monsters");
    }

    void PlayerInit()
    {
        player = new Player(10);
        AimerUI.Instance.SetBallsLeftText(player);
    }

    void CurrentMonsterInit()
    {
        currentMonsters = new CurrentMonsters(3);
    }

    public static MonsterSO GetRandomMonster()
    {
        return allMonsters[Random.Range(0, allMonsters.Length)];
    }
}