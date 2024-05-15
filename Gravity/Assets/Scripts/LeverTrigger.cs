using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public GameObject reverseGravityZone;
    public bool useTimer = false;  // Boolean to control if the timer is used
    public float timerDuration = 5f;  // Duration of the timer in seconds

    private bool isTimerActive = false;
    private float timer = 0f;

    void Start()
    {
        reverseGravityZone.SetActive(false);
    }

    void Update()
    {
        if (isTimerActive)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0f)
            {
                reverseGravityZone.SetActive(false);
                isTimerActive = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool wasActive = reverseGravityZone.activeSelf;

            reverseGravityZone.SetActive(!wasActive);

            if (useTimer && !wasActive)
            {
                StartTimer();
            }
        }
    }

    private void StartTimer()
    {
        isTimerActive = true;
        timer = timerDuration;
    }
}
