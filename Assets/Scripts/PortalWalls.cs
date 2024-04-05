using UnityEngine;
using System.Collections;
using System;

public class PortalWalls : MonoBehaviour
{
    public bool portalWallsActive = false;

    public SpriteRenderer sprite;

    public SpriteRenderer leftWallSprite;
    public SpriteRenderer rightWallSprite;

    private Color originalLeftColor;
    private Color originalRightColor;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Slider")
        {
            // Save the original colors of the walls
            originalLeftColor = leftWallSprite.color;
            originalRightColor = rightWallSprite.color;

            // Change the colors of the walls to white
            leftWallSprite.color = Color.white;
            rightWallSprite.color = Color.white;

            portalWallsActive = true;
            StartCoroutine(DeactivatePortalWalls(30f));
            sprite.enabled = false;
            StartCoroutine(DeactivateSprite(40f));
        }
    }
    IEnumerator DeactivatePortalWalls(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Set the Trail Renderer active after the specified delay
        portalWallsActive = false;

        // Reset the colors of the walls to their original colors
        leftWallSprite.color = originalLeftColor;
        rightWallSprite.color = originalRightColor;
    }

    IEnumerator DeactivateSprite(float delay)
    {
        yield return new WaitForSeconds(delay);

        this.gameObject.SetActive(false);
    }
}
