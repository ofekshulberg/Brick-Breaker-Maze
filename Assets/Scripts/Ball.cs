using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float maxUpwardAngle = 79f;
    public float maxSidewaysAngle = 15f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BallMovement();
    }

    private void BallMovement()
    {

    }
}
