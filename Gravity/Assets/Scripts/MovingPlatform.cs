using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;  // Starting point
    public Transform pointB;  // End point
    public float speed = 2f;  // Speed of the platform
    public bool stopAtPointB;  // Boolean to control the platform behavior

    private Vector3 targetPosition;
    private bool movingToPointB = true;

    void Start()
    {
        // Initialize the target position to point B
        targetPosition = pointB.position;
    }

    void Update()
    {
        if (stopAtPointB)
        {
            MoveToPointB();
        }
        else
        {
            PingPongMovement();
        }
    }

    void MoveToPointB()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
        {
            // Stop moving once we reach point B
            enabled = false;
        }
    }

    void PingPongMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Switch target position when the platform reaches the target
            if (movingToPointB)
            {
                targetPosition = pointA.position;
            }
            else
            {
                targetPosition = pointB.position;
            }
            movingToPointB = !movingToPointB;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
