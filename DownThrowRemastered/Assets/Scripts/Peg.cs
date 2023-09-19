using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    [SerializeField] float pegDestroyTime = 1.5f;
    float currentDestroyTime = 0;

    bool hasBallCollided = false;
    bool hasBallTriggered = false;
    bool hasDeathCalled = false;

    delegate void PegHit();
    event PegHit onPegHit;

    delegate void PegDeath();
    event PegDeath onPegDeath;

    private void Awake()
    {
        onPegHit += Peg_OnPegHit;
        onPegDeath += Peg_onPegDeath;
    }

    private void Peg_OnPegHit()
    {
        hasBallCollided = true;
    }

    private void Peg_onPegDeath()
    {
        if (hasDeathCalled) return;
        hasDeathCalled = true;

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBall(collision.gameObject))
        {
            onPegHit?.Invoke();
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
            if (currentDestroyTime < pegDestroyTime)
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
        if (isBall(collision.gameObject))
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
