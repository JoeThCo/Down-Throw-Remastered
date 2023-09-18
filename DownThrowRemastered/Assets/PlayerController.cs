using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool canShoot = true;
    [Space(10)]
    [SerializeField] float throwPower = 5f;
    [SerializeField] Transform firePoint;

    public delegate void PlayerShootBall();

    public static event PlayerShootBall playerShootBall;

    private void Start()
    {
        playerShootBall += OnShootBall;
        BottomEdge.bottomEdgeCollision += OnBottemEdgeCollision;
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