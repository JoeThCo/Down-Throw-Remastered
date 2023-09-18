using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    public delegate void BottomEdgeCollision();
    public static event BottomEdgeCollision bottomEdgeCollision;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            bottomEdgeCollision.Invoke();
            Destroy(collision.gameObject);
        }
    }
}