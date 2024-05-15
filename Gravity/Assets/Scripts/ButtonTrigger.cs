using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject reverseGravityZone;

    public void Start()
    {
        reverseGravityZone.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        reverseGravityZone.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        reverseGravityZone.SetActive(false);
    }
}
