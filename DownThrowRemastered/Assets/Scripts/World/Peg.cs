using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    public const float DESTROY_TIME = 1.5f;
    float currentDestroyTime = 0;

    bool hasBallCollided = false;
    bool hasBallTriggered = false;
    bool hasDeathCalled = false;

    delegate void PegHit(Ball ball);
    event PegHit onPegHit;

    delegate void PegDeath();
    event PegDeath onPegDeath;

    public virtual void Awake()
    {
        onPegHit += Peg_OnPegHit;
        onPegDeath += Peg_onPegDeath;
    }

    public virtual void OnPegHit(Ball ball) { }

    void Peg_OnPegHit(Ball ball)
    {
        hasBallCollided = true;

        OnPegHit(ball);
    }

    public virtual void OnPegDeath()
    {
        EventManager.Invoke(CustomEvent.ScoreChange, 1);
    }

    void Peg_onPegDeath()
    {
        if (hasDeathCalled) return;
        hasDeathCalled = true;

        OnPegDeath();

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBall(collision.gameObject))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();

            onPegHit?.Invoke(ball);
            EventManager.Invoke(CustomEvent.PegHit);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBall(collision.gameObject))
        {
            hasBallTriggered = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isBall(collision.gameObject))
        {
            if (currentDestroyTime < DESTROY_TIME)
            {
                currentDestroyTime += Time.deltaTime;
            }
            else
            {
                onPegDeath?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBall(collision.gameObject) && hasBallTriggeredAndCollided())
        {
            EventManager.Invoke(CustomEvent.PegDestroy);
            onPegDeath?.Invoke();
        }
        else
        {
            ResetBools();
        }
    }

    void ResetBools()
    {
        hasBallTriggered = false;
        hasBallCollided = false;
    }

    bool hasBallTriggeredAndCollided()
    {
        return hasBallTriggered && hasBallCollided;
    }

    bool isBall(GameObject obj)
    {
        return obj.CompareTag("Ball");
    }
}
