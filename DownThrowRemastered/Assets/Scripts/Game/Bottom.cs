using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnBallBottoms += EventManager_OnBallBottoms;
    }

    private void OnDisable()
    {
        EventManager.OnBallBottoms -= EventManager_OnBallBottoms;
    }

    private void EventManager_OnBallBottoms(Ball ball)
    {
        EventManager.Invoke(CustomEvent.MonsterDamage, ball.Damage);
        EventManager.Invoke(CustomEvent.PlayerShootEnd);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            EventManager.Invoke(CustomEvent.BallBottoms, ball);

            Destroy(collision.gameObject);
        }
    }
}