using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public delegate void PegHit();
    public static event PegHit OnPegHit;

    public delegate void PegDestroy();
    public static event PegDestroy OnPegDestroy;

    public delegate void BoardClear();
    public static event BoardClear OnBoardClear;

    public delegate void PlayerShootStart();
    public static event PlayerShootStart OnPlayerShoot;

    public delegate void PlayerShootEnd();
    public static event PlayerShootEnd OnPlayerShootEnd;

    public delegate void BallBottoms(Ball ball);
    public static event BallBottoms OnBallBottoms;

    public delegate void NewMonster();
    public static event NewMonster OnNewMonster;

    public delegate void MonsterDamage(Ball ball);
    public static event MonsterDamage OnMonsterDamage;

    public delegate void MonsterDead(Monster monster);
    public static event MonsterDead OnMonsterDead;

    public delegate void NodeEnter(int count);
    public static event NodeEnter OnNodeEnter;

    public delegate void NodeClear();
    public static event NodeClear OnNodeClear;

    public delegate void WorldClear();
    public static event WorldClear OnWorldClear;

    public delegate void ScoreChange(int change);
    public static event ScoreChange OnScoreChange;

    public delegate void HighScoreChange();
    public static event HighScoreChange OnHighScoreChange;

    public delegate void GameOver();
    public static event NodeClear OnGameOver;

    public delegate void YouWin();
    public static event NodeClear OnYouWin;

    public delegate void GoldChange(int change);
    public static event GoldChange OnCashChange;

    public static void Invoke(CustomEvent customEvent, object parameter = null)
    {
        switch (customEvent)
        {
            //Pegs
            case CustomEvent.PegHit:
                OnPegHit?.Invoke();
                break;

            case CustomEvent.PegDestroy:
                OnPegDestroy?.Invoke();
                break;

            case CustomEvent.BoardClear:
                OnBoardClear?.Invoke();
                break;

            //Player
            case CustomEvent.PlayerShootStart:
                OnPlayerShoot?.Invoke();
                break;

            case CustomEvent.PlayerShootEnd:
                OnPlayerShootEnd?.Invoke();
                break;

            //Monsters
            case CustomEvent.NewMonster:
                OnNewMonster?.Invoke();
                break;

            case CustomEvent.MonsterDamage:
                OnMonsterDamage?.Invoke((Ball)parameter);
                break;

            case CustomEvent.MonsterDead:
                OnMonsterDead?.Invoke((Monster)parameter);
                break;

            //Other
            case CustomEvent.BallBottoms:
                OnBallBottoms?.Invoke((Ball)parameter);
                break;

            case CustomEvent.NodeEnter:
                OnNodeEnter?.Invoke(Convert.ToInt32(parameter));
                break;

            case CustomEvent.NodeClear:
                OnNodeClear?.Invoke();
                break;

            case CustomEvent.WorldClear:
                OnWorldClear?.Invoke();
                break;

            //Game
            case CustomEvent.HighScoreChange:
                OnHighScoreChange?.Invoke();
                break;

            case CustomEvent.ScoreChange:
                OnScoreChange?.Invoke(Convert.ToInt32(parameter));
                break;

            case CustomEvent.GameOver:
                OnGameOver?.Invoke();
                break;

            case CustomEvent.YouWin:
                OnYouWin?.Invoke();
                break;

            case CustomEvent.GoldChange:
                OnCashChange?.Invoke(Convert.ToInt32(parameter));
                break;

            default:
                break;
        }
    }
}