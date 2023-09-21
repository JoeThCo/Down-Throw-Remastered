using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PegHit();
    public static event PegHit OnPegHit;

    public delegate void PegDestroy();
    public static event PegDestroy OnPegDestroy;

    public delegate void PlayerShoot();
    public static event PlayerShoot OnPlayerShoot;

    public delegate void BallBottoms(Ball ball);
    public static event BallBottoms OnBallBottoms;

    public delegate void NewMonster(Monster monster);
    public static event NewMonster OnNewMonster;

    public delegate void MonsterDamage(Ball ball);
    public static event MonsterDamage OnMonsterDamage;

    public delegate void MonsterDead(Monster monster);
    public static event MonsterDead OnMonsterDead;

    public delegate void AreaClear();
    public static event AreaClear OnAreaClear;

    public delegate void GameOver();
    public static event AreaClear OnGameOver;

    public delegate void YouWin();
    public static event AreaClear OnYouWin;

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

            //Player
            case CustomEvent.PlayerShoot:
                OnPlayerShoot?.Invoke();
                break;

            //Monsters
            case CustomEvent.NewMonster:
                OnNewMonster?.Invoke((Monster)parameter);
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

            case CustomEvent.AreaClear:
                OnAreaClear?.Invoke();
                break;

            case CustomEvent.GameOver:
                OnGameOver?.Invoke();
                break;

            case CustomEvent.YouWin:
                OnYouWin?.Invoke();
                break;

            default:
                break;
        }
    }
}