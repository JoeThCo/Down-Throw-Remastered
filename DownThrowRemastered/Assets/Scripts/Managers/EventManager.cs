using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public delegate void PegHit(Peg peg);
    public static event PegHit OnPegHit;

    public delegate void PegDestroy();
    public static event PegDestroy OnPegDestroy;

    public delegate void BoardClear();
    public static event BoardClear OnBoardClear;

    public delegate void PlayerShootStart();
    public static event PlayerShootStart OnPlayerShootStart;

    public delegate void PlayerShootEnd();
    public static event PlayerShootEnd OnPlayerShootEnd;

    public delegate void BallBottoms(Ball ball);
    public static event BallBottoms OnBallBottoms;

    public delegate void NewMonster(Monster monster);
    public static event NewMonster OnNewMonster;

    public delegate void MonsterDamage(int damage);
    public static event MonsterDamage OnMonsterDamage;

    public delegate void MonsterEffectDamage(int damage);
    public static event MonsterEffectDamage OnMonsterEffectDamage;

    public delegate void MonsterDead(Monster monster);
    public static event MonsterDead OnMonsterDead;

    public delegate void NodeClear();
    public static event NodeClear OnNodeClear;

    public delegate void WorldClear();
    public static event WorldClear OnWorldClear;

    public delegate void ScoreChange(int change);
    public static event ScoreChange OnScoreChange;

    public delegate void GameOver();
    public static event GameOver OnGameOver;

    public delegate void YouWin();
    public static event YouWin OnYouWin;

    public delegate void GoldChange(int change);
    public static event GoldChange OnGoldChange;

    public static void InvokeOnPegHit(Peg peg)
    {
        OnPegHit?.Invoke(peg);
    }

    public static void InvokeOnPegDestroy()
    {
        OnPegDestroy?.Invoke();
    }

    public static void InvokeBoardClear()
    {
        OnBoardClear?.Invoke();
    }

    public static void InvokePlayerShootStart()
    {
        OnPlayerShootStart?.Invoke();
    }

    public static void InvokePlayerShootEnd()
    {
        OnPlayerShootEnd?.Invoke();
    }

    public static void InvokeOnNewMonster(Monster monster)
    {
        OnNewMonster?.Invoke(monster);
    }

    public static void InvokeOnMonsterDamage(int damage)
    {
        OnMonsterDamage?.Invoke(damage);
    }

    public static void InvokeOnMonsterEffectDamage(int damage)
    {
        OnMonsterEffectDamage?.Invoke(damage);
    }

    public static void InvokeOnMonsterDead(Monster monster)
    {
        OnMonsterDead?.Invoke(monster);
    }

    public static void InvokeBallBottoms(Ball ball)
    {
        OnBallBottoms?.Invoke(ball);
    }

    public static void InvokeNodeClear()
    {
        OnNodeClear?.Invoke();
    }

    public static void InvokeWorldClear()
    {
        OnWorldClear?.Invoke();
    }

    public static void InvokeScoreChange(int change)
    {
        OnScoreChange?.Invoke(change);
    }

    public static void InvokeGameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void InvokeYouWin()
    {
        OnYouWin?.Invoke();
    }

    public static void InvokeGoldChange(int change)
    {
        OnGoldChange?.Invoke(change);
    }
}