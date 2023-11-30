using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class Ball : MonoBehaviour
{
    [SerializeField] Transform sprite;
    [SerializeField] Rigidbody2D rb;
    public int Damage { get; private set; }

    [Space(10)]

    [SerializeField] float maxBallSpeed = 5;

    [Space(10)]

    [SerializeField] float maxScale = 1.25f;
    [SerializeField] float maxTime = .12f;

    const float MIN_SCALE = .01f;
    const float MIN_CLAMP = .1f;
    const float MAX_CLAMP = 1f;

    public void ChangeDamage(int change)
    {
        Damage += change;
    }

    void collisionJuice()
    {
        float speed = lerpedBallSpeed();
        float scale = (1 + speed) * maxScale;
        float time = Mathf.Max(MIN_SCALE, maxTime - (speed * maxTime));

        Sequence.Create()
            .Chain(Tween.Scale(sprite, startValue: Vector3.one, endValue: Vector3.one * scale, duration: time, Ease.Linear))
            .Chain(Tween.Scale(sprite, startValue: Vector3.one * scale, endValue: Vector3.one, duration: time, Ease.Linear));
    }

    float lerpedBallSpeed()
    {
        return Mathf.Clamp(rb.velocity.magnitude / maxBallSpeed, MIN_CLAMP, MAX_CLAMP);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionJuice();
    }
}