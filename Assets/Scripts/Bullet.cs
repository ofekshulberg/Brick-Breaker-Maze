using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D bullet;

    private float bulletSpeed = 15f;

    private void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            bullet.MovePosition(bullet.position + Vector2.up * bulletSpeed * Time.deltaTime);
        }
    }
}
