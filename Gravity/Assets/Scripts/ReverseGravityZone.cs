using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.gravityScale = -2f;  // Reverse the gravity
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.gravityScale = 1;  // Reset the gravity to normal
        }
    }
}
