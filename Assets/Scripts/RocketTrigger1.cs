using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTrigger1 : MonoBehaviour
{
    public RocketController rocket;
    public RocketController rocket2;
    public RocketController rocket3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rocket.StartMoving();
            rocket2.StartMoving();
            rocket3.StartMoving();
        }
    }
}
