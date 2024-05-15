using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    public float speed = 5f;  // Speed of the acid drop

    void Update()
    {
        // Move the acid downward
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Werkt");
        if (other.CompareTag("Player"))
        {
            // Call the CheckForRespawnDeath method on the player
            other.GetComponent<Playermovment>().CheckForRespawnDeath();
        }

        // Destroy the acid drop when it hits something
        Destroy(gameObject);
    }
}
