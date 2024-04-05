using Unity.VisualScripting;
using UnityEngine;

public class SideWalls : MonoBehaviour
{
    public GameObject ballTrail;

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Check if the PortalWalls are active
        PortalWalls portalWalls = FindObjectOfType<PortalWalls>();
        if (portalWalls != null && portalWalls.portalWallsActive == true)
        {
            // Your code to react when portalWallsActive is true
            if (col.gameObject.CompareTag("Ball"))
            {
                // Get the velocity of the ball
                Rigidbody2D ballRigidbody = col.gameObject.GetComponent<Rigidbody2D>();
                Vector2 velocity = ballRigidbody.velocity;

                // Reverse the x-component of the velocity
                velocity.x *= -1;

                // Apply the new velocity to the ball
                ballRigidbody.velocity = velocity;

                // Teleport the ball to the other side
                Vector3 newPosition = col.transform.position;
                newPosition.x = newPosition.x * -1;
                col.transform.position = newPosition;
            }
        }
    }
}
