using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] Transform rotatePoint;
    [Space(10)]
    [SerializeField] float rotationLimit = 75f;
    [SerializeField] [Range(0f, 250f)] float rotateSpeed = 10f;

    private float currentAngle = 0;
    const int ROTATE_OFFSET = 90;

    private void FixedUpdate()
    {
        AimMouse();
    }

    void AimKeys()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float change = rotateSpeed * horizontal * Time.deltaTime;

        if (currentAngle + change > rotationLimit || currentAngle + change < -rotationLimit) return;

        currentAngle += change;
        transform.RotateAround(rotatePoint.position, Vector3.forward, change);
    }

    void AimMouse()
    {
        Vector3 mousePosition = GameManager.Cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = (mousePosition - rotatePoint.position).normalized;

        float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotateSpeed);

        float clampedAngle = Mathf.Clamp(smoothedAngle + ROTATE_OFFSET, -rotationLimit, rotationLimit);
        float change = clampedAngle - currentAngle;

        currentAngle += change;
        transform.RotateAround(rotatePoint.position, Vector3.forward, change);
    }
}