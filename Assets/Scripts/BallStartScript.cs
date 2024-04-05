using UnityEngine;

public class BallStartScript : MonoBehaviour
{
    public bool ballActive = false;

    public GameObject ballPrefab;

    public float speed = 30f;

    public float upSpeed = 6f;
    public float sideSpeed = 3.6f;

    private void Update()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnBall();
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SpawnBall()
    {
        if (!ballActive)
        {
            GameObject ballInstance = Instantiate(ballPrefab, new Vector2(transform.position.x, transform.position.y + 0.5f), transform.rotation);
            Rigidbody2D ballRb = ballInstance.GetComponent<Rigidbody2D>();

            ballRb.AddForce(new Vector2(sideSpeed, upSpeed).normalized * speed, ForceMode2D.Impulse);
            ballActive = true;
        }
    }
}
