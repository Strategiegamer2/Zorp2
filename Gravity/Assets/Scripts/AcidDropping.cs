using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDropping : MonoBehaviour
{
    public GameObject acidPrefab;  // Prefab of the acid to be dropped
    public Transform dropPoint;  // Point from where the acid will be dropped
    public float dropInterval = 2f;  // Interval between drops

    void Start()
    {
        // Start dropping acid at regular intervals
        InvokeRepeating("DropAcid", 0f, dropInterval);
    }

    void DropAcid()
    {
        Instantiate(acidPrefab, dropPoint.position, dropPoint.rotation);
    }
}
