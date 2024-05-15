using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the current active scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Load the next scene in the build order
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
