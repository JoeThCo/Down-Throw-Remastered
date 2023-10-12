using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] Transform rotatePoint;
    [SerializeField] AimController aimController;
    [Space(10)]
    private float currentAngle = 0;
    [SerializeField] float rotationLimit = 75f;
    [SerializeField] [Range(0f, 250f)] float rotateSpeed = 10f;

    private void FixedUpdate()
    {
        if (!aimController.canShoot) return;

        float horizontal = Input.GetAxis("Horizontal");
        float change = rotateSpeed * horizontal * Time.deltaTime * SettingsManager.GetAimSensitivity();

        if (currentAngle + change > rotationLimit || currentAngle + change < -rotationLimit) return;

        currentAngle += change;
        transform.RotateAround(rotatePoint.position, Vector3.forward, change);
    }
}