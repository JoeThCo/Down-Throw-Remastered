using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] Transform rotatePoint;
    [SerializeField] AimController aimController;
    [Space(10)]
    [SerializeField] [Range(5, 10)] float aimSensitivty;
    [SerializeField] float rotationLimit = 75f;

    private const float MIN_PERCENT = -1;
    private const float MAX_PERCENT = 1;

    float lastAngle = 0;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    float GetShooterAngle(Vector2 mousePos)
    {
        float angle = mousePos.x / (aimSensitivty);
        return Mathf.Clamp(angle, MIN_PERCENT, MAX_PERCENT) * rotationLimit;
    }

    private void FixedUpdate()
    {
        if (!aimController.CanAngleChange()) return;

        float angle = GetShooterAngle(cam.ScreenToWorldPoint(Input.mousePosition));
        transform.RotateAround(rotatePoint.position, Vector3.forward, -(lastAngle - angle));

        lastAngle = angle;
    }
}