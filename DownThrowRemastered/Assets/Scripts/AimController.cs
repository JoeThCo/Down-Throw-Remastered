using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] float throwPower = 5f;
    [SerializeField] Transform firePoint;

    [HideInInspector] public bool canShoot = true;

    public delegate void PlayerShootBall();
    public static event PlayerShootBall playerShootBall;

    private void Start()
    {
        playerShootBall += OnShootBall;
        Bottom.bottomEdgeCollision += OnBottemEdgeCollision;
    }

    private void OnBottemEdgeCollision()
    {
        canShoot = true;
    }

    private void OnShootBall()
    {
        Rigidbody2D ball = ItemSpawner.SpawnGame("Ball", firePoint.transform.position).GetComponent<Rigidbody2D>();
        ball.velocity = -firePoint.transform.up * throwPower;

        canShoot = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && canShoot)
        {
            playerShootBall.Invoke();
        }
    }
}