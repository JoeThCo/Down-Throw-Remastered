using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class Peg : MonoBehaviour
{
    public const float DESTROY_TIME = 1.5f;
    float currentDestroyTime = 0;

    bool hasBallCollided = false;
    bool hasBallTriggered = false;
    bool hasDeathCalled = false;

    const float BASE_TIME = .1f;
    const float RANDOM_TIME = .25f;

    delegate void PegHit();
    event PegHit onPegHit;

    delegate void PegDeath(Ball ball);
    event PegDeath onPegDeath;

    private void Start()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;
        spawnJuice(scale);
    }

    void spawnJuice(Vector3 scale)
    {
        Tween.Scale(transform, Vector3.zero, scale, duration: BASE_TIME + (Random.value * RANDOM_TIME), ease: Ease.Linear);
    }

    private void OnEnable()
    {
        onPegHit += Peg_OnPegHit;
        onPegDeath += Peg_onPegDeath;
    }

    private void OnDisable()
    {
        onPegHit -= Peg_OnPegHit;
        onPegDeath -= Peg_onPegDeath;
    }

    public virtual void OnPegHit()
    {
        StaticSpawner.PlaySFX("pegHit");
    }

    void Peg_OnPegHit()
    {
        hasBallCollided = true;
        OnPegHit();
    }

    public virtual void OnPegDeath(Ball ball)
    {
        EventManager.InvokeOnPegHit(this);
        EventManager.InvokeScoreChange(1);
        StaticSpawner.PlaySFX("pegDeath");

        ParticleSystem breakPart = StaticSpawner.SpawnGame("PegBreak", transform.position).GetComponent<ParticleSystem>();

        ParticleSystem.MainModule main = breakPart.main;
        Color setColor = GetComponentInChildren<SpriteRenderer>().color;
        main.startColor = setColor;
    }

    void Peg_onPegDeath(Ball ball)
    {
        if (hasDeathCalled) return;
        hasDeathCalled = true;

        OnPegDeath(ball);

        Destroy(gameObject);
    }

    #region Collision/Triggers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBall(collision.gameObject))
        {
            onPegHit?.Invoke();
            EventManager.InvokeOnPegHit(this);
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
                Ball ball = collision.gameObject.GetComponent<Ball>();
                onPegDeath?.Invoke(ball);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBall(collision.gameObject) && hasBallTriggeredAndCollided())
        {
            EventManager.InvokeOnPegDestroy();

            Ball ball = collision.gameObject.GetComponent<Ball>();
            onPegDeath?.Invoke(ball);
        }
        else
        {
            ResetBools();
        }
    }
    #endregion

    #region Helpers
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
    #endregion
}
