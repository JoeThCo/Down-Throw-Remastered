using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] Transform rotatePoint;
    [SerializeField] [Range(5, 10)] float aimSensitivty;
    [SerializeField] float rotationLimit = 75f;

    float lastAngle = 0;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    float GetShooterAngle(Vector2 mousePos)
    {
        float angle = mousePos.x / (aimSensitivty);
        return Mathf.Clamp(angle, -1, 1) * rotationLimit;
    }

    private void FixedUpdate()
    {
        float angle = GetShooterAngle(cam.ScreenToWorldPoint(Input.mousePosition));
        //transform.rotation = Quaternion.Euler(Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles);
        transform.RotateAround(rotatePoint.position, Vector3.forward, -(lastAngle - angle));

        lastAngle = angle;
    }
}