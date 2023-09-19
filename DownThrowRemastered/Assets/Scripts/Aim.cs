using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] [Range(5, 10)] float aimSensitivty;
    [SerializeField] float rotationLimit = 75f;

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
        transform.rotation = Quaternion.Euler(Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles);
    }
}