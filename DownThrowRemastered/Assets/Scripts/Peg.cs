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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBall(collision.gameObject))
        {
            hasBallCollided = true;
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
                OnPegDeath();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBall(collision.gameObject))
        {
            if (isBall(collision.gameObject) && hasBallTriggeredAndCollided())
            {
                OnPegDeath();
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

    void OnPegDeath()
    {
        if (hasDeathCalled) return;
        hasDeathCalled = true;

        Destroy(gameObject);
    }
}
