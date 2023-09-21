using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    private void Start()
    {
        EventManager.OnBallBottoms += EventManager_OnBallBottoms;
    }

    private void EventManager_OnBallBottoms(Ball ball)
    {
        if (ball == null) return;
        Debug.Log("Damage: " + ball.damage);
        EventManager.Invoke(CustomEvent.MonsterDamage, ball);
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