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

    public delegate void MonsterDamage();
    public static event MonsterDamage OnMonsterDamage;

    public delegate void MonsterDeath();
    public static event MonsterDeath OnMonsterDeath;

    public static void Invoke(CustomEvent customEvent, Ball ball = null)
    {
        switch (customEvent)
        {
            case CustomEvent.PegHit:
                OnPegHit?.Invoke();
                break;

            case CustomEvent.PegDestroy:
                OnPegDestroy?.Invoke();
                break;

            case CustomEvent.PlayerShoot:
                OnPlayerShoot?.Invoke();
                break;

            case CustomEvent.MonsterDamage:
                OnMonsterDamage?.Invoke();
                break;

            case CustomEvent.MonsterDeath:
                OnMonsterDeath?.Invoke();
                break;

            case CustomEvent.BallBottoms:
                OnBallBottoms?.Invoke(ball);
                break;

            default:
                break;
        }
    }
}