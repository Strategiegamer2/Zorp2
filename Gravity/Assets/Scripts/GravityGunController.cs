using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunController : MonoBehaviour
{
    public float range = 10f;  // Max range of the gravity gun
    public LayerMask magneticLayer;  // Layer mask to identify magnetic blocks
    public float forceMagnitude = 0.0001f;  // Force applied to the block when pushing or pulling
    public Color rayColor = Color.red;  // Color of the raycast line
    public Camera cam;

    private Transform selectedBlock;


    void Update()
    {
        RaycastForBlock();
        ApplyForceToBlock();
    }

    void RaycastForBlock()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector2 mousePosition2D = cam.ScreenToWorldPoint(mousePosition);  // Convert to world coordinates

        // Define the end point for visualization, casting vertically down in the Scene view might be counterintuitive in 2D view
        Vector2 endPoint = new Vector2(mousePosition2D.x, mousePosition2D.y - range);

        // Cast a ray from the mouse position
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.down, range, magneticLayer);

        // Draw the ray in the Scene view for debugging
        Debug.DrawLine(mousePosition2D, endPoint, rayColor, 0.02f);

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (hit.collider != null)
            {
                selectedBlock = hit.transform;  // Select the block if hit
            }
            else
            {
                selectedBlock = null;  // Deselect if no block is hit
            }
        }
    }

    void ApplyForceToBlock()
    {
        if (selectedBlock != null)
        {
            Vector2 directionToBlock = (selectedBlock.position - transform.position).normalized;
            Rigidbody2D blockRigidbody = selectedBlock.GetComponent<Rigidbody2D>();

            if (Input.GetMouseButton(0))  // Left mouse button (Pull)
            {
                blockRigidbody.AddForce(-directionToBlock * forceMagnitude);  // Pull the block towards the player
            }
            else if (Input.GetMouseButton(1))  // Right mouse button (Push)
            {
                blockRigidbody.AddForce(directionToBlock * forceMagnitude);  // Push the block away from the player
            }
        }
    }
}
