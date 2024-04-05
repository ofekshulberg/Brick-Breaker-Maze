using UnityEngine;
using System.Collections;

public class SliderScript : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;

    public GameObject slider;

    public GameObject laserGunPrefab;
    private GameObject laserGun;

    public GameObject bulletsPrefab;
    private GameObject bullets;

    public float moveSpeed = 5f;  // Adjust this speed as needed

    public float maxBounceAngle = 75f;

    private float sliderWidth;

    void Start()
    {
        // Get the width of the slider
        sliderWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        laserGun = Instantiate(laserGunPrefab, transform.position, Quaternion.identity, transform);
        bullets = Instantiate(bulletsPrefab, transform.position, Quaternion.identity, transform);
    }

    public void ActivateLaserGun()
    {
        // Set the child object active
        laserGun.SetActive(true);
        bullets.SetActive(true);

        // Start the coroutine to deactivate after 30 seconds
        StartCoroutine(DeactivateLaserGun(30f));
    }

    IEnumerator DeactivateLaserGun(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Set the child object inactive after the specified delay
        laserGun.SetActive(false);
        bullets.SetActive(false);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float newXPosition = Mathf.Clamp(mousePosition.x, leftWall.transform.position.x + sliderWidth / 2, rightWall.transform.position.x - sliderWidth / 2);

        slider.transform.position = new Vector3(newXPosition, slider.transform.position.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Wall" && col.gameObject.tag != "Bullet")
        {        
            Ball ball = col.gameObject.GetComponent<Ball>();
            Rigidbody2D ballRB = ball.rigidBody;

            if (ball != null && ball.rigidBody != null)
            {
                Vector3 paddlePos = this.transform.position;
                Vector2 contactPoint = col.GetContact(0).point;

                float offset = paddlePos.x - contactPoint.x;
                float width = col.otherCollider.bounds.size.x / 2;

                float currentAngle = Vector2.SignedAngle(Vector2.up, ballRB.velocity);
                float bounceAngle = (offset / width) * this.maxBounceAngle;
                float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

                Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward * maxBounceAngle);
                ballRB.velocity = rotation * Vector2.up * 0.98f * ballRB.velocity.magnitude;
            }
        }
    }
}
