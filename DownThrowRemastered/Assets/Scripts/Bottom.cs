using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            EventManager.Invoke(CustomEvent.BallBottoms);
            Destroy(collision.gameObject);
        }
    }
}