using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] AimerUI aimerUI;
    [SerializeField] ScoreUI scoreUI;

    Player player;
    CurrentMonsters currentMonsters;

    public static float CurrentDifficulty = 1;
    private const float AREA_COMPLETE_INCREMENT = .33f;

    private int currentScore;
    private int highScore;

    const int SCORE_MULTIPLIER = 10;
    public const int MONSTER_DEFEAT_MULTIPLIER = 3;

    const string HIGHSCORE_ID = "HighScore";

    public const int CURRENT_TEST_MONSTERS = 3;
    public const float MAX_MONSTERS = 6;
    static MonsterSO[] allMonsters;

    private void Start()
    {
        Load();
        Init();
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
        ItemSpawner.PlaySFX("gameOver");
    }

    private void EventManager_OnAreaClear()
    {
        Debug.Log("Area clear!");
        ItemSpawner.PlaySFX("areaClear");
        CurrentDifficulty += AREA_COMPLETE_INCREMENT;

        MenuManager.Instance.DisplayMenus("AreaClear");
        AreaClearUI.Instance.SetScoreText(currentScore);
        
        currentMonsters = new CurrentMonsters(CURRENT_TEST_MONSTERS);
    }

    void Load()
    {
        ItemSpawner.Load();
        allMonsters = Resources.LoadAll<MonsterSO>("Monsters");
    }

    void Init()
    {
        aimerUI.Init();

        player = new Player(10);
        AimerUI.Instance.SetBallsLeftText(player);
        currentMonsters = new CurrentMonsters(CURRENT_TEST_MONSTERS);

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